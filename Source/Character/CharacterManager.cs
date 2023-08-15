using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// キャラクターを管理する
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        // キャラクターのデータベース
        [SerializeField]
        private CharacterDatabase characterDatabase;

        // 選択しているキャラクター
        private Character selectedCharacter;
        public Character SelectedCharacter { get { return selectedCharacter; } }

        // 初期化
        /*
        private void Awake() 
        {
            ClearSelectedCharacter();
        }
        */

        // selecterdCharacterを空にする
        private void ClearSelectedCharacter() 
        {
            if (selectedCharacter != null) 
                selectedCharacter = null;
        }

        // キャラクターを選択する関数
        public void SetSelectedCharacter(Character.CharacterType ct) 
        {
            selectedCharacter = characterDatabase.GetCharacter(ct); 
        }
    }
}