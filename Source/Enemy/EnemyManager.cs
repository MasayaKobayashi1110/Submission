using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// エネミーの管理をする
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] 
        private EnemyDatabase enemyDatabase;

        public List<Enemy> enemyList = new List<Enemy>(5);
        public List<Enemy> eliteList = new List<Enemy>(1);
        public List<Enemy> bossList = new List<Enemy>(1);

        private void Awake() 
        {
            ClearList();
            SetTypeEnemyList();
        }

        // リストを空にする
        private void ClearList() 
        {
            enemyList.Clear();
            eliteList.Clear();
            bossList.Clear();
        }

        public void SetTypeEnemyList() 
        {
            for (int i = 0; i < enemyDatabase.GetEnemyList().Count; i++) 
            {
                Enemy e = enemyDatabase.GetEnemyList()[i];
                if (e.IsBoss) 
                    bossList.Add(e);
                else if (e.IsElite)
                    eliteList.Add(e);
                else 
                    enemyList.Add(e);
            }
        }
    }
}