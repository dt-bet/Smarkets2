using OxyPlotEx.ViewModel;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Smarkets.WpfApp2
{

    public class DispatcherX : IContext
    {
        private Dispatcher dispatcher;

        public DispatcherX(Dispatcher dispatcher) => this.dispatcher = dispatcher;

        public void BeginInvoke(Action action) => dispatcher.BeginInvoke(action);


        public void Invoke(Action action) => dispatcher.Invoke(action);

    }
}
