using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Entity.XML
{
    public enum TeamClass
    {
        None, A, B
    }

    public enum ScoreType
    {
        None, Game, Set

    }

    public enum ScorePeriod
    {
        None, FirstSet, SecondSet, ThirdSet, FourthSet, FifthSet, FullTime
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
