using System;

namespace MK_Map
{
    /// <summary>
    /// 他のPointとの等価性を比較する
    /// </summary>
    public class Point : IEquatable<Point>
    {
        // 座標x, y
        public int x;
        public int y;

        // コンストラクタ（x,yを受け取る）
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // 指定された別のPointオブジェクトと現在のオブジェクトが等しいか判断する
        public bool Equals(Point other) 
        {
            if (ReferenceEquals(null, other)) return false; // otherがnull
            if (ReferenceEquals(this, other)) return true; // otherが同じ
            return x == other.x && y == other.y;
        }

        // 指定された別のオブジェクトとの等価性を比較する
        public override bool Equals(object obj) 
        {
            if (ReferenceEquals(null, obj)) return false; // objがnull
            if (ReferenceEquals(this, obj)) return true; // objが同じ
            if (obj.GetType() != this.GetType()) return false; // objのタイプが違う
            return Equals((Point)obj);
        }

        // 現在のPointオブジェクトのハッシュコード（個別の識別子）を返す
        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }
        
        // Pointオブジェクトの座標値を文字列にして返す
        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}