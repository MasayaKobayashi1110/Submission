using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// キャラクター情報
    /// </summary>
    [CreateAssetMenu(menuName = "Character/Character")]
    public class Character : ScriptableObject 
    {
        [Header("キャラクターの情報")]

        [Tooltip("キャラクタークラス")]
        [SerializeField] private CharacterType type;
        public CharacterType Type { get { return type; } }
        public enum CharacterType {
            Warrior,
        };

        [Tooltip("キャラクターの画像")]
        [SerializeField] private Sprite visual;
        public Sprite Visual { get { return visual; } }

        [Tooltip("初期レリック")]
        [SerializeField] private Relic startingRelic;
        public Relic StartingRelic { get { return startingRelic; } }

        [Tooltip("初期デッキ")] 
        [SerializeField] private List<Card> startingDeck = new List<Card>(10);
        public List<Card> StartingDeck { get { return startingDeck; } }
        
        [Header("キャラクターの初期ステータス")]

        [Tooltip("最大体力")]
        [SerializeField] private int maxHealth;
        public int MaxHealth { get { return maxHealth; } }

        [Tooltip("最大エネルギー")]
        [SerializeField] private int maxEnergy;
        public int MaxEnergy { get { return maxEnergy; } }

        [Tooltip("初期ドロー枚数")]
        [SerializeField] private int drawAmount;
        public int DrawAmount { get { return drawAmount; } }
    }
}