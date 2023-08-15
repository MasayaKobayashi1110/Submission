using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    [System.Serializable]
    public class EnemyAction 
    {
        public IntentType intentType;
        public enum IntentType {
            Attack,
            Block,
            Buff,
            Debuff,
        };

        public int attackAmount;
        public int blockAmount;
        public int buffAmount;
        public int debuffAmount;
        public Buff.Type buffType;
        public int chance;
        public Sprite actionIcon;
    }
}