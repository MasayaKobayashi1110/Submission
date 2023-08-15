using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class ClickChoice : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private int num;

        private MysteryManager mysteryManager;

        private void Awake() 
        {
            mysteryManager = FindObjectOfType<MysteryManager>();
        }

        public void OnPointerDown(PointerEventData data) 
        {
            mysteryManager.PushButton(num);
        }
    }
}