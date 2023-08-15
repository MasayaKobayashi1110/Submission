using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private GameObject TutorialScreen;

        public bool tutorial;

        private void Awake() 
        {
            tutorial = false;
            OffTutorial();
        }

        public void OnTutorial() 
        {
            if (tutorial == false) 
            {
                TutorialScreen.SetActive(true);
                tutorial = true;
            }
        }

        public void OffTutorial() 
        {
            TutorialScreen.SetActive(false);
        }
    }
}

