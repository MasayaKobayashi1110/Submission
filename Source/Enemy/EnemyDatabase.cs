using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// エネミーのデータベース
    /// </summary>
    [CreateAssetMenu (menuName = "Enemy/EnemyDatabase")]
    public class EnemyDatabase : ScriptableObject
    {
        // エネミーの情報を格納する
        [SerializeField] 
        private List<Enemy> enemyList = new List<Enemy>(7);

        public List<Enemy> GetEnemyList() 
        {
            return enemyList;
        }
    }
}