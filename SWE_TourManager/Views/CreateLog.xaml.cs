using SWE_TourManager.Models;
using SWE_TourManager.ViewModels;
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
using System.Windows.Shapes;

namespace SWE_TourManager.Views
{
    /// <summary>
    /// Interaction logic for CreateLog.xaml
    /// </summary>
    public partial class CreateLog : Window
    {
        public CreateLog(TourItem tour)
        {
            InitializeComponent();
            this.DataContext = new CreateLogVM(tour);
        }

        
    }
}
