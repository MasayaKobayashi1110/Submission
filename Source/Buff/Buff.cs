using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MK
{
    [System.Serializable]
    public struct Buff
    {
        public enum Type {
            rage, // 攻撃力上昇（定数）怒り（レイジ）1ターンのみ
            block, // 守備力上昇（定数）ブロック 1ターンのみ
            weakness, // 与ダメージ低下（*0.75f）弱体 ターン減少-1
            vulnerable, // 被ダメージ増加（*1.5f）脆弱 ターン減少-1
        }
        public Type type; // バフの種類
        public Sprite buffIcon; // バフの画像
        [Range(0,999)] public int buffValue; // バフの効果量
    }
}