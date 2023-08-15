using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class CloseTutorial : MonoBehaviour, IPointerDownHandler
    {
        private TutorialManager tutorialManager;

        private void Awake() 
        {
            tutorialManager = FindObjectOfType<TutorialManager>();
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            tutorialManager.OffTutorial();
        }
    }
}