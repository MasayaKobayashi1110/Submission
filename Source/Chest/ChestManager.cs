using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    public class ChestManager : MonoBehaviour
    {
        private ScreenManager screenManager;
        private RelicManager relicManager;
        private PlayerStatsUI playerStatsUI;
        private PlayerManager playerManager;

        private void Awake() 
        {
            screenManager = FindObjectOfType<ScreenManager>();
            relicManager = FindObjectOfType<RelicManager>();
            playerStatsUI = FindObjectOfType<PlayerStatsUI>();
            playerManager = FindObjectOfType<PlayerManager>();
        }

        public void ClickChest() 
        {
            Relic r = relicManager.GetRandomRelic();
            relicManager.SetRelic(r);
            playerStatsUI.DisplayRelics(r);
            playerManager.CheckRelics(r);
            screenManager.SelectScreen("Map");
        }
    }
}