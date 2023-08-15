using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// バトルするキャラクターの管理
    /// </summary>
    public class Fighter : MonoBehaviour
    {
        [Header("ステータス")]
        public int maxHealth;
        public int currentHealth;
        public int currentBlock = 0;
        public FighterUI fighterUI;

        [Header("バフ")]
        public Buff rage;
        public Buff block;
        public Buff weakness;
        public Buff vulnerable;
        public GameObject buffPrefab;
        public Transform buffParent;

        public bool isPlayer;
        public Enemy enemy;

        private PlayerManager playerManager;
        private BattleManager battleManager;

        [SerializeField] private DamageIndicator damageIndicator;
        public int totalDamage;

        private void Awake() 
        {
            playerManager = FindObjectOfType<PlayerManager>();
            battleManager = FindObjectOfType<BattleManager>();
            fighterUI = GetComponent<FighterUI>();
        }

        // エネミーのセット
        public void SetEnemy(Enemy e) 
        {
            isPlayer = false;
            enemy = e;
            fighterUI.SetFighterName(enemy.Title);
            maxHealth = enemy.MaxHealth;
            currentHealth = maxHealth;
            fighterUI.SetMaxHealth(maxHealth);
            fighterUI.DisplayHealth(currentHealth);
            fighterUI.SetFighterVisual(enemy.Visual);
            ResetBuffs();
        }

        // プレイヤーのセット
        public void SetPlayer() 
        {
            isPlayer = true;
            fighterUI.SetFighterName(playerManager.GetCharacterType());
            maxHealth = playerManager.GetMaxHealth();
            currentHealth = maxHealth;
            fighterUI.SetMaxHealth(maxHealth);
            fighterUI.DisplayHealth(currentHealth);
            fighterUI.SetFighterVisual(playerManager.GetCharacterVisual());
            ResetBuffs();
        }

        // バフのリセット
        public void ResetBuffs() 
        {
            currentBlock = 0;
            rage.buffValue = 0;
            block.buffValue = 0;
            vulnerable.buffValue = 0;
            weakness.buffValue = 0;
            fighterUI.ResetBuffUI();
        }

        // 各種バフデバフの追加
        #region Buff
        public void AddRage(int amount)
        {
            rage.buffValue += amount;
            if (rage.buffValue < 0) 
                rage.buffValue = 0;
            fighterUI.SetBuffUI(rage);
        }

        public void AddBlock(int amount) 
        {
            block.buffValue += amount;
            if (block.buffValue < 0) 
                block.buffValue = 0;
            fighterUI.SetBuffUI(block);
        }

        public void AddVulnerable(int amount)
        {
            vulnerable.buffValue += amount;
            if (vulnerable.buffValue < 0) 
                vulnerable.buffValue = 0;
            fighterUI.SetBuffUI(vulnerable);
        }

        public void AddWeakness(int amount) 
        {
            weakness.buffValue += amount;
            if (weakness.buffValue < 0) 
                weakness.buffValue = 0;
            fighterUI.SetBuffUI(weakness);
        }
        #endregion

        // ターン終了時のデバフの再評価
        public void EndTurnUpdateDebuff() 
        {
            AddVulnerable(-1);
            AddWeakness(-1);
        }

        // ターン終了時のバフの再評価
        public void EndTurnUpdateBuff() 
        {
            AddRage(-rage.buffValue);
            AddBlock(-block.buffValue);
        }

        // ダメージを表示する
        public void DisplayDamage() 
        {
            damageIndicator.DisplayDamage(totalDamage);
        }

        // ダメージを受ける
        public void TakeDamage(int amount) 
        {
            // ブロックの計算
            if (block.buffValue > 0) amount = BlockDamage(amount);

            // ダメージを受ける
            if (amount != 0) 
            {
                totalDamage += amount;
                currentHealth -= amount;
                fighterUI.DisplayHealth(currentHealth);
            }

            // HPが0になる
            if (currentHealth <= 0) 
                Die();
        }

        // ブロックの計算
        public int BlockDamage(int amount) 
        {
            // 完全ブロック
            if (block.buffValue >= amount) 
            {
                block.buffValue -= amount;
                amount = 0;
            } 
            else 
            {
                amount -= block.buffValue;
                block.buffValue = 0;
            }

            fighterUI.SetBuffUI(block);
            return amount;
        }

        // 回復
        public void GetHealth(int amount) 
        {
            currentHealth += amount;
            if (currentHealth > maxHealth) 
                currentHealth = maxHealth;
            fighterUI.DisplayHealth(currentHealth);
        }

        // 最大HPの増加
        public void GetMaxHealth(int amount) 
        {
            maxHealth += amount;
            currentHealth += amount;
            fighterUI.DisplayMaxHealth(amount);
            fighterUI.DisplayHealth(currentHealth);
        }

        public void Die() 
        {
            if (!isPlayer) 
            {
                // Win
                battleManager.EndFight(true);
            }
            else 
            {
                battleManager.EndFight(false);
            }
        }

        public void RemoveCard(Card c) 
        {
            playerManager.RemoveCardinPlayerDeck(c);
        } 

    }
}