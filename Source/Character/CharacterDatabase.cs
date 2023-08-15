using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// キャラクターのデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "Character/CharacterDatabase")]
    public class CharacterDatabase : ScriptableObject
    {
        // キャラクターデータを格納する
        [SerializeField]
        private List<Character> characterList = new List<Character>(1);

        // キャラクターデータの一覧を返す関数
        public List<Character> GetCharacterList() 
        {
            return characterList;
        }

        // タイプを指定してそのキャラクターを返す関数
        public Character GetCharacter(Character.CharacterType ct) 
        {
            Character c = characterList.Find(data => data.Type == ct);
            return c;
        }
    }
}