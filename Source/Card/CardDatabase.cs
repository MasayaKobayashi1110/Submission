using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK
{
    /// <summary>
    /// カードのデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "Card/CardDatabase")]
    public class CardDatabase : ScriptableObject
    {
        // カードデータを格納する
        [SerializeField]
        private List<Card> cardList = new List<Card>(30);

        // カードデータの一覧を返す関数
        public List<Card> GetCardList() 
        {
            return cardList;
        }

        // 名前を指定してそのカードを返す
        public Card GetCard(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c;
        }

        // 名前を指定してそのカードのisUpgradedを返す
        public bool GetIsUpgraded(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.IsUpgraded;
        }

        // 名前を指定してそのカードのdescriptionを返す
        public string GetDescription(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.Description;
        }

        // 名前を指定してそのカードのcostを返す
        public int GetCost(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.Cost;
        }

        // 名前を指定してそのカードのattackAmountを返す
        public int GetAttackAmount(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.AttackAmount;
        }

        // 名前を指定してそのカードのBlockAmountを返す
        public int GetBlockAmount(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.BlockAmount;
        }

        // 名前を指定してそのカードのbuffAmountを返す
        public int GetBuffAmount(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.BuffAmount;
        }

        // 名前を指定してそのカードのdebuffAmountを返す
        public int GetDebuffAmount(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.DebuffAmount;
        }

        // 名前を指定してそのカードのpriceを返す
        public int GetPrice(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.Price;
        }

        // 名前を指定してそのカードのminiVisualを返す
        public Sprite GetMiniVisual(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.MiniVisual;
        }

        // 名前を指定してそのカードのdetailVisualを返す
        public Sprite GetDetailVisual(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.DetailVisual;
        }

        // 名前を指定してそのカードのisPlayableを返す
        public bool GetIsPlayable(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.IsPlayable;
        }

        // 名前を指定してそのカードのcategoryを返す
        public Card.CardCategory GetCategory(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.Category;
        }

        // 名前を指定してそのカードのtypeを返す
        public Card.CardType GetType(string title) 
        {
            Card c = cardList.Find(data => data.Title == title);
            return c.Type;
        }
    }
}