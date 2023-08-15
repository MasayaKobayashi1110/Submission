using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// エネミーの情報
    /// </summary>
    [CreateAssetMenu (menuName = "Enemy/Enemy")]
    public class Enemy : ScriptableObject
    {
        [Header("基本情報")]
        [Tooltip("名前")]
        [SerializeField] private string title;
        public string Title { get { return title; } }

        [Tooltip("ビジュアル")]
        [SerializeField] private Sprite visual;
        public Sprite Visual { get { return visual; } }

        [Tooltip("最大体力")]
        [SerializeField] private int maxHealth;
        public int MaxHealth { get { return maxHealth; } }

        [Tooltip("ドロップ")]
        [SerializeField] private int coin;
        public int Coin { get { return coin; } }

        [Tooltip("強敵")]
        [SerializeField] private bool isElite;
        public bool IsElite { get { return isElite; } }

        [Tooltip("ボス")]
        [SerializeField] private bool isBoss;
        public bool IsBoss { get { return isBoss; } }

        [Tooltip("行動")]
        [SerializeField] private List<EnemyAction> enemyAction = new List<EnemyAction>(5);
        public List<EnemyAction> EnemyAction { get { return enemyAction; } }


    }
}