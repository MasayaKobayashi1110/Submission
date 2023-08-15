using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK 
{
    /// <summary>
    /// ファイターのUIの管理
    /// </summary>
    public class FighterUI : MonoBehaviour
    {
        [Tooltip("名前")]
        [SerializeField] private TMP_Text title;
        [Tooltip("体力")]
        [SerializeField] private TMP_Text healthAmount;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private SpriteRenderer sr;
        [Tooltip("バフデバフ")]
        [SerializeField] private List<BuffUI> buffUIs = new List<BuffUI>(4);

        // 体力の表示
        public void DisplayHealth(int amount) 
        {
            healthAmount.text = $"{amount}/{healthSlider.maxValue}";
            healthSlider.value = amount;
        }

        public void DisplayMaxHealth(int amount) 
        {
            healthSlider.maxValue += amount;
        }

        // 最大体力の設定
        public void SetMaxHealth(int amount) 
        {
            healthSlider.maxValue = amount;
        }

        // 名前の表示
        public void SetFighterName(string fighterName) 
        {
            title.text = fighterName;
        }

        // 画像の表示
        public void SetFighterVisual(Sprite s) 
        {
            sr.sprite = s;
        }

        // バフデバフUIのリセット
        public void ResetBuffUI() 
        {
            foreach (BuffUI buffUI in buffUIs) 
            {
                buffUI.gameObject.SetActive(false);
            }
        }

        // バフデバフUIのセット
        public void SetBuffUI(Buff b)
        {
            for (int i = 0; i < buffUIs.Count; i++) 
            {
                if (buffUIs[i].buffType == b.type) 
                {
                    if (buffUIs[i].gameObject.activeSelf == false) 
                    {
                        buffUIs[i].gameObject.SetActive(true);
                        buffUIs[i].DisplayBuff(b);
                    }

                    if (buffUIs[i].gameObject.activeSelf && b.buffValue == 0) 
                    {
                        buffUIs[i].gameObject.SetActive(false);
                    }
                        
                    buffUIs[i].UpdateAmountText(b);
                    break;
                }
            }
        }

    }
}