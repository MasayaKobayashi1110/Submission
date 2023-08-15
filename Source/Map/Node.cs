using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK_Map 
{
    public enum NodeStates
    {
        Locked, // ロック状態
        Visited, // 到達済み
        Attainable, // 進行可能
    }

    public enum NodeType 
    {
        Entrance, // スタート地点
        Enemy, // 敵
        Elite, // 強敵
        Boss, // ボス
        Mystery, // イベント
        Treasure, // 宝
        Store, // お店
        Rest, // 休憩
    }

    public class Node : MonoBehaviour, IPointerDownHandler
    {
        public Point point;

        public int direction;
        public GameObject top;
        public GameObject bottom;
        public GameObject left;
        public GameObject right;

        public GameObject locked;
        public SpriteRenderer sr;
        public Color32 visitedColor = Color.gray;

        public NodeStates state;

        public NodeType type;
        public SpriteRenderer typeSr;
        public List<Sprite> typeSprite = new List<Sprite>(8);

        public void SetUp(int dir, bool start) 
        {
            direction = dir;
            int d = direction;

            if (d - 8 > -1) {
                right.SetActive(true);
                d -= 8;
            } else {
                right.SetActive(false);
            }

            if (d - 4 > -1) {
                left.SetActive(true);
                d -= 4;
            } else {
                left.SetActive(false);
            }

            if (d - 2 > -1) {
                bottom.SetActive(true);
                d -= 2;
            } else {
                bottom.SetActive(false);
            }

            if (d - 1 > -1) {
                top.SetActive(true);
                d -= 1;
            } else {
                top.SetActive(false);
            }

            // 初期はロック状態
            state = NodeStates.Locked;
            SetState(state);

            if (start) 
            {
                state = NodeStates.Attainable;
                SetState(state);
            }
        }

        public void SetState(NodeStates state) 
        {
            switch (state) 
            {
                case NodeStates.Locked: // ロック
                    locked.SetActive(true);
                    break;
                case NodeStates.Visited: // 到達済み
                    sr.color = visitedColor;
                    break;
                case NodeStates.Attainable: // 到達可能
                    locked.SetActive(false);
                    break;
            }
        }

        public void SetUpNodeType(NodeType t) 
        {
            type = t;
            if (type == NodeType.Entrance) 
                typeSr.sprite = typeSprite[0];
            if (type == NodeType.Enemy) 
                typeSr.sprite = typeSprite[1];
            if (type == NodeType.Elite) 
                typeSr.sprite = typeSprite[2];
            if (type == NodeType.Boss) 
                typeSr.sprite = typeSprite[3];
            if (type == NodeType.Mystery) 
                typeSr.sprite = typeSprite[4];
            if (type == NodeType.Treasure) 
                typeSr.sprite = typeSprite[5];
            if (type == NodeType.Store) 
                typeSr.sprite = typeSprite[6];
            if (type == NodeType.Rest) 
                typeSr.sprite = typeSprite[7];
        }

        public void OnPointerDown(PointerEventData data) 
        {
            MapPlayerTracker.Instance.SelectNode(this);
        }
    } 
}