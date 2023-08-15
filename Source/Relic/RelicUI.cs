using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace MK 
{
    public class RelicUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Relic relic;
        [SerializeField] private SpriteRenderer relicVisual;
        [SerializeField] private GameObject speech;
        [SerializeField] private TMP_Text description;

        private void Awake() 
        {
            //description = speech.GetComponentInChildren<TMP_Text>(true);
            speech.SetActive(false);
        }

        public void LoadRelic(Relic r) 
        {
            relic = r;
            relicVisual.sprite = relic.Visual;
            description.text = relic.Title + " : \n" + relic.Description;
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