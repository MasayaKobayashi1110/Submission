using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// カードの管理をする
    /// </summary>
    public class CardManager : MonoBehaviour
    {
        // カードのデータベース
        [SerializeField]
        private CardDatabase cardDatabase;

        // プレイヤーのデッキ
        public List<Card> playerDeck = new List<Card>(20);
        // 戦士専用カードのリスト
        private List<Card> warriorCardList = new List<Card>(10);
        // 色なし（共有）カードのリスト
        private List<Card> colorlessCardList = new List<Card>(10);
        // 妨害、状態異常カードのリスト
        private List<Card> curseCardList = new List<Card>(5);

        
        // 初期化
        private void Awake() 
        {
            ClearAllCardList();
            SetTypeCardList();
        }

        public void Initialization() 
        {
            ClearAllCardList();
            SetTypeCardList();
        }

        // 全てのリストを空にする
        private void ClearAllCardList() 
        {
            playerDeck.Clear();
            warriorCardList.Clear();
            colorlessCardList.Clear();
            curseCardList.Clear();
        }

        // カードのクラスでリスト分けをする
        private void SetTypeCardList() 
        {
            for (int i = 0; i < cardDatabase.GetCardList().Count; i++) 
            {
                Card c = cardDatabase.GetCardList()[i];
                if (c.Type == Card.CardType.Warrior) 
                    warriorCardList.Add(c);
                else if (c.Type == Card.CardType.Colorless)
                    colorlessCardList.Add(c);
                else if (c.Type == Card.CardType.Curse)
                    curseCardList.Add(c);
            }
        }

        // プレイヤーの初期デッキをセットする関数
        public void SetPlayerDeck(Character character) 
        {
            playerDeck.AddRange(character.StartingDeck);
        }

        // プレイヤーのデッキを返す関数
        public List<Card> GetPlayerDeck() 
        {
            return playerDeck;
        }

        // タイプを指定してそのリストのサイズを返す関数
        public int GetCountCardList(Card.CardType t) 
        {
            int value = 0;
            // 戦士
            if (t == Card.CardType.Warrior)
                value = warriorCardList.Count;
            // 共有
            else if (t == Card.CardType.Colorless)
                value = colorlessCardList.Count;
            // 状態異常
            else if (t == Card.CardType.Curse)
                value = curseCardList.Count;
            return value;
        }

        // タイプを指定してそのリストのカードをランダムで返す関数
        public Card GetRandomCard(Card.CardType t) 
        {
            Card c = null;
            if (GetCountCardList(t) == 0) return c;
            // ランダムな値
            int value = Random.Range(0, GetCountCardList(t) - 1);
            
            // 戦士
            if (t == Card.CardType.Warrior)
                c = warriorCardList[value];
            // 共有
            else if (t == Card.CardType.Colorless) 
                c = colorlessCardList[value];
            // 状態異常
            else if (t == Card.CardType.Curse) 
                c = curseCardList[value];
            
            return c;
        }
    }
}