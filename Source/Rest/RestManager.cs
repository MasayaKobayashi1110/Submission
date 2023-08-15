using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    public class RestManager : MonoBehaviour
    {
        private ScreenManager screenManager;
        public Fighter player;
        int amount;

        private void Awake() 
        {
            screenManager = FindObjectOfType<ScreenManager>();
        }

        public void Rest() 
        {
            amount = player.maxHealth / 4;
            player.GetHealth(amount);
            GoToMap();
        }

        public void GoToMap() 
        {
            screenManager.SelectScreen("Map");
        }
    }
}