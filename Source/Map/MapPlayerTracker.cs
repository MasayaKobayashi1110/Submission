using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK;

namespace MK_Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public static MapPlayerTracker Instance;

        private MapGenerator mapGenerator;
        private ScreenManager screenManager;

        private void Awake() 
        {
            Instance = this;
            mapGenerator = GetComponent<MapGenerator>();
            screenManager = FindObjectOfType<ScreenManager>();
        }

        public void SelectNode(Node n) 
        {
            if (n.state == NodeStates.Locked) return;
            if (n.state == NodeStates.Visited) return;
            if (n.state == NodeStates.Attainable) 
            {
                SetVisitedNode(n);
                SetAttainableNodes(n);
                EnterNode(n);
            }
        }

        private void EnterNode(Node n) 
        {
            if (n.type == NodeType.Entrance) 
            {
                mapGenerator.regeneration = false;
                screenManager.ActiveRegenerationButton(false);
                return;
            }
            else if (n.type == NodeType.Enemy) 
            {
                screenManager.SelectScreen("Enemy");
                return;
            }
            else if (n.type == NodeType.Elite) 
            {
                screenManager.SelectScreen("Elite");
                return;
            }
            else if (n.type == NodeType.Boss) 
            {
                screenManager.SelectScreen("Boss");
                return;
            }
            else if (n.type == NodeType.Mystery) 
            {
                screenManager.SelectScreen("Mystery");
                return;
            }
            else if (n.type == NodeType.Treasure) 
            {
                screenManager.SelectScreen("Treasure");
                return;
            }
            else if (n.type == NodeType.Store) 
            {
                screenManager.SelectScreen("Store");
                return;
            }
            else if (n.type == NodeType.Rest) 
            {
                screenManager.SelectScreen("Rest");
                return;
            }
        }

        private void SetVisitedNode(Node n) 
        {
            n.state = NodeStates.Visited;
            n.SetState(n.state);
        }

        private void SetAttainableNodes(Node n) 
        {
            mapGenerator.SetAttainableNodes(n);
        }
    } 
}