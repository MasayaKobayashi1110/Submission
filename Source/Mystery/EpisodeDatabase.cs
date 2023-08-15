using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// エピソードのデータベース
    /// </summary>
    [CreateAssetMenu (menuName = "Mystery/EpisodeDatabase")]
    public class EpisodeDatabase : ScriptableObject
    {
        [SerializeField] private List<Episode> episodeList = new List<Episode>(10);

        // データの一覧をリストで返す関数
        public List<Episode> GetEpisodeList() 
        {
            return episodeList;
        }

        // タイトルを指定してそのエピソードを返す関数
        public Episode GetEpisode(string title) 
        {
            Episode ep = episodeList.Find(data => data.Title == title);
            return ep;
        }

        // タイトルを指定してそのエピソードのtextFileを返す関数
        public TextAsset GetText(string title) 
        {
            Episode ep = episodeList.Find(data => data.Title == title);
            return ep.TextFile;
        }

        // タイトルを指定してそのエピソードのnumChoicesを返す関数
        public int GetNum(string title) 
        {
            Episode ep = episodeList.Find(data => data.Title == title);
            return ep.NumChoices;
        }

        // タイトルを指定してそのエピソードのchoiceを返す関数
        public string GetChoice(string title, int num) 
        {
            Episode ep = episodeList.Find(data => data.Title == title);
            if (num == 1) return ep.Choice1;
            else if (num == 2) return ep.Choice2;
            else if (num == 3) return ep.Choice3;
            return null;
        }
    }
}