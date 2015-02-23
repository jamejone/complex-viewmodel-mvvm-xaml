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
using ComplexMvvmExample.ViewModel;

namespace ComplexMvvmExample.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Class3 c3 = new Class3();
            Class2 c2 = new Class2(c3);
            Class1 c1 = new Class1(c2);

            DataContext = c1;

            c1.PropertyChanged += (o, e) => DebugOutput.Text += "Class1: " + e.PropertyName + '\n';
            c2.PropertyChanged += (o, e) => DebugOutput.Text += "Class2: " + e.PropertyName + '\n';
            c3.PropertyChanged += (o, e) => DebugOutput.Text += "Class3: " + e.PropertyName + '\n';
        }
    }
}
