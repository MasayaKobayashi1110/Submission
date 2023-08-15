using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK 
{
    public class PlayerStatsUI : MonoBehaviour
    {
        public List<RelicUI> relicUIs = new List<RelicUI>(16);
        private RelicManager relicManager;

        [SerializeField] private TMP_Text coinText;
        public int coinAmount;

        private void Awake()
        {
            relicManager = FindObjectOfType<RelicManager>();
            coinAmount = 0;
            UpdateCoins();
        }

        public void Initialization() 
        {
            coinAmount = 0;
            UpdateCoins();
            foreach (RelicUI r in relicUIs) 
                r.gameObject.SetActive(false);
        }

        public void DisplayRelics(Relic r)
        {
            RelicUI relicUI = relicUIs[relicManager.PlayerRelic.Count - 1];
            relicUI.LoadRelic(r);
            relicUI.gameObject.SetActive(true);
        }

        private void UpdateCoins() 
        {
            coinText.text = coinAmount.ToString();
        }

        public void SetCoins(int amount) 
        {
            coinAmount += amount;
            UpdateCoins();
        }
    }
}