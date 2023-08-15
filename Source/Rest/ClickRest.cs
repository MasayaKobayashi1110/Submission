using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class ClickRest : MonoBehaviour, IPointerDownHandler
    {
        private RestManager restManager;
        public bool rest;

        private void Awake() 
        {
            restManager = FindObjectOfType<RestManager>();
        }

        public void OnPointerDown(PointerEventData data) 
        {
            if (rest) 
                restManager.Rest();
            else 
                restManager.GoToMap();
        }
    }
}