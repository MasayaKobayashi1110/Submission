using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// エネミーの行動を管理する
    /// </summary>
    public class EnemyTurnActions : MonoBehaviour
    {
        public List<EnemyAction> turns = new List<EnemyAction>(10);
        public int turnNumber;
        public bool midTurn;

        private Enemy enemy;
        private Fighter fighter;
        private Fighter player;

        private int totalDamage;
        private float weaknessDamage;
        private float vulnerableDamage;

        [SerializeField] private DamageIndicator playerDamageIndicator;

        public void Awake() 
        {
            fighter = GetComponent<Fighter>();
        }

        public void SetEnemyTurnActions(Fighter p, Enemy e) 
        {
            enemy = e;
            player = p;
            GenerateTurns();
        }

        private void GenerateTurns() 
        {
            foreach (EnemyAction eA in enemy.EnemyAction) 
            {
                for (int i = 0; i < eA.chance; i++) 
                {
                    turns.Add(eA);
                }
            }
            turns.Shuffle();
        }

        public void TakeTurn() 
        {
            switch (turns[turnNumber].intentType) 
            {
                case EnemyAction.IntentType.Attack:
                    StartCoroutine(AttackPlayer());
                    break;
                case EnemyAction.IntentType.Block:
                    ApplyBuffToSelf(turns[turnNumber].buffType);
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.Buff:
                    ApplyBuffToSelf(turns[turnNumber].buffType);
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.Debuff:
                    ApplyDebuffToPlayer(turns[turnNumber].buffType);
                    StartCoroutine(ApplyBuff());
                    break;
                default:
                    Debug.Log("この行動は予想できまい!");
                    break;
            }
        }

        private void WrapUpTurn() 
        {
            turnNumber++;
            if (turnNumber == turns.Count) turnNumber = 0;

            fighter.EndTurnUpdateDebuff();
            midTurn = false;
        }

        private IEnumerator AttackPlayer() 
        {
            totalDamage = turns[turnNumber].attackAmount + fighter.rage.buffValue;
            // 与ダメージ低下の計算をする
            if (fighter.weakness.buffValue > 0) 
            {
                weaknessDamage = totalDamage * 0.75f;
                totalDamage = (int)weaknessDamage;
            }
            // 被ダメージ増加の計算をする
            if (fighter.vulnerable.buffValue > 0) 
            {
                vulnerableDamage = totalDamage * 1.5f;
                totalDamage = (int)vulnerableDamage;
            }

            yield return new WaitForSeconds(0.5f);
            playerDamageIndicator.DisplayDamage(totalDamage);
            player.TakeDamage(totalDamage);
            yield return new WaitForSeconds(1f);
            WrapUpTurn();
        }

        private IEnumerator ApplyBuff() 
        {
            yield return new WaitForSeconds(1f);
            WrapUpTurn();
        }

        private void ApplyBuffToSelf(Buff.Type t) 
        {
            if (t == Buff.Type.rage) 
                fighter.AddRage(turns[turnNumber].buffAmount);
            if (t == Buff.Type.block) 
                fighter.AddBlock(turns[turnNumber].blockAmount);
        }

        private void ApplyDebuffToPlayer(Buff.Type t) 
        {
            if (t == Buff.Type.vulnerable) 
                player.AddVulnerable(turns[turnNumber].debuffAmount);
            if (t == Buff.Type.weakness) 
                player.AddWeakness(turns[turnNumber].debuffAmount);
        }
    }
}