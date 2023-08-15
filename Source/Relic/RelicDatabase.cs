using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// レリックのデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "Relic/RelicDatabase")]
    public class RelicDatabase : ScriptableObject
    {
        // レリックデータを格納する(初期レリック種類 : 20)
        [SerializeField]
        private List<Relic> relicList = new List<Relic>(20);

        // レリックデータの一覧を返す関数
        public List<Relic> GetRelicList() 
        {
            return relicList;
        }

        // 名前を指定してそのレリックを返す関数
        public Relic GetRelic(string title) 
        {
            Relic r = relicList.Find(data => data.Title == title);
            return r;
        }

        // 名前を指定してそのレリックのdescriptionを返す関数
        public string GetDescription(string title) 
        {
            Relic r = relicList.Find(data => data.Title == title);
            return r.Description;
        }

        // 名前を指定してそのレリックのvisualを返す関数
        public Sprite GetSprite(string title) 
        {
            Relic r = relicList.Find(data => data.Title == title);
            return r.Visual;
        }

        // 名前を指定してそのレリックのtypeを返す関数
        public Relic.RelicType GetType(string title) 
        {
            Relic r = relicList.Find(data => data.Title == title);
            return r.Type;
        }
    }
}