using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace MK 
{
    /// <summary>
    /// カードの表示を管理する
    /// </summary>
    public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public Card card;

        [Header("縮小")]
        [SerializeField] private GameObject miniCard;
        [SerializeField] private TMP_Text titleTextMini;
        [SerializeField] private TMP_Text costTextMini;
        [SerializeField] private TMP_Text amountTextMini;
        [SerializeField] private SpriteRenderer srMini;

        [Header("拡大")]
        [SerializeField] private GameObject detailCard;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private TMP_Text amountText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private SpriteRenderer sr;

        //[Header("解説表示")]
        //[SerializeField] private GameObject speech;

        private Animator animator;
        private bool selected;

        private BattleManager battleManager;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            selected = false;
            battleManager = FindObjectOfType<BattleManager>();
        }

        public void LoadCard(Card c)
        {
            card = c;
            // 縮小
            titleTextMini.text = card.Title;
            costTextMini.text = card.Cost.ToString();
            srMini.sprite = card.MiniVisual;

            // 拡大
            titleText.text = card.Title;
            costText.text = card.Cost.ToString();
            descriptionText.text = card.Description;
            sr.sprite = card.DetailVisual;

            animator.SetBool("Selected", false);
            selected = false;
            //gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (card.IsPlayable == false) 
                return; 
            if (!selected && card.Cost <= battleManager.currentEnergy) 
            {
                animator.SetBool("Selected", true);
                selected = true;
                battleManager.SelectCard(this);
            }
            else if (selected)
            {
                animator.SetBool("Selected", false);
                selected = false;
                battleManager.DeselectCard(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            detailCard.SetActive(true);
            miniCard.SetActive(false);
            //speech.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            miniCard.SetActive(true);
            detailCard.SetActive(false);
            //speech.SetActive(false);
        }
    }
}