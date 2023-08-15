using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// レリックを管理する
    /// </summary>
    public class RelicManager : MonoBehaviour
    {
        // レリックのデータベース
        [SerializeField]
        private RelicDatabase relicDatabase;

        // プレイヤーのレリック(レリック所有数 : 20)
        private List<Relic> playerRelic = new List<Relic>(20);
        public List<Relic> PlayerRelic { get { return playerRelic; } }
        // コモンレリックのリスト
        private List<Relic> commonRelicList = new List<Relic>(10);
        // レアレリックのリスト
        private List<Relic> rareRelicList = new List<Relic>(5);
        // カースレリックのリスト
        private List<Relic> curseRelicList = new List<Relic>(4);
        // キャラクターレリックのリスト
        private List<Relic> characterRelicList = new List<Relic>(1);

        private List<Relic> gotRelicList = new List<Relic>(20);

        private int random;

        // 初期化
        private void Awake() 
        {
            ClearAllRelicList();
            SetTypeRelicList();
        }

        public void Initialization() 
        {
            ClearAllRelicList();
            SetTypeRelicList();
        }

        // 全てのリストを空にする
        private void ClearAllRelicList() 
        {
            playerRelic.Clear();
            commonRelicList.Clear();
            rareRelicList.Clear();
            curseRelicList.Clear();
            characterRelicList.Clear();
            gotRelicList.Clear();
        }

        // レリックのクラスでリスト分けをする
        private void SetTypeRelicList() 
        {
            for (int i = 0; i < relicDatabase.GetRelicList().Count; i++) 
            {
                Relic r = relicDatabase.GetRelicList()[i];
                if (r.Type == Relic.RelicType.Common)
                    commonRelicList.Add(r);
                else if (r.Type == Relic.RelicType.Rare)
                    rareRelicList.Add(r);
                else if (r.Type == Relic.RelicType.Curse)
                    curseRelicList.Add(r);
                else if (r.Type == Relic.RelicType.Character)
                    characterRelicList.Add(r);
            }
        }

        // キャラクターの初期レリックをセットする
        public void SetStartingRelic(Character character)
        {
            playerRelic.Add(character.StartingRelic);
        }

        public void SetRandomRelic() 
        {
            random = Random.Range(0, commonRelicList.Count - 1);
            playerRelic.Add(commonRelicList[random]);
        }

        public Relic GetRandomRelic() 
        {
            random = Random.Range(0, commonRelicList.Count - 1);
            Relic r = commonRelicList[random];
            commonRelicList.Remove(commonRelicList[random]);
            return r;
        }

        public void SetRelic(Relic r) 
        {
            playerRelic.Add(r);
        }

        public Relic GetSelectRelic(string type, string name) 
        {
            if (type == "Rare") 
            {
                Relic r = rareRelicList.Find(data => data.Title == name); 
                rareRelicList.Remove(r);
                return r;
            }
            else if (type == "Curse")
            {
                Relic r = curseRelicList.Find(data => data.Title == name);
                curseRelicList.Remove(r);
                return r;
            }
            return null;
        }

        /*
        public List<Relic> GetPlayerRelic()
        {
            return playerRelic;
        }

        // リストのサイズを返す関数
        public int GetCountRelicList(Relic.RelicClass relicClass) 
        {
            int value = 0;
            if (relicClass == Relic.RelicClass.Common)
                value = commonRelicList.Count;
            else if (relicClass == Relic.RelicClass.Rare)
                value = rareRelicList.Count;
            else if (relicClass == Relic.RelicClass.Curse) 
                value = curseRelicList.Count;
            else if (relicClass == Relic.RelicClass.Character)
                value = characterRelicList.Count;
            return value;
        }

        // 指定されたクラスのレリックをランダムに返す関数
        public Relic GetRandomRelic(Relic.RelicClass relicClass) 
        {
            Relic r = null;
            if (GetCountRelicList(relicClass) == 0) return r;
            int value = Random.Range(0, GetCountRelicList(relicClass) - 1);
            
            if (relicClass == Relic.RelicClass.Common) 
            {
                r = commonRelicList[value];
                commonRelicList.RemoveAt(value);
            }
            else if (relicClass == Relic.RelicClass.Rare)
            {
                r = rareRelicList[value];
                rareRelicList.RemoveAt(value);
            }
            else if (relicClass == Relic.RelicClass.Curse)
            {
                r = curseRelicList[value];
                curseRelicList.RemoveAt(value);
            }
            else if (relicClass == Relic.RelicClass.Character)
            {
                r = characterRelicList[value];
                characterRelicList.RemoveAt(value);
            }
            return r;
        }
        */

    }
}