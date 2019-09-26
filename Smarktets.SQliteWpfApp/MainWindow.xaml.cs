using System;
using System.Collections.Generic;
using System.Linq;
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
using Smarkets.Map;
using Smarkets.Model.XML;
using Smarkets.DAL;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using SportsBetting.Entity.Sqlite;
using System.Collections.ObjectModel;
using SportsBetting.DAL.Sqlite;
using System.ComponentModel;

namespace Smarktets.SQliteWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Main();
        }

        private BackgroundWorker worker2;
        private static Repo conn;
        static ObservableCollection<IList<Match>> matches = new ObservableCollection<IList<Match>>();

        private void Main()
        {
            int i = 0;
            var worker = new BackgroundWorker();
            worker2 = new BackgroundWorker();
           
            worker.DoWork += (sender, e) =>
            {
                double count = Smarkets.DAL.XMLRepo.CountFiles();

                foreach (var emr in Smarkets.DAL.XMLRepo.SelectFiles())
                {
                    i++;
                    var entities = Smarkets.Map.EntityMap
                    .MapToEntity(emr.Events
                    .Where(ee => ee.Type == "Football match" && ee.ParentName.ToLower() != "outright").ToArray(), emr.TimeStamp);
                    worker.ReportProgress((int)((double)i / count), entities);
                }
            };
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;

            worker.RunWorkerAsync();

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            conn.Dispose();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                Dispatcher.BeginInvoke(new Action(() => PBar.Value = e.ProgressPercentage));
                worker2.DoWork += (sender2, e2) =>
                  {
                      conn = new SportsBetting.DAL.Sqlite.Repo("" + "db." + UtilityDAL.Constants.SqliteDbExtension,false);
                      conn.TransferToDB((Match[])e.UserState);
                      conn.Dispose();
                  };
            }
            while(worker2.IsBusy)
            {

            }
            worker2.RunWorkerAsync();
        }



        //var files=Smarkets.DAL.XMLRepo.SelectFiles();
        //double cnt =(double) Convert.ToDouble(files.Count());
        //using (conn = new SportsBetting.DAL.Sqlite.Repo("" + "db." + UtilityDAL.Constants.SqliteDbExtension))
        //{
        //    var disposable = Observable.Create<Odds>(observer =>
        //    {
        //        foreach (var file in files)
        //            file.ToObservable().Subscribe(__ => observer.OnNext(__));

        //        return System.Reactive.Disposables.Disposable.Empty;
        //    })
        //    .Subscribe(_ =>
        //    {
        //       

        //        Application.Current.Dispatcher.BeginInvoke(new Action(()=>
        //        {
        //            conn.TransferToDB(entities);
        //            PBar.Value = 1 / cnt;
        //        }));
        //    });
        //}
        ////using (conn = new SportsBetting.DAL.Sqlite.Repo(System.IO.Directory.GetCurrentDirectory()+ "db." + UtilityDAL.Constants.SqliteDbExtension))
        ////{
        ////    conn.TransferToDB(matches.SelectMany(_ => _).ToList());
        ////}

        //Console.ReadLine();

    }
}
