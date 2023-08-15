using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class RewardCardButton : MonoBehaviour, IPointerDownHandler
    {
        private RewardManager rewardManager;
        private ScreenManager screenManager;
        [SerializeField] private GameObject cardRewardContainer;
        [SerializeField] private int number;
        [SerializeField] private bool handle;
        [SerializeField] private bool skip;

        private void Awake() 
        {
            rewardManager = FindObjectOfType<RewardManager>();
            screenManager = FindObjectOfType<ScreenManager>();
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            if (skip) 
            {
                cardRewardContainer.SetActive(false);
                screenManager.SelectScreen("Map");
                return;
            }

            if (handle == false) 
            {
                rewardManager.SelectedCard(number);
                screenManager.SelectScreen("Map");
            }
            else 
                rewardManager.HandleCards();
        }
    }
}