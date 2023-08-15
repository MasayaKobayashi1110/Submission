using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// Mysteryのエピソード
    /// </summary>
    [CreateAssetMenu (menuName = "Mystery/Episode")]
    public class Episode : ScriptableObject
    {
        [Tooltip("タイトル")]
        [SerializeField] private string title;
        public string Title { get { return title; } }

        [Tooltip("テキストファイル")]
        [SerializeField] private TextAsset textFile;
        public TextAsset TextFile { get { return textFile; } }

        [Tooltip("選択肢の数")]
        [SerializeField]
        private int numChoices;
        public int NumChoices { get { return numChoices; } }

        [Tooltip("選択肢の答え")]
        [SerializeField]
        private string choice1, choice2, choice3;
        public string Choice1 { get { return choice1; } }
        public string Choice2 { get { return choice2; } }
        public string Choice3 { get { return choice3; } }
    }
}