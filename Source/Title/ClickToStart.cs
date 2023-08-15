using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class ClickToStart : MonoBehaviour, IPointerDownHandler
    {
        private TitleManager titleManager;

        private void Awake() 
        {
            titleManager = FindObjectOfType<TitleManager>();
        }

        public void OnPointerDown(PointerEventData data) 
        {
            titleManager.ClickTitle();
        }
    }
}