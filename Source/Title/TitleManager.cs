using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    public class TitleManager : MonoBehaviour
    {
        private ScreenManager screenManager;

        private void Awake() 
        {
            screenManager = FindObjectOfType<ScreenManager>();
        }

        public void ClickTitle() 
        {
            screenManager.SelectScreen("Map");
        }
    }
}