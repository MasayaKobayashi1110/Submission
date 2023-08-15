using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class TurnEndButton : MonoBehaviour, IPointerDownHandler
    {
        private BattleManager battleManager;

        private void Awake() 
        {
            battleManager = FindObjectOfType<BattleManager>();
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            battleManager.ChangeTurn();
        }
    }
}