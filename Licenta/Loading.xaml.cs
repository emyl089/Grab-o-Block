using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Licenta
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public Loading()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.03);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Nada
        }

        void timer_Tick(object sender, EventArgs e)
        {
            pb.Value += 1;
            if(pb.Value == 100)
            {
                timer.Stop();
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }
    }
}
