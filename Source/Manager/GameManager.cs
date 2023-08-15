using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    public class GameManager : MonoBehaviour
    {
        private ScreenManager screenManager;

        private void Awake() 
        {
            screenManager = GetComponent<ScreenManager>();
        }

        private void Start() 
        {
            GoToTitle();
        }

        public void GoToTitle() 
        {
            screenManager.SelectScreen("Title");
        }
    }
}