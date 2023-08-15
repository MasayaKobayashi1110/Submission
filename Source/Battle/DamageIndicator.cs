using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MK 
{
    public class DamageIndicator : MonoBehaviour
    {
        public Animator animator;
        public TMP_Text text;
        public void DisplayDamage(int amount) 
        {
            text.text = amount.ToString();
            animator.SetTrigger("DisplayDamage");
        }
    }
}