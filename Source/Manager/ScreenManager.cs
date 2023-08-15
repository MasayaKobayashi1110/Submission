using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK_Map;

namespace MK 
{
    public class ScreenManager : MonoBehaviour
    {
        public GameObject titleScreen;
        public GameObject mapScreen;
        public GameObject battleScreen;
        public GameObject rewardScreen;
        public GameObject mysteryScreen;
        public GameObject storeScreen;
        public GameObject treasureScreen;
        public GameObject restScreen;
        public GameObject gameOverScreen;
        public GameObject gameClearScreen;
        public GameObject options;
        public GameObject regenerationButton;

        [SerializeField] private BgmManager titleBgmManager;
        [SerializeField] private BgmManager mainBgmManager;
        private bool titleBgm = false;
        
        private MapGenerator mapGenerator;
        private BattleManager battleManager;
        private MysteryManager mysteryManager;
        private PlayerManager playerManager;
        private SceneFader sceneFader;

        private bool resetMap = true;
        [SerializeField]
        private Fighter player;

        private void Awake() 
        {
            mapGenerator = FindObjectOfType<MapGenerator>();
            battleManager = FindObjectOfType<BattleManager>();
            mysteryManager = FindObjectOfType<MysteryManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            sceneFader = GetComponent<SceneFader>();
        }

        private void Update() 
        {
            if (Input.GetKey(KeyCode.Space)) 
                SelectScreen("Map");
        }

        public void SelectScreen(string s) 
        {
            StartCoroutine(LoadScreen(s));
        }

        private IEnumerator LoadScreen(string s) 
        {
            if (s != "Title") 
            {
                StartCoroutine(sceneFader.UI_Fade());
                yield return new WaitForSeconds(1.0f);
            }
            else if (titleBgm) 
            {
                StartCoroutine(sceneFader.UI_Fade());
                yield return new WaitForSeconds(1.0f);
            }
            
            if (s == "Title") 
            {
                if (!titleBgm) 
                {
                    titleBgmManager.OnStartBGM();
                    titleBgm = true;
                }
                ActiveScreen(true, false, false, false, false, false, false, false, false, false);
                options.SetActive(false);
                resetMap = true;
                mapGenerator.regeneration = true;
                ActiveRegenerationButton(true);
                
            }
            else if (s == "Map") 
            {
                ActiveScreen(false, true, false, false, false, false, false, false, false, false);
                if (titleBgm) 
                {
                    mainBgmManager.OnStartBGM();
                    titleBgm = false;
                }
                mainBgmManager.OnStartBGM();
                if (resetMap) 
                {
                    playerManager.GameStart();
                    mysteryManager.Initialization();
                    resetMap = false;
                    player.SetPlayer();
                    yield return new WaitForSeconds(0.5f);
                    mapGenerator.ShowMap();
                }
            }
            else if (s == "Enemy") 
            {
                ActiveScreen(false, false, true, false, false, false, false, false, false, false);
                battleManager.StartEnemyFight();
            }
            else if (s == "Elite") 
            {
                ActiveScreen(false, false, true, false, false, false, false, false, false, false);
                battleManager.StartEliteFight();
            }
            else if (s == "Boss") 
            {
                ActiveScreen(false, false, true, false, false, false, false, false, false, false);
                battleManager.StartBossFight();
            }
            else if (s == "Reward") 
            {
                ActiveScreen(false, false, false, true, false, false, false, false, false, false);
            }
            else if (s == "Mystery") 
            {
                ActiveScreen(false, false, false, false, true, false, false, false, false, false);
                mysteryManager.SetEpisode();
            }
            else if (s == "Store") 
            {
                ActiveScreen(false, false, false, false, false, true, false, false, false, false);
            }
            else if (s == "Treasure") 
            {
                ActiveScreen(false, false, false, false, false, false, true, false, false, false);
            }
            else if (s == "Rest") 
            {
                ActiveScreen(false, false, false, false, false, false, false, true, false, false);
            }
            else if (s == "GameOver") 
            {
                ActiveScreen(false, false, false, false, false, false, false, false, true, false);
                if (!titleBgm) 
                {
                    titleBgmManager.OnStartBGM();
                    titleBgm = true;
                }
            }
            else if (s == "GameClear") 
            {
                ActiveScreen(false, false, false, false, false, false, false, false, false, true);
                if (!titleBgm) 
                {
                    titleBgmManager.OnStartBGM();
                    titleBgm = true;
                }
            }
        }

        private void ActiveScreen(bool title, bool map, bool battle, 
            bool reward, bool mystery, bool store, bool treasure, bool rest, 
            bool gameOver, bool gameClear) 
        {
            titleScreen.SetActive(title);
            mapScreen.SetActive(map);
            battleScreen.SetActive(battle);
            rewardScreen.SetActive(reward);
            mysteryScreen.SetActive(mystery);
            storeScreen.SetActive(store);
            treasureScreen.SetActive(treasure);
            restScreen.SetActive(rest);
            gameOverScreen.SetActive(gameOver);
            gameClearScreen.SetActive(gameClear);
        }

        public void ActiveRegenerationButton(bool re) 
        {
            regenerationButton.SetActive(re);
        }

        public IEnumerator SceneFade() 
        {
            StartCoroutine(sceneFader.UI_Fade());
            yield return new WaitForSeconds(1.0f);
        }
    }
}