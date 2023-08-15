using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// カードの情報
    /// </summary>
    [CreateAssetMenu(menuName = "Card/Card")]
    public class Card : ScriptableObject
    {
        [Header("カードの情報")]

        [Tooltip("名前")]
        [SerializeField] private string title;
        public string Title { get { return title; } }

        [Tooltip("強化しているのか")]
        [SerializeField] private bool isUpgraded;
        public bool IsUpgraded { get { return isUpgraded; } }

        [Tooltip("説明")]
        [SerializeField] private CardString description;
        public string Description {
            get {
                if (!isUpgraded) return description.baseAmount;
                else return description.upgradedAmount;
            }
        }

        [Tooltip("コスト")]
        [SerializeField] private CardInt cost;
        public int Cost {
            get {
                if (!isUpgraded) return cost.baseAmount;
                else return cost.upgradedAmount;
            }
        }

        [Tooltip("攻撃力")]
        [SerializeField] private CardInt attackAmount;
        public int AttackAmount {
            get {
                if (!isUpgraded) return attackAmount.baseAmount;
                else return attackAmount.upgradedAmount;
            }
        }

        [Tooltip("ブロック")]
        [SerializeField] private CardInt blockAmount;
        public int BlockAmount {
            get {
                if (!isUpgraded) return blockAmount.baseAmount;
                else return blockAmount.upgradedAmount;
            }
        }

        [Tooltip("バフ")]
        [SerializeField] private CardInt buffAmount;
        public int BuffAmount {
            get {
                if (!isUpgraded) return buffAmount.baseAmount;
                else return buffAmount.upgradedAmount;
            }
        }

        [Tooltip("デバフ")]
        [SerializeField] private CardInt debuffAmount;
        public int DebuffAmount {
            get {
                if (!isUpgraded) return debuffAmount.baseAmount;
                else return debuffAmount.upgradedAmount;
            }
        }

        [Tooltip("値段")]
        [SerializeField] private CardInt price;
        public int Price {
            get {
                if (!isUpgraded) return price.baseAmount;
                else return price.upgradedAmount;
            }
        }

        [Tooltip("プレイ可能かどうか")]
        [SerializeField] private CardBoolean isPlayable;
        public bool IsPlayable {
            get {
                if (!isUpgraded) return isPlayable.baseAmount;
                else return isPlayable.upgradedAmount;
            }
        }

        [Tooltip("プレイ可能かどうか")]
        [SerializeField] private CardBoolean isConsumption;
        public bool IsConsumption {
            get {
                if (!isUpgraded) return isConsumption.baseAmount;
                else return isConsumption.upgradedAmount;
            }
        }

        [Tooltip("カードのミニ画像")]
        [SerializeField] private Sprite miniVisual;
        public Sprite MiniVisual { get { return miniVisual; } }

        [Tooltip("カードの詳細画像")]
        [SerializeField] private Sprite detailVisual;
        public Sprite DetailVisual { get { return detailVisual; } }

        [Header("カードカテゴリー")]
        [SerializeField] private CardCategory category;
        public CardCategory Category { get { return category; } }
        public enum CardCategory
        {
            Attack, // 攻撃系
            Skill, // スキル系
            None, // なし
        };

        [Header("カードタイプ")]
        [SerializeField] private CardType type;
        public CardType Type { get { return type; } }
        public enum CardType 
        {
            Warrior, // 戦士専用
            Colorless, // 共有
            Curse, // 妨害、状態異常
        };
    }

    [System.Serializable]
    public struct CardInt
    {
        public int baseAmount;
        public int upgradedAmount;
    }

    [System.Serializable]
    public struct CardString
    {
        public string baseAmount;
        public string upgradedAmount;
    }

    [System.Serializable]
    public struct CardBoolean
    {
        public bool baseAmount;
        public bool upgradedAmount;
    }

    /*
    [System.Serializable]
    public struct CardSprite
    {
        public Sprite baseAmount;
        public Sprite upgradedAmount;
    }
    */

    /*
    [System.Serializable]
    public struct CardBuffs
    {
        public Buff.Type buffType;
        public CardInt buffAmount;
    }
    */
}