using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MK 
{
    /// <summary>
    /// リワード画面の管理
    /// </summary>
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private RewardUI coinReward; //コイン
        [SerializeField] private RewardUI relicReward; // レリック
        [SerializeField] private GameObject cardButton; // カード(ボタン)
        [SerializeField] private List<RewardUI> cardRewards = new List<RewardUI>(3);
        private List<Card> rewardCardList = new List<Card>(3);
        [SerializeField] private GameObject cardRewardContainer;
        private PlayerManager playerManager;
        private CardManager cardManager;
        private RelicManager relicManager;

        private ScreenManager screenManager;
        private PlayerStatsUI playerStatsUI;

        private void Awake() 
        {
            playerManager = FindObjectOfType<PlayerManager>();
            cardManager = FindObjectOfType<CardManager>();
            relicManager = FindObjectOfType<RelicManager>();
            screenManager = FindObjectOfType<ScreenManager>();
            playerStatsUI = FindObjectOfType<PlayerStatsUI>();
        }

        public void ActiveCoin(bool b) 
        {
            coinReward.gameObject.SetActive(b);
        }

        public void ActiveRelic(bool b) 
        {
            relicReward.gameObject.SetActive(b);
        }

        public void ActiveCard(bool b) 
        {
            cardButton.SetActive(b);
        }

        public void DisplayCoin(int amount) 
        {
            coinReward.DisplayCoin(amount);
        }

        public void HandleCards() 
        {
            if (rewardCardList.Count != 0) rewardCardList.Clear();
            for (int i = 0; i < 3; i++) 
            {
                if (i == 2) 
                {
                    rewardCardList.Add(cardManager.GetRandomCard(Card.CardType.Colorless));
                    continue;
                }
                rewardCardList.Add(cardManager.GetRandomCard(Card.CardType.Warrior));
            }

            cardRewardContainer.SetActive(true);
            // 3枚表示
            for (int i = 0; i < 3; i++) 
            {
                cardRewards[i].DisplayCard(rewardCardList[i]);
            }
        }

        // カードの報酬選択
        public void SelectedCard(int cardIndex) 
        {
            // デッキにカードの追加をする
            cardManager.playerDeck.Add(rewardCardList[cardIndex]);

            // カードの非表示
            cardRewardContainer.SetActive(false);
            screenManager.SelectScreen("Map");
        }

        public void GetRelic()
        {
            Relic r = relicManager.GetRandomRelic();
            relicReward.DisplayRelic(r);
            relicManager.SetRelic(r);
            playerStatsUI.DisplayRelics(r);
            playerManager.CheckRelics(r);
        }
    }
}