using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class ClickToTitle : MonoBehaviour, IPointerDownHandler
    {
        private GameManager gameManager;

        private void Awake() 
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        public void OnPointerDown(PointerEventData data) 
        {
            gameManager.GoToTitle();
        }
    }
}