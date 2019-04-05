using DistribuidoraWandall.DB;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DistribuidoraWandall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DBEntities.InitializeDatabase();

            InitializeComponent();
        }
    }
}
