using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK 
{
    /// <summary>
    /// エネミーのUIの表示を管理する
    /// </summary>
    public class EnemyUI : MonoBehaviour
    {
        public Enemy enemy;

        [Tooltip("名前")]
        [SerializeField] private TMP_Text title;
        [Tooltip("体力")]
        [SerializeField] private TMP_Text healthAmount;
    }
}