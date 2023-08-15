using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    public class CardActions : MonoBehaviour
    {
        private Card card;
        private Fighter enemy;
        private Fighter player;
        
        private int totalDamage;
        private float weaknessDamage;
        private float vulnerableDamage;

        private BattleManager battleManager;
        private PlayerManager playerManager;

        private void Awake() 
        {
            battleManager = GetComponent<BattleManager>();
            playerManager = FindObjectOfType<PlayerManager>();
        }

        public void SetCardAction(Fighter p, Fighter e) 
        {
            enemy = e;
            player = p;
        }

        public void PerformAction(Card c) 
        {
            card = c;
            switch (card.Title) 
            {
                #region Attack
                case "たたきつける":
                    AttackEnemy(card.AttackAmount);
                    GetVulnerable(card.DebuffAmount, enemy);
                    break;
                case "ボディプレス":
                    AttackEnemy(player.block.buffValue);
                    break;
                case "諸刃の剣":
                    AttackEnemy(card.AttackAmount);
                    AttackSelf(card.DebuffAmount);
                    break;
                case "ギャンブルアタック":
                    AttackEnemyRandomValue(card.AttackAmount);
                    break;
                case "ヘビーストライク":
                    AttackEnemy(card.AttackAmount);
                    GetRage(card.BuffAmount, player);
                    break;
                case "鉄の波動":
                    AttackEnemy(card.AttackAmount);
                    GetBlock(card.BuffAmount, player);
                    break;
                case "牽制":
                    AttackEnemy(card.AttackAmount);
                    DrawCards(card.BuffAmount);
                    break;
                case "シャッフルストライク":
                    AttackEnemy(card.AttackAmount);
                    AllDiscardCardInHand();
                    DrawCards(card.BuffAmount);
                    break;
                case "スラッシュ":
                    AttackEnemy(card.AttackAmount);
                    break;
                case "サンダー":
                    AttackEnemy(card.AttackAmount);
                    GetWeakness(card.DebuffAmount, enemy);
                    break;
                case "トリプルストライク":
                    AttackEnemy(card.AttackAmount);
                    AttackEnemy(card.AttackAmount);
                    AttackEnemy(card.AttackAmount);
                    break;
                case "ツインストライク":
                    AttackEnemy(card.AttackAmount);
                    AttackEnemy(card.AttackAmount);
                    break;
                #endregion
                #region Block
                case "ブロック":
                    GetBlock(card.BlockAmount, player);
                    break;
                case "ホーリーシールド":
                    GetBlock(card.BlockAmount, player);
                    break;
                case "鉄壁":
                    GetBlock(card.BlockAmount, player);
                    break;
                #endregion
                #region Curse
                case "やけど":
                    AttackSelf(card.AttackAmount);
                    break;
                case "骨折":
                    break;
                case "忘却":
                    break;
                #endregion
                #region Skill
                case "回復":
                    GetHealth(card.BuffAmount);
                    break;
                case "どくどく":
                    break;
                #endregion
                default:
                    Debug.Log("なんだ、このカードは!?");
                    break;
            }
        }

        // エネミーを攻撃する
        private void AttackEnemy(int amount)
        {
            // 基礎ダメージとバフデバフの計算を行う
            totalDamage = amount + player.rage.buffValue;
            // 与ダメージ低下の計算をする
            if (player.weakness.buffValue > 0) 
            {
                weaknessDamage = totalDamage * 0.75f;
                totalDamage = (int)weaknessDamage;
            }
            // 被ダメージ増加の計算をする
            if (enemy.vulnerable.buffValue > 0) 
            {
                vulnerableDamage = totalDamage * 1.5f;
                totalDamage = (int)vulnerableDamage;
            }

            #region レリック
            if (playerManager.HasRelics("赤い宝石")) 
                totalDamage -= 1;
            if (playerManager.HasRelics("赤いグローブ")) 
                totalDamage -= 1;
            if (playerManager.HasRelics("赤い盾")) 
                totalDamage -= 1;
            #endregion

            if (totalDamage < 0) totalDamage = 0;

            enemy.TakeDamage(totalDamage);
        }

        // エネミーを攻撃する(Random)
        private void AttackEnemyRandomValue(int amount)
        {
            totalDamage = Random.Range(0, amount);
            // 基礎ダメージとバフデバフの計算を行う
            totalDamage += player.rage.buffValue;
            // 与ダメージ低下の計算をする
            if (player.weakness.buffValue > 0) 
            {
                weaknessDamage = totalDamage * 0.75f;
                totalDamage = (int)weaknessDamage;
            }
            // 被ダメージ増加の計算をする
            if (enemy.vulnerable.buffValue > 0) 
            {
                vulnerableDamage = totalDamage * 1.5f;
                totalDamage = (int)vulnerableDamage;
            }

            #region レリック
            if (playerManager.HasRelics("赤い宝石")) 
                totalDamage -= 1;
            if (playerManager.HasRelics("赤いグローブ")) 
                totalDamage -= 1;
            if (playerManager.HasRelics("赤い盾")) 
                totalDamage -= 1;
            #endregion

            if (totalDamage < 0) totalDamage = 0;

            enemy.TakeDamage(totalDamage);
        }

        private void AttackSelf(int amount) 
        {
            player.TakeDamage(amount);
        }

        private void GetRage(int amount, Fighter f) 
        {
            f.AddRage(amount);
        }

        private void GetBlock(int amount, Fighter f) 
        {
            f.AddBlock(amount);
        }

        private void GetVulnerable(int amount, Fighter f) 
        {
            f.AddVulnerable(amount);
        }

        private void GetWeakness(int amount, Fighter f) 
        {
            f.AddWeakness(amount);
        }

        private void GetHealth(int amount) 
        {
            player.GetHealth(amount);
        }

        private void DrawCards(int amount) 
        {
            battleManager.DrawCards(amount);
        }

        private void AllDiscardCardInHand() 
        {
            battleManager.AllDiscardCard();
        }

    }
}