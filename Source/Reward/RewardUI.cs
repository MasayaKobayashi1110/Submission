using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK 
{
    /// <summary>
    /// リワードの表示
    /// </summary>
    public class RewardUI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text cost;
        [SerializeField] private TMP_Text title;

        public void DisplayCoin(int amount) 
        {
            description.text = $"{amount}コイン";
        }

        public void DisplayRelic(Relic r) 
        {
            sr.sprite = r.Visual;
            description.text = $"{r.Title}:{r.Description}";
        }

        public void DisplayCard(Card c) 
        {
            sr.sprite = c.DetailVisual;
            description.text = c.Description;
            cost.text = c.Cost.ToString();
            title.text = c.Title;
        }
    }
}