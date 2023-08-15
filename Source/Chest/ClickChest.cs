using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class ClickChest : MonoBehaviour, IPointerDownHandler
    {
        private ChestManager chestManager;

        private void Awake() 
        {
            chestManager = FindObjectOfType<ChestManager>();
        }

        public void OnPointerDown(PointerEventData data) 
        {
            chestManager.ClickChest();
        }
    }
}