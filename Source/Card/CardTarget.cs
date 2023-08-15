using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    /// <summary>
    /// カードをプレイする
    /// </summary>
    public class CardTarget : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject speech;

        private BattleManager battleManager;

        private void Awake() 
        {
            battleManager = FindObjectOfType<BattleManager>();
            speech.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            battleManager.PlayCard();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            speech.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            speech.SetActive(false);
        }
    }
}