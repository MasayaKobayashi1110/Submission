using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class ClickOptions : MonoBehaviour, IPointerDownHandler
    {
        public GameObject optionScreen;

        private bool open = false;

        public void OnPointerDown(PointerEventData data) 
        {
            if (!open) 
            {
                open = true;
                optionScreen.SetActive(true);
            }
            else 
            {
                open = false;
                optionScreen.SetActive(false);
            }
        }
    }
}
