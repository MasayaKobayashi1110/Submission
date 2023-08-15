using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK 
{
    /// <summary>
    /// バトルを管理する
    /// </summary>
    public class BattleManager : MonoBehaviour
    {
        [Header("カード")]
        private List<Card> drawPile = new List<Card>(20);
        private List<Card> cardsInHand = new List<Card>(7);
        private List<Card> discardPile = new List<Card>(20);
        private List<CardUI> selectedCards = new List<CardUI>(7);
        [SerializeField] private List<CardUI> cardsInHandGameObjects = new List<CardUI>(7);

        [Header("デッキUI")]
        private int drawAmount;
        [SerializeField] private TMP_Text discardPileCountText;
        [SerializeField] private TMP_Text drawPileCountText;

        [Header("エネルギー")]
        private int maxEnergy;
        public int currentEnergy;
        [SerializeField] private TMP_Text energyText;
        [SerializeField] private List<GameObject> energyObject = new List<GameObject>(8);

        [Header("体力")]
        private int maxHealth;
        private int currentHealth;

        private PlayerManager playerManager;
        private EnemyManager enemyManager;
        private CardActions cardActions;
        private EnemyTurnActions enemyTurnActions;
        private RewardManager rewardManager;
        private ScreenManager screenManager;
        private TutorialManager tutorialManager;

        [SerializeField] private GameObject rewardCanvas;

        [SerializeField] private Fighter enemy;
        [SerializeField] private Fighter player;

        private int random;
        private int damageAmount;
        private int turnNum;

        private Turn turn;
        private enum Turn 
        {
            Player,
            Enemy
        };
        [SerializeField] private GameObject endTurnButton;

        [SerializeField] private Animator enemyTurn;
        [SerializeField] private Animator playerTurn;

        public int totalDamage;
        public int totalPlayerDamage;

        private void Awake() 
        {
            playerManager = FindObjectOfType<PlayerManager>();
            enemyManager = FindObjectOfType<EnemyManager>();
            cardActions = GetComponent<CardActions>();
            enemyTurnActions = FindObjectOfType<EnemyTurnActions>();
            rewardManager = FindObjectOfType<RewardManager>();
            screenManager = FindObjectOfType<ScreenManager>();
            tutorialManager = GetComponent<TutorialManager>();
        }

        public void StartEnemyFight() 
        {
            Debug.Log("通常戦闘");
            tutorialManager.OnTutorial();
            random = Random.Range(0, enemyManager.enemyList.Count - 1);
            BeginBattle(enemyManager.enemyList[random]);
        }

        public void StartEliteFight() 
        {
            Debug.Log("強敵戦闘");
            random = Random.Range(0, enemyManager.eliteList.Count - 1);
            BeginBattle(enemyManager.eliteList[random]);
        }

        public void StartBossFight() 
        {
            Debug.Log("ボス戦闘");
            random = Random.Range(0, enemyManager.bossList.Count - 1);
            BeginBattle(enemyManager.bossList[random]);
        }

        private void BeginBattle(Enemy e) 
        {
            turnNum = 1;
            turn = Turn.Player;
            cardActions.SetCardAction(player, enemy);
            enemy.SetEnemy(e);
            enemyTurnActions.SetEnemyTurnActions(player, e);

            drawAmount = playerManager.GetDrawAmount();
            maxEnergy = playerManager.GetMaxEnergy();
            maxHealth = playerManager.GetMaxHealth();

            #region レリック
            if (playerManager.HasRelics("黒い宝石")) 
                maxEnergy += 1;
            if (playerManager.HasRelics("聖なる宝石")) 
                maxEnergy += 1;
            if (playerManager.HasRelics("赤い宝石")) 
                maxEnergy += 1;
            if (playerManager.HasRelics("黒いグローブ")) 
                drawAmount += 1;
            if (playerManager.HasRelics("黒いグローブ")) 
                drawAmount += 1;
            if (playerManager.HasRelics("聖なるグローブ")) 
                drawAmount += 1;
            if (playerManager.HasRelics("赤いグローブ")) 
                drawAmount += 1;
            if (playerManager.HasRelics("邪なグローブ")) 
                drawAmount -= 1;
            if (playerManager.HasRelics("黒い盾")) 
                player.AddBlock(2);
            if (playerManager.HasRelics("聖なる盾")) 
                player.AddBlock(5);
            if (playerManager.HasRelics("赤い盾")) 
                player.AddBlock(1);
            #endregion
            currentEnergy = maxEnergy;

            // エネルギーをリセットする
            ResetEnergyUI();
            UpdateEnergyText();

            // 手札があったらすべて墓地へ送る
            foreach (Card card in cardsInHand) 
                DiscardCard(card);
            
            // 全てのcardUIを非表示にする
            foreach (CardUI cardUI in cardsInHandGameObjects) 
                cardUI.gameObject.SetActive(false);
            
            drawPile.Clear();
            cardsInHand.Clear();
            discardPile.Clear();
            discardPile.AddRange(playerManager.GetPlayerDeck());
            ShuffleCards();
            DrawCards(drawAmount);

        }

        // カードを墓地から山札へ（シャッフルして）
        private void ShuffleCards() 
        {
            discardPile.Shuffle();
            drawPile.AddRange(discardPile);
            discardPile.Clear();
            discardPileCountText.text = $"捨て札({discardPile.Count})";
        }

        // ドロー
        public void DrawCards(int amount) 
        {
            int cardsDrawn = 0;
            // ドロー枚数まで、もしくは7枚まで
            while (cardsDrawn < amount && cardsInHand.Count < 7) 
            {
                // 山札が切れたらシャッフル
                if (drawPile.Count < 1) ShuffleCards();

                cardsInHand.Add(drawPile[0]);
                DisplayCardInHand(drawPile[0]);
                drawPile.Remove(drawPile[0]);
                drawPileCountText.text = $"山札({drawPile.Count})";
                cardsDrawn++;
            }
        }

        private void DisplayCardInHand(Card c) 
        {
            CardUI cardUI = cardsInHandGameObjects[cardsInHand.Count - 1];
            cardUI.LoadCard(c);
            cardUI.gameObject.SetActive(true);
        }

        public void AllDiscardCard() 
        {
            foreach(Card card in cardsInHand) DiscardCard(card);
            foreach(CardUI cardUI in cardsInHandGameObjects) { 
                cardUI.gameObject.SetActive(false);
                cardsInHand.Remove(cardUI.card);
            }
        }

        private void DiscardCard(Card c) 
        {
            discardPile.Add(c);
            discardPileCountText.text = $"捨て札({discardPile.Count})";
        }

        public void SelectCard(CardUI c) 
        {
            selectedCards.Add(c);
            currentEnergy -= c.card.Cost;
            UpdateEnergyText();
            ReduceEnergyUI(c.card.Cost);
        }

        public void DeselectCard(CardUI c) 
        {
            selectedCards.Remove(c);
            currentEnergy += c.card.Cost;
            UpdateEnergyText();
            IncreaseEnergyUI(c.card.Cost);
        }

        public void PlayCard() 
        {
            #region カード枚数攻撃バフ系レリック 発動
            if (playerManager.HasRelics("短剣") == true && selectedCards.Count >= 2) 
                player.AddRage(2);
            if (playerManager.HasRelics("剣") == true && selectedCards.Count >= 3) 
                player.AddRage(4);
            if (playerManager.HasRelics("聖剣") == true && selectedCards.Count >= 4) 
                player.AddRage(10);
            if (playerManager.HasRelics("邪な剣") == true && selectedCards.Count >= 2) 
                player.AddRage(-2);
            #endregion

            totalDamage = 0;
            player.totalDamage = 0;
            totalPlayerDamage = 0;
            enemy.totalDamage = 0;

            for (int i = 0; i < selectedCards.Count; i++) 
            {
                if (player.currentHealth > 0  && enemy.currentHealth > 0) 
                {
                    cardActions.PerformAction(selectedCards[i].card);
                }
            }

            if (player.totalDamage != 0)
                player.DisplayDamage();
            if (enemy.totalDamage != 0)
                enemy.DisplayDamage();

            #region カード枚数攻撃バフ系レリック 解除
            if (playerManager.HasRelics("邪な剣") == true && selectedCards.Count >= 2) 
                player.AddRage(2);
            if (playerManager.HasRelics("短剣") == true && selectedCards.Count >= 2) 
                player.AddRage(-2);
            if (playerManager.HasRelics("剣") == true && selectedCards.Count >= 3) 
                player.AddRage(-4);
            if (playerManager.HasRelics("聖剣") == true && selectedCards.Count >= 4) 
                player.AddRage(-10);
            #endregion

            for (int i = 0; i < selectedCards.Count; i++) 
            {
                selectedCards[i].gameObject.SetActive(false);
                cardsInHand.Remove(selectedCards[i].card);
                if (selectedCards[i].card.IsConsumption == true) 
                {
                    player.RemoveCard(selectedCards[i].card);
                    continue;
                }
                DiscardPlayedCard(selectedCards[i].card);
            }
            selectedCards.Clear();
        }

        private void DiscardPlayedCard(Card c) 
        {
            discardPile.Add(c);
            discardPileCountText.text = $"捨て札({discardPile.Count})";
        }

        private void UpdateEnergyText() 
        {
            energyText.text = $"{currentEnergy}/{maxEnergy}";
        }

        private void ReduceEnergyUI(int cost) 
        {
            for (int j = 0; j < cost; j++)
            {
                for (int i = energyObject.Count - 1; i > -1; i--) 
                {
                    if (energyObject[i].activeSelf) 
                    {
                        energyObject[i].SetActive(false);
                        break;
                    }
                }
            }
        }

        private void IncreaseEnergyUI(int cost) 
        {
            for (int j = 0; j < cost; j++) 
            {
                for (int i = 0; i < energyObject.Count; i++) 
                {
                    if (energyObject[i].activeSelf == false) 
                    {
                        energyObject[i].SetActive(true);
                        break;
                    }
                } 
            }
        }

        private void ResetEnergyUI() 
        {
            foreach (GameObject energy in energyObject) 
            {
                energy.SetActive(false);
            }

            for (int i = 0; i < maxEnergy; i++) 
            {
                energyObject[i].SetActive(true);
            }
        }

        public void ChangeTurn() 
        {
            if (turn == Turn.Player) 
            {
                turn = Turn.Enemy;
                endTurnButton.SetActive(false);

                AllDiscardCard();

                enemy.EndTurnUpdateBuff();
                player.EndTurnUpdateDebuff();
                StartCoroutine(HandleEnemyTurn());
            }
            else 
            {
                turnNum += 1;
                turn = Turn.Player;
                StartCoroutine(HandlePlayerTurn());
            }
        }

        private IEnumerator HandlePlayerTurn()
        {
            playerTurn.SetTrigger("TurnStart");
            yield return new WaitForSeconds(2.0f);
            player.EndTurnUpdateBuff();

            ResetEnergyUI();
            
            currentEnergy = maxEnergy;
            UpdateEnergyText();

            # region レリック
            if (playerManager.HasRelics("黒い盾")) 
                player.AddBlock(2);
            if (playerManager.HasRelics("聖なる盾")) 
                player.AddBlock(5);
            if (playerManager.HasRelics("赤い盾")) 
                player.AddBlock(1);
            if (playerManager.HasRelics("怒りの仮面") && turnNum % 3 == 0) 
                player.AddRage(5);
            if (playerManager.HasRelics("鉄の仮面") && turnNum % 3 == 0) 
                player.AddBlock(5);
            # endregion

            endTurnButton.SetActive(true);
            DrawCards(drawAmount);
        }

        private IEnumerator HandleEnemyTurn() 
        {
            enemyTurn.SetTrigger("TurnStart");
            yield return new WaitForSeconds(2.0f);
            enemyTurnActions.midTurn = true;
            enemyTurnActions.TakeTurn();
            yield return new WaitForSeconds(0.5f);
            ChangeTurn();
        }

        public void EndFight(bool win) 
        {
            if (!win) 
            {
                player.ResetBuffs();
                Debug.Log("GameOver");
                screenManager.SelectScreen("GameOver");
            }

            Debug.Log("Win");
            player.ResetBuffs();
            if (playerManager.HasRelics("吸血鬼の牙")) 
                player.GetHealth(6);
            
            if (enemy.enemy.IsBoss == false)
                HandleRewardScreen();
            else 
                HandleRewardBoss();
        }

        private void HandleRewardScreen() 
        {
            rewardCanvas.SetActive(true);
            rewardManager.ActiveCoin(true);
            rewardManager.DisplayCoin(enemy.enemy.Coin);
            playerManager.SetCoins(enemy.enemy.Coin);
            rewardManager.ActiveCard(true);

            if (enemy.enemy.IsElite) 
            {
                rewardManager.ActiveRelic(true);
                rewardManager.GetRelic();
            }
            else 
            {
                rewardManager.ActiveRelic(false);
            }
        }

        private void HandleRewardBoss() 
        {
            playerManager.SetCoins(enemy.enemy.Coin);
            screenManager.SelectScreen("GameClear");
        }
    }
}