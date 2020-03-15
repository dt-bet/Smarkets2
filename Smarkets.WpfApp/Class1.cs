using MoreLinq;
using Reactive.Bindings;
using Smarkets.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Smarkets.WpfApp
{
    class Class1
    {
        public ObservableCollection<Winning> Collection { get; } = new ObservableCollection<Winning>();

        public ObservableCollection<string> Collection2 { get; } = new ObservableCollection<string>();


        public ReactiveProperty<double> Value { get; set; }

        public Class1()
        {
            Value = Helper.GetChanges(Collection).Where(_ => _.Action == NotifyCollectionChangedAction.Add).Select(_ => Collection.Select(w => w.Amount).Sum()).ToReactiveProperty();

            GetProfit();
            // GetEventIds();
        }

        private void GetEventIds()
        {
            var enm2 = System.IO.Directory.EnumerateFiles("..//..//..//Smarkets.EF6App//Data", "*.sqlite", System.IO.SearchOption.AllDirectories)
             .Select(_ => (new SQLite.SQLiteAsyncConnection(_).Table<Match>().ToListAsync()).ToObservable())
             .ToObservable()
             .SubscribeOn(System.Reactive.Concurrency.DefaultScheduler.Instance)
             .SelectMany(_ => _.Select(a => a.First().EventId.ToString()));

            enm2.ObserveOnDispatcher().Subscribe(_ => Collection2.Add(_));
        }

        private void GetProfit()
        {
            using (var enm = Smarkets.DAL.Sqlite.ScoreMatch.MatchDefault().GetEnumerator())
            {

                Observable.Generate(enm, _ => enm.MoveNext(), _ => enm, _ => _.Current/*, _ => TimeSpan.FromSeconds(100)*/)
                    .SubscribeOn(System.Reactive.Concurrency.DefaultScheduler.Instance)
                    .ObserveOnDispatcher()
                    .Subscribe(_ =>
                    {
                        foreach (var pr in GetOdds(_).TakeLast(1))
                        {
                            if (pr.home < 1.2)
                            {
                                double winning = ((_.HomeScore > _.AwayScore) ? (pr.home) : 0) - 1d;

                                Collection.Add(new Winning { Home = pr.home, Draw = pr.draw, Away = pr.away, Amount = winning });
                            }
                        }
                        //Console.WriteLine(Collection.DefaultIfEmpty().Average(a_ => a_?.Amount));
                    });
            }
        }


        private static IEnumerable<(double home, double draw, double away)> GetOdds(Match src)
        {
            var market = src.Markets.SingleOrDefault(_ => _.Key == Betting.Enum.MarketType.FullTimeResult);
            var xx = market?.IndexedContracts.Count().Equals(3) ?? false ?
                  market.IndexedContracts.ToDictionary(c=>c.Key,c => c.Value.MaxOffers):
                  default;

            return xx != null?
             from home in xx[Betting.Enum.ContractType.Home]
                     join draw in xx[Betting.Enum.ContractType.Draw] on home.Time equals draw.Time
                     join away in xx[Betting.Enum.ContractType.Away] on home.Time equals away.Time
                     select (home.Value/100d, draw.Value / 100d, away.Value / 100d):
             new List<(double home, double draw, double away)>();

        }
    }


    public class Winning
    {
        public double Home { get; set; }

        public double Draw { get; set; }

        public double Away { get; set; }

        public double Amount { get; set; }
    }
    public static class Helper
    {
        public static IObservable<NotifyCollectionChangedEventArgs> GetChanges<T>(this ObservableCollection<T> collection)
        {
            return Observable
                   .FromEvent<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                      handler => (sender, args) => handler(args),
                        handler => collection.CollectionChanged += handler,
                        handler => collection.CollectionChanged -= handler);
        }
    }
}
