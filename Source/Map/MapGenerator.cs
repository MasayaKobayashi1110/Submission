using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK_Map 
{
    /// <summary>
    /// マップの生成をする
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        public GameObject nodePrefab; // ノードのプレハブを格納する
        public Transform mapLayer; // ノードの親
        // マップの幅
        [Range(1, 7)]
        public int width = 5;
        // マップの高さ
        [Range(1, 7)]
        public int height = 5;
        // ノード間の幅と高さ
        [Range(1, 6)]
        public int size = 5;
        // 生成されるノードの最低数
        [Range(1, 49)]
        public int threshold = 15;

        // ノードを生成可能な座標のリスト
        private List<Point> points = new List<Point>(49);
        // 生成されたノードのリスト
        private List<Node> mapNodes = new List<Node>(49);

        // マップの左上の座標
        private int leftTopX;
        private int leftTopY;

        // 再生成可能かどうか
        public bool regeneration = true;

        // マップの自動生成をする関数
        public void ShowMap() 
        {
            // 再生成不可なら返す
            if (regeneration == false) return;

            // 初期化をする
            Initialization();
            // 生成中はマウスをロックする
            Cursor.lockState = CursorLockMode.Locked;
            // マップの左上の座標を計算する
            CalculateLeftTopPoint();
            // ノードを生成可能な座標を計算する
            CreatePoint();
            // スタート地点のノードを生成する
            StartCoroutine("CreateFirstNode");
            // ノードを生成する
            StartCoroutine(CreateNode(0));
        }

        // マップの初期化をする
        private void Initialization() 
        {
            // 生成可能な座標のリストを空にする
            points.Clear();

            // ノードのリストにあるオブジェクトをすべて消去する
            foreach (Node nodes in mapNodes) 
                Destroy(nodes.gameObject);
            
            // ノードのリストを空にする
            mapNodes.Clear();
        }
        
        // マップの左上の座標を計算する
        private void CalculateLeftTopPoint() 
        {
            // 必ずマップの中心が画面の中心になるようにする
            if (width % 2 == 0) 
                leftTopX = (width / 2 * size - 2) * -1;
            if (height % 2 == 0)
                leftTopY = height / 2 * size - 2;
            if (width % 2 != 0) 
                leftTopX = width / 2 * size * -1;
            if (height % 2 != 0) 
                leftTopY = height / 2 * size;
        }
        
        // ノードを生成可能な座標を計算する
        private void CreatePoint() 
        {
            // 高さの分だけループする
            for (int i = 0; i < height; i++) 
            {
                // 幅の分だけループする
                for (int j = 0; j < width; j++) 
                {
                    // 左上の座標から一行ずつ座標を計算する
                    Point p = new Point(leftTopX + size * j, leftTopY - size * i);
                    // リストに追加する
                    points.Add(p);
                }
            }
        }

        // リストpointsの座標を確かめるための関数
        private void CreateBackground() 
        {
            for (int i = 0; i < points.Count; i++) 
            {
                // 座標にオブジェクトを配置する
                Instantiate(nodePrefab, new Vector3(points[i].x, points[i].y, 20), Quaternion.identity, mapLayer);
            }
        }

        // スタート地点のノードを生成する
        private IEnumerator CreateFirstNode() 
        {
            yield return new WaitForSeconds(0.2f);

            // リストpoints内のランダムな座標を取得する
            int fp = Random.Range(0, points.Count - 1);
            // fpの座標にノードを生成する
            var mapNode = Instantiate(nodePrefab, new Vector3(points[fp].x, points[fp].y, 20), Quaternion.identity, mapLayer);
            // 生成したノードのクラスNodeを取得する
            var node = mapNode.GetComponent<Node>();

            // fpの座標でクラスPointを作成する
            Point p = new Point(points[fp].x, points[fp].y);
            // 生成したノードが持つクラスNode内のpointに座標を渡す
            node.point = p;
            // 生成したノードの所有している方向のセットアップをする
            node.SetUp(CheckNeighborPoint(node.point), true);
            // リストmapNodesに生成したノードを追加する
            mapNodes.Add(node);
        }

        // ノード周囲4方向の座標がリストpointsに存在するか確認する
        private int CheckNeighborPoint(Point p) 
        {
            // 方向を表す整数型、初期状態は所有する方向なしで0
            int direction = 0;
            // 周囲4方向のx座標とy座標を計算する
            int pxr = p.x + size;
            int pxl = p.x - size;
            int pyt = p.y + size;
            int pyb = p.y - size;
            /*
                上方向は+1、下方向は+2、左方向は+4、右方向は+8
                    1
                4       8
                    2
            */
            // 上方向
            // リストpointsに上の座標が存在するか確認する
            Point neighbor = points.Find(data => data.ToString() == $"({p.x}, {pyt})");
            // 存在するなら処理をする
            if (neighbor != null)
                direction += 1;
            
            // 下方向
            // リストpointsに下の座標が存在するか確認する
            neighbor = points.Find(data => data.ToString() == $"({p.x}, {pyb})");
            if (neighbor != null)
                direction += 2;
            
            // 左方向
            // リストpointsに左の座標が存在するか確認する
            neighbor = points.Find(data => data.ToString() == $"({pxl}, {p.y})");
            if (neighbor != null)
                direction += 4;

            // 右方向
            // リストpointsに右の座標が存在するか確認する
            neighbor = points.Find(data => data.ToString() == $"({pxr}, {p.y})");
            if (neighbor != null)
                direction += 8;

            // 方向を表す整数型directionを返す
            return direction;
        }

        // ノードを生成する
        private IEnumerator CreateNode(int num) 
        {
            // 生成アニメーションのための待機時間
            yield return new WaitForSeconds(0.2f);
            // ひとつ前のノードが何番目なのかを表す整数型
            int number = num;
            // ひとつ前のノードの所有している方向を取得する
            int dir = mapNodes[number].direction;
            // ひとつ前のノードの周りの4方向の座標を計算する
            int pxr = mapNodes[number].point.x + size;
            int pxl = mapNodes[number].point.x - size;
            int pyt = mapNodes[number].point.y + size;
            int pyb = mapNodes[number].point.y - size;

            // 下方向の判別をする
            if (dir - 8 > -1) {
                // 下方向のノードがすでに存在するか確認する
                Node n = mapNodes.Find(data => data.point.ToString() == $"({pxr}, {mapNodes[number].point.y})");
                // 存在しないなら処理をする
                if (n == null) 
                {
                    // 下方向にノードを生成する
                    var mapNode = Instantiate(nodePrefab, new Vector3(pxr, mapNodes[number].point.y, 20), Quaternion.identity, mapLayer);
                    // 生成したノードの所有するクラスNodeを取得する
                    var node = mapNode.GetComponent<Node>();
                    // 生成した座標でクラスPointを作成する
                    Point p = new Point(pxr, mapNodes[number].point.y);
                    // 生成したノードに座標を渡す
                    node.point = p;
                    // ノードのセットアップをする
                    node.SetUp(CheckNeighborPointRandom(node.point, mapNodes[number].point), false);
                    // ノードをリストに追加する
                    mapNodes.Add(node);
                }
                dir -= 8;
            }

            // 左方向の判別をする
            if (dir - 4 > -1) {
                // 左方向のノードがすでに存在するか確認する
                Node n = mapNodes.Find(data => data.point.ToString() == $"({pxl}, {mapNodes[number].point.y})");
                if (n == null) 
                {
                    // 左方向にノードを生成する
                    var mapNode = Instantiate(nodePrefab, new Vector3(pxl, mapNodes[number].point.y, 20), Quaternion.identity, mapLayer);
                    var node = mapNode.GetComponent<Node>();
                    Point p = new Point(pxl, mapNodes[number].point.y);
                    node.point = p;
                    node.SetUp(CheckNeighborPointRandom(node.point, mapNodes[number].point), false);
                    mapNodes.Add(node);
                }
                dir -= 4;
            }

            // 下方向の判別をする
            if (dir - 2 > -1) {
                // 下方向のノードがすでに存在するか確認する
                Node n = mapNodes.Find(data => data.point.ToString() == $"({mapNodes[number].point.x}, {pyb})");
                if (n == null) 
                {
                    // 下方向にノードを生成する
                    var mapNode = Instantiate(nodePrefab, new Vector3(mapNodes[number].point.x, pyb, 20), Quaternion.identity, mapLayer);
                    var node = mapNode.GetComponent<Node>();
                    Point p = new Point(mapNodes[number].point.x, pyb);
                    node.point = p;
                    node.SetUp(CheckNeighborPointRandom(node.point, mapNodes[number].point), false);
                    mapNodes.Add(node);
                }
                dir -= 2;
            }

            // 上方向の判別をする
            if (dir - 1 > -1) {
                // 上方向のノードがすでに存在するか確認する
                Node n = mapNodes.Find(data => data.point.ToString() == $"({mapNodes[number].point.x}, {pyt})");
                if (n == null) 
                {
                    // 上方向にノードを生成する
                    var mapNode = Instantiate(nodePrefab, new Vector3(mapNodes[number].point.x, pyt, 20), Quaternion.identity, mapLayer);
                    var node = mapNode.GetComponent<Node>();
                    Point p = new Point(mapNodes[number].point.x, pyt);
                    node.point = p;
                    node.SetUp(CheckNeighborPointRandom(node.point, mapNodes[number].point), false);
                    mapNodes.Add(node);
                }
                dir -= 1;
            }

            // numberがリストmapNodesの要素数と一致しないなら処理をする
            if (number != mapNodes.Count - 1) 
                // 次のノードを生成する
                StartCoroutine(CreateNode(number + 1));
            else
                // ノードの生成が終了したらノードのタイプ
                CreateMapNodeType();
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