using Betfair.Abstract;
using Betfair.Model;
using MoreLinq;
using Optional.Unsafe;
using OxyPlotEx.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Smarkets.WpfApp2
{
    /// <summary>
    /// Interaction logic for TestUserControl.xaml
    /// </summary>
    public partial class TestUserControl : UserControl
    {
        private readonly MultiLineModel2<string> model;

        public TestUserControl()
        {
            InitializeComponent();
            PlotView.Model = PlotView.Model ?? new OxyPlot.PlotModel();
            model = new OxyPlotEx.ViewModel.MultiLineModel2<string>(new DispatcherX(this.Dispatcher), PlotView.Model);

            Init();
        }

        private async void Init()
        {
            await Task.Run(() =>
             {
                 List<KeyValuePair<string, (DateTime, double)>> list = new List<KeyValuePair<string, (DateTime, double)>>();


                 SelectAll().ToObservable().Subscribe(a =>
                 {
                     lock (list)
                     {
                         list.AddRange(a);
                     }

                     model.OnNext(list.AsEnumerable());
                 });


             });
        }

        private IEnumerable<IEnumerable<KeyValuePair<string, (DateTime, double)>>> SelectAll()
        {
            IEnumerable<(Betfair.Model.Football.ThreeWayResult, Betfair.Model.Football.ThreeWayOdd)> oddsResults =
                Class1.SelectAll().Choose(a => (a.Item2.HasValue, (a.Item1, a.Item2.ValueOrDefault()))).AsParallel();


            foreach (var batch in oddsResults.Batch(20))
            {
                IProfit[] profits = null;
                BetfairBet[] bets = null;
                try
                {

                    var arr = batch.ToArray();
                    bets = arr.Select(ar => ar.Item2).Cast<Betfair.Abstract.IOdd>()
                    .SelectMany(a => 
                    new FootballBet2.BetService().MakeBets(new[] { a })
                    .Select(b => new BetfairBet(a.MarketId, b.Type, b.Price, b.Amount, b.SelectionId, b.EventDate, b.PlacedDate))).ToArray();
                    if (bets.Any())
                    {
                        profits = Helper.GetProfit(bets, arr.Select(ar => ar.Item1)).ToArray();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (profits != null)
                    yield return profits.AsEnumerable().Group(bets);

            }

            //foreach (var dow in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
            //{
            //    Betfair.Abstract.IBet[] ty;
            //    try
            //    {
            //        var js = odds.Where(a => a.EventDate.DayOfWeek == dow).ToArray();
            //        ty = new FootballBet.BetService().MakeBets(js.Cast<Betfair.Abstract.IOdd>()).AsParallel().ToArray();
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //        continue;
            //    }
            //    var io = Helper.GetProfit(ty, results).AsParallel();

            //    foreach (IEnumerable<Profit> batch in io.Batch(20))
            //    {
            //        yield return batch.ByHDASelection(dow.ToString()).ToArray();
            //    }
            //}
        }
    }


    public static class Helper
    {
        public static IEnumerable<KeyValuePair<string, (DateTime, double)>> Group(this IEnumerable<IProfit> profit, IEnumerable<IBet> bet, string name = null)
        {
            return profit.Where(_ => _.Amount != 0)
                 .Join(bet,a => a.BetId,b=>b.GuId, (a,b)=>(a,b.Type))
                 .GroupBy(c=>c.Type)
                 .Select(xx =>
                 {
                     IProfit[] p= xx.Select(v=>v.a).ToArray();
                     double avg = p.Average(a => a.Amount) * 1d / p.Average(a => a.Wager);
                     (DateTime, double) dd = (p.First().EventDate, avg);
                     return new KeyValuePair<string, (DateTime, double)>(xx.Key.ToString(), dd);
                 });
        }

        public static IEnumerable<Profit> GetProfit(IEnumerable<Betfair.Abstract.IBet> bets, IEnumerable<Betfair.Abstract.IResult> results)
        {
            var now = DateTime.Now;
            var pastBets = bets;// 

            return from profit in
                       from result in results
                       join pastBet in pastBets
                       on result.MarketId equals pastBet.MarketId
                       into tempBets
                       from bet in tempBets
                       where bet != null && bet.Amount != 0
                       select new Profit
                       {
                           BetId =bet.GuId,
                           MarketId = result.MarketId,
                           EventDate = bet.EventDate,
                           Wager = bet.Amount,
                           Price = bet.Price,
                           SelectionId = bet.SelectionId,
                           Amount = CalculateProfit(result, bet)
                       }
                   where true
                   select profit;

        }


        public static int CalculateProfit(IResult result, IBet bet)
        {
            if (bet == null || result == null)
            {
                return 0;
            }

            var oc = GetOutCome((Betfair.Model.Football.ThreeWayResult)result);

            if (oc.HasValue)
            {
                var success = (Betfair.Model.Football.BetType)bet.Type == Map(oc.Value);
                return (bet.Amount > 0) ? bet.Amount * ((success ? bet.Price : 0) - 100) : 0;
            }
            return 0;
        }

        private static Betfair.Model.Football.BetType Map(Betfair.Model.Football.OutCome outcome)
        {
            switch (outcome)
            {
                case Betfair.Model.Football.OutCome.Home:
                    return Betfair.Model.Football.BetType.Home;
                case Betfair.Model.Football.OutCome.Away:
                    return Betfair.Model.Football.BetType.Away;
                case Betfair.Model.Football.OutCome.Draw:
                    return Betfair.Model.Football.BetType.Draw;
                default:
                    throw new ArgumentException("No Matches");
            }
        }

        private static Betfair.Model.Football.OutCome? GetOutCome(Betfair.Model.Football.ThreeWayResult result)
        {

            if (result.Player1Status == EndResult.Winner)
                return Betfair.Model.Football.OutCome.Home;
            if (result.Player2Status == EndResult.Winner)
                return Betfair.Model.Football.OutCome.Away;
            if (result.Player3Status == EndResult.Winner)
                return Betfair.Model.Football.OutCome.Draw;
            else
                return default;
        }
    }
}

