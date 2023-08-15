using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MK 
{
    public class MysteryManager : MonoBehaviour
    {
        [SerializeField] private EpisodeDatabase episodeDatabase;

        public List<Episode> unsolvedEpisodes = new List<Episode>(10);
        public List<Episode> resolvedEpisodes = new List<Episode>(10);

        private Episode seletcedEpisode;

        private int random;

        [SerializeField] private TMP_Text episodeText;
        [SerializeField] private GameObject choice1;
        [SerializeField] private TMP_Text choice1Text;
        [SerializeField] private GameObject choice2;
        [SerializeField] private TMP_Text choice2Text;
        [SerializeField] private GameObject choice3;
        [SerializeField] private TMP_Text choice3Text;

        private RelicManager relicManager;
        [SerializeField]private Fighter player;
        private PlayerStatsUI playerStatsUI;
        private ScreenManager screenManager;

        private void Awake() 
        {
            relicManager = FindObjectOfType<RelicManager>();
            playerStatsUI = FindObjectOfType<PlayerStatsUI>();
            screenManager = FindObjectOfType<ScreenManager>();
            ClearList();
            SetEpisodeList();
        }

        public void ClearList() 
        {
            unsolvedEpisodes.Clear();
            resolvedEpisodes.Clear();
        }

        public void Initialization() 
        {
            ClearList();
            SetEpisodeList();
        }

        public void SetEpisodeList() 
        {
            unsolvedEpisodes.AddRange(episodeDatabase.GetEpisodeList());
        }

        public void SetEpisode() 
        {
            random = Random.Range(0, unsolvedEpisodes.Count - 1);
            seletcedEpisode = unsolvedEpisodes[random];
            episodeText.text = seletcedEpisode.TextFile.text;
            SetNumAndChoice();
            resolvedEpisodes.Add(seletcedEpisode);
            unsolvedEpisodes.Remove(seletcedEpisode);
        }

        private void SetNumAndChoice() 
        {
            // 選択肢の数を取得する
            random = seletcedEpisode.NumChoices;

            // 選択肢をすべて非アクティブ化する
            choice1.SetActive(false);
            choice2.SetActive(false);
            choice3.SetActive(false);

            // 選択肢の数に合わせて回答ボタンをアクティブ化する
            if (random >= 1) 
            {
                choice1.SetActive(true);
                choice1Text.text = seletcedEpisode.Choice1;
            }
            if (random >= 2) 
            {
                choice2.SetActive(true);
                choice2Text.text = seletcedEpisode.Choice2;
            }
            if (random >= 3) 
            {
                choice3.SetActive(true);
                choice3Text.text = seletcedEpisode.Choice3;
            }
        }

        public void PushButton(int num) 
        {
            if (seletcedEpisode.Title == "邪な石") 
            {
                if (num == 1) 
                {
                    Relic r = relicManager.GetSelectRelic("Curse", "邪な石");
                    relicManager.SetRelic(r);
                    playerStatsUI.DisplayRelics(r);
                    GoToMap();
                }
                else 
                {
                    player.TakeDamage(10);
                    GoToMap();
                }
            }
            else if (seletcedEpisode.Title == "グローブ") 
            {
                if (num == 1) 
                {
                    random = Random.Range(0,99);
                    if (random <= 49) 
                    {
                        Relic r = relicManager.GetSelectRelic("Curse", "邪なグローブ");
                        relicManager.SetRelic(r);
                        playerStatsUI.DisplayRelics(r);
                    }
                    else 
                    {
                        Relic r = relicManager.GetSelectRelic("Rare", "聖なるグローブ");
                        relicManager.SetRelic(r);
                        playerStatsUI.DisplayRelics(r);
                    }
                    GoToMap();
                }
                else 
                {
                    GoToMap();
                }
            }
            else if (seletcedEpisode.Title == "回復") 
            {
                if (num == 1) 
                {
                    player.GetHealth(player.maxHealth);
                    GoToMap();
                }
                else 
                {
                    GoToMap();
                }
            }
            else if (seletcedEpisode.Title == "宝石")
            {
                if (num == 1) 
                {
                    Relic r = relicManager.GetSelectRelic("Rare", "聖なる宝石");
                    relicManager.SetRelic(r);
                    playerStatsUI.DisplayRelics(r);
                    GoToMap();
                }
                else 
                {
                    GoToMap();
                }
            }
            else if (seletcedEpisode.Title == "聖なる盾") 
            {
                if (num == 1) 
                {
                    if (playerStatsUI.coinAmount > 300) 
                    {
                        playerStatsUI.SetCoins(-300);
                        Relic r = relicManager.GetSelectRelic("Rare", "聖なる盾");
                        relicManager.SetRelic(r);
                        playerStatsUI.DisplayRelics(r);
                    }
                    
                    GoToMap();
                }
                else 
                {
                    GoToMap();
                }
            }
            else if (seletcedEpisode.Title == "聖剣") 
            {
                if (num == 1) 
                {
                    player.TakeDamage(30);
                    Relic r = relicManager.GetSelectRelic("Rare", "聖剣");
                    relicManager.SetRelic(r);
                    playerStatsUI.DisplayRelics(r);
                    GoToMap();
                }
                else 
                {
                    GoToMap();
                }
            }
            else if (seletcedEpisode.Title == "右か左か") 
            {
                if (num == 1) 
                {
                    random = Random.Range(0,3);
                    if (random < 3) 
                        player.GetHealth(10);
                    else 
                        player.TakeDamage(10);
                    GoToMap();
                }
                else 
                {
                    random = Random.Range(0,3);
                    if (random < 2) 
                        player.GetHealth(20);
                    else 
                        player.TakeDamage(20);
                    GoToMap();
                }
            }
        }

        private void GoToMap() 
        {
            screenManager.SelectScreen("Map");
        }
    }
}