using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace MK
{
    /// <summary>
    /// バフデバフのUIの表示
    /// </summary>
    public class BuffUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private TMP_Text buffAmountText;
        [SerializeField] private GameObject speech;
        [SerializeField] private TMP_Text description;

        public Buff.Type buffType;

        public void DisplayBuff(Buff b) 
        {
            sr.sprite = b.buffIcon;
            SetDescription();
        }

        private void SetDescription() 
        {
            if (buffType == Buff.Type.rage) 
                description.text = "レイジ : 1ターンの間、与ダメージを増加させる";
            if (buffType == Buff.Type.block) 
                description.text = "ブロック : 1ターンの間、被ダメージを減少させる";
            if (buffType == Buff.Type.vulnerable) 
                description.text = "脆弱 : 値のターンの間、被ダメージを増加させる";
            if (buffType == Buff.Type.weakness) 
                description.text = "弱体 : 値のターンの間、与ダメージを減少させる";
        }

        public void UpdateAmountText(Buff b) 
        {
            buffAmountText.text = b.buffValue.ToString();
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