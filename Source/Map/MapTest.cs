using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK_Map 
{
    public class MapTest : MonoBehaviour
    {
        public GameObject nodePrefab; // ノードのプレハブを格納する
        public Transform mapLayer; 
        [Range(1, 7)]
        public int width = 5;
        [Range(1, 7)]
        public int height = 5;
        [Range(1, 6)]
        public int size = 5;
        [Range(1, 49)]
        public int threshold = 15;

        private List<Point> points = new List<Point>(49);
        private List<Node> mapNodes = new List<Node>(49);

        private int leftTopX;
        private int leftTopY;

        public bool regeneration = true;
        public bool r = false;

        private void Update() 
        {
            if (Input.GetKey(KeyCode.A)) 
                ShowMap();
            if (Input.GetKey(KeyCode.S))
            {
                ShowMap();
                r = true;
            }
        }

        public void ShowMap() 
        {
            if (regeneration == false) return;
            
            Debug.Log("ShowMap");
            Initialization();
            //Cursor.lockState = CursorLockMode.Locked;
            CalculateLeftTopPoint();
            CreatePoint();
            /*
            CreateBackgroundLeftPoint();
            if (r == true) 
                CreateBackground();
                */
            if ( r == false)
            {
                StartCoroutine("CreateFirstNode");
                StartCoroutine(CreateNode(0));
            }
                
            //CreateFirstNode();
            //StartCoroutine(CreateNode(0));
            //CreateNode(0);
            //CreateMapNodeType();
        }

        private void Initialization() 
        {
            points.Clear();

            foreach (Node nodes in mapNodes) 
                Destroy(nodes.gameObject);
            
            mapNodes.Clear();
        }
        
        private void CalculateLeftTopPoint() 
        {
            if (width % 2 == 0) 
                leftTopX = (width / 2 * size - 2) * -1;
            if (height % 2 == 0)
                leftTopY = height / 2 * size - 2;
            if (width % 2 != 0) 
                leftTopX = width / 2 * size * -1;
            if (height % 2 != 0) 
                leftTopY = height / 2 * size;
        }
        
        private void CreatePoint() 
        {
            for (int i = 0; i < height; i++) 
            {
                for (int j = 0; j < width; j++) 
                {
                    Point p = new Point(leftTopX + size * j, leftTopY - size * i);
                    points.Add(p);
                }
            }
        }

        private void CreateBackgroundLeftPoint() 
        {
                Instantiate(nodePrefab, new Vector3(points[0].x, points[0].y, 20), Quaternion.identity, mapLayer);
        }

        private void CreateBackground() 
        {
            for (int i = 0; i < points.Count; i++) 
            {
                Instantiate(nodePrefab, new Vector3(points[i].x, points[i].y, 20), Quaternion.identity, mapLayer);
            }
        }

        private IEnumerator CreateFirstNode() 
        {
            yield return new WaitForSeconds(0.2f);

            int fp = Random.Range(0, points.Count - 1);
            //int fp = 0;
            var mapNode = Instantiate(nodePrefab, new Vector3(points[fp].x, points[fp].y, 20), Quaternion.identity, mapLayer);
            var node = mapNode.GetComponent<Node>();

            Point p = new Point(points[fp].x, points[fp].y);
            node.point = p;
            node.SetUp(CheckNeighborPoint(node.point), true);
            //node.SetUp(0, true);
            mapNodes.Add(node);
        }

        private int CheckNeighborPoint(Point p) 
        {
            int direction = 0;
            int pxr = p.x + size;
            int pxl = p.x - size;
            int pyt = p.y + size;
            int pyb = p.y - size;

            Point neighbor = points.Find(data => data.ToString() == $"({p.x}, {pyt})");
            if (neighbor != null)
                direction += 1;
            
            neighbor = points.Find(data => data.ToString() == $"({p.x}, {pyb})");
            if (neighbor != null)
                direction += 2;
            
            neighbor = points.Find(data => data.ToString() == $"({pxl}, {p.y})");
            if (neighbor != null)
                direction += 4;

            neighbor = points.Find(data => data.ToString() == $"({pxr}, {p.y})");
            if (neighbor != null)
                direction += 8;

            return direction;
        }

        private IEnumerator CreateNode(int num) 
        {
            yield return new WaitForSeconds(0.2f);

            int number = num;
            int dir = mapNodes[number].direction;
            int pxr = mapNodes[number].point.x + size;
            int pxl = mapNodes[number].point.x - size;
            int pyt = mapNodes[number].point.y + size;
            int pyb = mapNodes[number].point.y - size;

            if (dir - 8 > -1) {
                Node n = mapNodes.Find(data => data.point.ToString() == $"({pxr}, {mapNodes[number].point.y})");
                if (n == null) 
                {
                    var mapNode = Instantiate(nodePrefab, new Vector3(pxr, mapNodes[number].point.y, 20), Quaternion.identity, mapLayer);
                    var node = mapNode.GetComponent<Node>();
                    Point p = new Point(pxr, mapNodes[number].point.y);
                    node.point = p;
                    node.SetUp(CheckNeighborPointRandom(node.point, mapNodes[number].point), false);
                    mapNodes.Add(node);
                }
                dir -= 8;
            }

            if (dir - 4 > -1) {
                Node n = mapNodes.Find(data => data.point.ToString() == $"({pxl}, {mapNodes[number].point.y})");
                if (n == null) 
                {
                    var mapNode = Instantiate(nodePrefab, new Vector3(pxl, mapNodes[number].point.y, 20), Quaternion.identity, mapLayer);
                    var node = mapNode.GetComponent<Node>();
                    Point p = new Point(pxl, mapNodes[number].point.y);
                    node.point = p;
                    node.SetUp(CheckNeighborPointRandom(node.point, mapNodes[number].point), false);
                    mapNodes.Add(node);
                }
                dir -= 4;
            }

            if (dir - 2 > -1) {
                Node n = mapNodes.Find(data => data.point.ToString() == $"({mapNodes[number].point.x}, {pyb})");
                if (n == null) 
                {
                    var mapNode = Instantiate(nodePrefab, new Vector3(mapNodes[number].point.x, pyb, 20), Quaternion.identity, mapLayer);
                    var node = mapNode.GetComponent<Node>();
                    Point p = new Point(mapNodes[number].point.x, pyb);
                    node.point = p;
                    node.SetUp(CheckNeighborPointRandom(node.point, mapNodes[number].point), false);
                    mapNodes.Add(node);
                }
                dir -= 2;
            }

            if (dir - 1 > -1) {
                Node n = mapNodes.Find(data => data.point.ToString() == $"({mapNodes[number].point.x}, {pyt})");
                if (n == null) 
                {
                    var mapNode = Instantiate(nodePrefab, new Vector3(mapNodes[number].point.x, pyt, 20), Quaternion.identity, mapLayer);
                    var node = mapNode.GetComponent<Node>();
                    Point p = new Point(mapNodes[number].point.x, pyt);
                    node.point = p;
                    node.SetUp(CheckNeighborPointRandom(node.point, mapNodes[number].point), false);
                    mapNodes.Add(node);
                }
                dir -= 1;
            }

            if (number != mapNodes.Count - 1) 
                StartCoroutine(CreateNode(number + 1));
            else 
                CreateMapNodeType();
            
                //CreateNode(number + 1);
        }

        private int CheckNeighborPointRandom(Point p, Point pp) 
        {
            int direction = 0;
            int r = 0;
            int pxr = p.x + size;
            int pxl = p.x - size;
            int pyt = p.y + size;
            int pyb = p.y - size;

            Point neighbor;
            Node neighborNode;

            neighbor = points.Find(data => data.ToString() == $"({p.x}, {pyt})");
            if (neighbor != null)
            {
                if (neighbor.x == pp.x && neighbor.y == pp.y) 
                    direction += 1;
                else 
                {
                    neighborNode = mapNodes.Find(data => data.point.ToString() == $"({p.x}, {pyt})");
                    if (neighborNode != null) 
                    {
                        // もし上Nodeが下directionをもっているなら、このNodeは上を持つ
                        if (CheckDirection(neighborNode, 2))
                            direction += 1;
                    }
                    else 
                    {
                        if (mapNodes.Count < threshold) 
                            direction += 1;
                        else 
                        {
                            r = Random.Range(0, 9);
                            if (r > 4)
                                direction += 1;
                        }
                    }
                }
            }
                
            neighbor = points.Find(data => data.ToString() == $"({p.x}, {pyb})");
            if (neighbor != null)
            {
                if (neighbor.x == pp.x && neighbor.y == pp.y) 
                    direction += 2;
                else 
                {
                    neighborNode = mapNodes.Find(data => data.point.ToString() == $"({p.x}, {pyb})");
                    if (neighborNode != null) 
                    {
                        // もし下Nodeが上directionをもっているなら、このNodeは下を持つ
                        if (CheckDirection(neighborNode, 1))
                            direction += 2;
                    }
                    else 
                    {
                        if (mapNodes.Count < threshold) 
                            direction += 2;
                        else 
                        {
                            r = Random.Range(0, 9);
                            if (r > 4)
                                direction += 2;
                        }
                    }
                }
            }
            
            neighbor = points.Find(data => data.ToString() == $"({pxl}, {p.y})");
            if (neighbor != null)
            {
                if (neighbor.x == pp.x && neighbor.y == pp.y) 
                    direction += 4;
                else 
                {
                    neighborNode = mapNodes.Find(data => data.point.ToString() == $"({pxl}, {p.y})");
                    if (neighborNode != null) 
                    {
                        // もし左Nodeが右directionをもっているなら、このNodeは左を持つ
                        if (CheckDirection(neighborNode, 8))
                            direction += 4;
                    }
                    else 
                    {
                        if (mapNodes.Count < threshold) 
                            direction += 4;
                        else 
                        {
                            r = Random.Range(0, 9);
                            if (r > 4)
                                direction += 4;
                        }
                    }
                }
            }

            neighbor = points.Find(data => data.ToString() == $"({pxr}, {p.y})");
            if (neighbor != null)
            {
                if (neighbor.x == pp.x && neighbor.y == pp.y) 
                    direction += 8;
                else 
                {
                    neighborNode = mapNodes.Find(data => data.point.ToString() == $"({pxr}, {p.y})");
                    if (neighborNode != null) 
                    {
                        // もし右Nodeが左directionをもっているなら、このNodeは右を持つ
                        if (CheckDirection(neighborNode, 4))
                            direction += 8;
                    }
                    else 
                    {
                        if (mapNodes.Count < threshold) 
                            direction += 8;
                        else 
                        {
                            r = Random.Range(0, 9);
                            if (r > 4)
                                direction += 8;
                        }
                    }
                }
            }
            return direction;
        }

        private bool CheckDirection(Node n, int num) 
        {
            // numの仕様
            // num => 1 : nが上方向のdirectionをもつか判断する
            // num => 2 : nが下方向のdirectionをもつか判断する
            // num => 4 : nが左方向のdirectionをもつか判断する
            // num => 8 : nが右方向のdirectionをもつか判断する
            if (n == null) return false;

            int d = n.direction;

            // right
            if (d - 8 > -1) {
                d -= 8;
                if (num == 8) return true;
            }

            // left
            if (d - 4 > -1) {
                d -= 4;
                if (num == 4) return true;
            }

            // bottom
            if (d - 2 > -1) {
                d -= 2;
                if (num == 2) return true;
            }

            // top
            if (d - 1 > -1) {
                d -= 1;
                if (num == 1) return true;
            }

            return false;
        }

        public void SetAttainableNodes(Node n) 
        {
            int d = n.direction;
            Point p = n.point;
            int pxr = p.x + size;
            int pxl = p.x - size;
            int pyt = p.y + size;
            int pyb = p.y - size;

            Node neighborNode;

            // right
            if (d - 8 > -1) {
                d -= 8;
                neighborNode = mapNodes.Find(data => data.point.ToString() == $"({pxr}, {p.y})");
                neighborNode.state = NodeStates.Attainable;
                neighborNode.SetState(neighborNode.state);
            }

            // left
            if (d - 4 > -1) {
                d -= 4;
                neighborNode = mapNodes.Find(data => data.point.ToString() == $"({pxl}, {p.y})");
                neighborNode.state = NodeStates.Attainable;
                neighborNode.SetState(neighborNode.state);
            }

            // bottom
            if (d - 2 > -1) {
                d -= 2;
                neighborNode = mapNodes.Find(data => data.point.ToString() == $"({p.x}, {pyb})");
                neighborNode.state = NodeStates.Attainable;
                neighborNode.SetState(neighborNode.state);
            }

            // top
            if (d - 1 > -1) {
                d -= 1;
                neighborNode = mapNodes.Find(data => data.point.ToString() == $"({p.x}, {pyt})");
                neighborNode.state = NodeStates.Attainable;
                neighborNode.SetState(neighborNode.state);
            }
        }

        private void CreateMapNodeType() 
        {
            for (int i = 0; i < mapNodes.Count; i++) 
            {
                if (i == 0) 
                {
                    mapNodes[i].SetUpNodeType(NodeType.Entrance);
                }
                else if (i == mapNodes.Count - 1) 
                {
                    mapNodes[i].SetUpNodeType(NodeType.Boss);
                }
                else if (i >= threshold - 2 && i <= threshold) 
                {
                    mapNodes[i].SetUpNodeType(NodeType.Elite);
                }
                else 
                {
                    int r = Random.Range(0, 99);
                    if (r < 59) 
                        mapNodes[i].SetUpNodeType(NodeType.Enemy);
                    else if (r < 72) 
                        mapNodes[i].SetUpNodeType(NodeType.Mystery);
                    else if (r < 89) 
                        mapNodes[i].SetUpNodeType(NodeType.Treasure);
                    //else if (r < 91) 
                    //    mapNodes[i].SetUpNodeType(NodeType.Store);
                    else if (r < 99) 
                        mapNodes[i].SetUpNodeType(NodeType.Rest);
                }
            }
            Cursor.lockState = CursorLockMode.None;
        }
    }
}