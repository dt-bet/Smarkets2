using Betfair.Abstract;
using Betfair.Model;
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
            //var matches = new Betfair.DAL.FootballConnection().GetMatches();

            Init();


            //var tc = io.Sum(a => a.Amount);
        }

        private async void Init()
        {
            await System.Threading.Tasks.Task.Run(() => GetAll().SelectMany(a => a).AsParallel().ToArray())
                .ContinueWith(async a => model.OnNext(await a), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private IEnumerable<KeyValuePair<string, (DateTime, double)>[]> GetAll()
        {
            //var xx = new Betfair.DAL.FootballConnection().GetAllOdds();

            //var xs = new Betfair.DAL.FootballConnection().GetResults();

            var xx = Class1.GetOdds().AsParallel().Take(100).ToArray();

            var xs = Class1.GetResults().AsParallel().ToArray();

            foreach (var dow in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
            {
                Betfair.Abstract.IBet[] ty;
                try
                {
                    var js = xx.Where(a => a.EventDate.DayOfWeek == dow).ToArray();
                    ty = new FootballBet.BetService().MakeBets(js.Cast<Betfair.Abstract.IOdd>()).ToArray();
                }
                catch (Exception e)
                {
                    continue;
                }
                var io = Helper.GetProfit(ty, xs);
                yield return io.ByHDASelection(dow.ToString()).ToArray();
            }
            new FootballBet.BetService().MakeBets(xx);
           ; ;

        }
    }


    public static class Helper
    {
        public static IEnumerable<KeyValuePair<string, (DateTime, double)>> ByHDASelection(this IEnumerable<Profit> profit, string name = null)
        {
            return profit.Where(_ => _.Amount != 0)
                 .GroupBy(a => a.SelectionId)
                 .Select(_ =>
                 {
                     double avg = _.Average(a => a.Amount) * 1d / _.Average(a => a.Wager);
                     (DateTime, double) dd = (_.First().EventDate, avg);
                     return new KeyValuePair<string, (DateTime, double)>(name, dd);
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

            var success = bet.SelectionId == result.WinnerId;
            return (bet.Amount > 0) ? bet.Amount * ((success ? bet.Price : 0) - 100) : 0;

        }
    }
}

