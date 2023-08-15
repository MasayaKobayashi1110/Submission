using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// レリックの情報
    /// </summary>
    [CreateAssetMenu(menuName = "Relic/Relic")]
    public class Relic : ScriptableObject
    {
        [Header("レリックの情報")]
        [Tooltip("名前")]
        [SerializeField] private string title;
        public string Title { get { return title; } }

        [Tooltip("説明")]
        [SerializeField] private string description;
        public string Description { get { return description; } }

        [Tooltip("画像")]
        [SerializeField] private Sprite visual;
        public Sprite Visual { get { return visual; } }

        [Header("レリックタイプ")]
        [SerializeField] private RelicType type;
        public RelicType Type { get { return type; } }
        public enum RelicType 
        {
            Common, // 一般的
            Rare, // ボス限定
            Curse, // 呪い（デメリット）
            Character, // キャラクター限定
        };
    }
}