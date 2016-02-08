using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataHolder;
using IDirectCommunication;


namespace TestovaciKlient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            nacitajTypyJedal();
        }

        public void nacitajTypyJedal()
        {
            IList<TTypJedla> typyJedal=new List<TTypJedla>();
            using (ChannelFactory<IServiceSprava> stolyServiceProxy =
                new ChannelFactory<IServiceSprava>("MyServiceStolyEndpoint"))
            {
                stolyServiceProxy.Open();
                IServiceSprava stolyService = stolyServiceProxy.CreateChannel();
                typyJedal = stolyService.typyJedal("sk");

                stolyServiceProxy.Close();
            }
            

            listBoxTypyJedal.ItemsSource = typyJedal;
            


        }

        private void listBoxTypyJedal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void listBoxTypyJedal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList<TTypJedla> items=(IList<TTypJedla>) listBoxTypyJedal.SelectedItems;
            if (items.Count > 0)
            {
                int index = items[0].Id;
            }
        }
    }
}
