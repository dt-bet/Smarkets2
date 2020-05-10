using System;
using System.Collections.Generic;
using System.Text;
using UtilityEnum;

namespace Smarkets.Entity.XML
{
    public enum TeamClass
    {
        None, 
        [NamesAttribute("home")]
        A,
        [NamesAttribute("away")]
        B,
        Draw
    }

    public enum ScoreType
    {
        None, Game, Set, GameHalf

    }

    public enum ScorePeriod
    {
        None, FirstSet, SecondSet, ThirdSet, FourthSet, FifthSet, FullTime, HalfTime
    }

    public class Result
    {
        public Result()
        {

        }

        [SQLite.PrimaryKey]
        public long Id { get; set; }

        public long EventId { get; set; }

        public TeamClass TeamClass { get; set; }

        public ScoreType ScoreType { get; set; }

        public ScorePeriod ScorePeriod { get; set; }

        public int Score { get; set; }
    }
}
