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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWE_TourManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int clicked = 0;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void buttonHello_Click(object sender, RoutedEventArgs e)
        {
            
            switch (clicked)
            {
                case 0:
                    buttonHello.Content = "Why";
                    break;
                case 1:
                    buttonHello.Content = "Stop";
                    break;
                case 3:
                    buttonHello.Width = 200;
                    buttonHello.Content = "Seriously, Stop";
                    break;
                case 5:
                    buttonHello.Content = "(╯°□°）╯︵ ┻━┻";
                    break;
                case 6:
                    buttonHello.Width = 400;
                    buttonHello.Content = "At least you are not double clicking...";                    
                    break;
            }

            clicked++;
        }

        private void buttonHello_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            buttonHello.Background = Brushes.DarkCyan;
            buttonHello.Width = 200;
            buttonHello.Content = "now you broke me :(";
            buttonHello.IsEnabled = false;
        }
    }
}
