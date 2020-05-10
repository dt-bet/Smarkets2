using Betting.Entity.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarkets.Tennis.SqliteApp
{
    class Process
    {
        public static void GetProfits()
        {

            using var sqlite = new SQLite.SQLiteConnection("../../../Data/Main.sqlite");
            var odds = sqlite.Table<TwoWayOdd>().Skip(10000).ToArray();
            var xyzs = sqlite.Table<XYZPoint>().ToArray();
            var profits = from odd in odds
                          join codd in odds.Select(Convert)
                          on (odd.MarketId, odd.OddsDate) equals (codd.MarketId, codd.OddsDate)
                          join result in sqlite.Table<Result>().ToArray()
                          on odd.MarketId equals result.MarketId
                          let ratio = GetRatio(codd)
                          let profit = xyzs.SingleOrDefault(a => a.X == ratio)
                          where profit != null && profit.Y > 0
                          select profit.Y * GetProfit(odd, result) / GetPremium(odd);


            var arr = profits.ToArray();
        }


        public static void InsertPoints()
        {
            SQLitePCL.Batteries.Init();

            using var sqlite = new SQLite.SQLiteConnection("../../../Data/Main.sqlite");
            sqlite.CreateTable<XYZPoint>();

            var profit = from odd in sqlite.Table<TwoWayOdd>().ToArray().Take(10000).Select(Convert)
                         join result in sqlite.Table<Result>().ToArray()
                         on odd.MarketId equals result.MarketId
                         let xy = (GetRatio(odd), GetProfit(odd, result))
                         group xy by xy.Item1
                         into temp
                         orderby temp.Key
                         select (temp.Key, profit: temp.Average(a => (double)a.Item2), count: temp.Count());


            var arr = profit.Select(p3 => new XYZPoint((int)(p3.Key), (int)(p3.profit * XYZPoint.Factor), (int)(p3.count))).AsParallel().ToArray();

            sqlite.DeleteAll<XYZPoint>();
            int inserts = sqlite.InsertAll(arr);
        }


        private static double GetRatio(TwoWayOdd twoWayOdd)
        {
            var ratio = ((double)twoWayOdd.Player1Odd) / twoWayOdd.Player2Odd;
            ratio = (int)(10 * System.Math.Log((ratio < 1 ? 1 / ratio : ratio)));
            return ratio;
        }

        private static int GetProfit(TwoWayOdd odd, Result result)
        {
            var div = (1d * odd.Player1Odd / odd.Player2Odd);
            if (div > 1)
            {
                return result.WinnerId == odd.Player2Id ?
                     (int)odd.Player2Odd - 100 :
                     -100;
            }
            else
            {
                div = 1 / div;

                return result.WinnerId == odd.Player1Id ?
                       (int)odd.Player1Odd - 100 :
                         -100;
            }
        }
        private static double GetPremium(TwoWayOdd twoWayOdd)
        {
            var sum = 100d / twoWayOdd.Player1Odd + 100d / twoWayOdd.Player2Odd;
            return ((Math.Max(1, sum) - 1) * 1000) + 1;
        }


        private static TwoWayOdd Convert(TwoWayOdd twoWayOdd)
        {
            var sum = 100d / twoWayOdd.Player1Odd + 100d / twoWayOdd.Player2Odd;


            return new TwoWayOdd(
                twoWayOdd.EventDate,
               null,
               null,
               twoWayOdd.MarketId,
                (uint)(twoWayOdd.Player1Odd * sum),
               (uint)(twoWayOdd.Player2Odd * sum),
                twoWayOdd.Player1Id,
                twoWayOdd.Player2Id,
                twoWayOdd.Player1Name,
                twoWayOdd.Player2Name,
                twoWayOdd.OddsDate);
        }

    }

    public class WXYZPoint : XYZPoint
    {
        public WXYZPoint()
        {
        }

        public WXYZPoint(int x, int y, int z, int w) : base(x, y, z)
        {
            W = w;

        }

        public int W { get; set; }
    }


    public class XYZPoint : Point, IEquatable<XYZPoint>
    {

        public XYZPoint()
        {
        }

        public XYZPoint(int x, int y, int z) : base(x, y)
        {
            Z = z;

        }

        public int Z { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as XYZPoint);
        }

        public bool Equals(XYZPoint other)
        {
            return other != null &&
                   base.Equals(other) &&
                   Z == other.Z && (this as Point).Equals(other as Point);
        }

        public override int GetHashCode()
        {
            var hashCode = -1899095202;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            hashCode = hashCode * (this as Point).GetHashCode();
            return hashCode;
        }

        public static bool operator ==(XYZPoint left, XYZPoint right)
        {
            return EqualityComparer<XYZPoint>.Default.Equals(left, right);
        }

        public static bool operator !=(XYZPoint left, XYZPoint right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"X:{X} Y:{Y} Z:{Z}";
        }
    }

    public class Point : IEquatable<Point>
    {
        public const int Factor = 1000;

        public Point()
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Point left, Point right)
        {
            return EqualityComparer<Point>.Default.Equals(left, right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }
}
