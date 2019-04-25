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

namespace course_proj_5sem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void SetColor(string color)
        {
            gr1.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));
            gr2.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));
            gr3.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));

            //ColorConverter.ConvertFromString(color);
        }       
        
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////////
            ///
            Books b = new Books();
            frame1.Content = b;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Settings z = new Settings(this);
            frame1.Content = z;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Basket z = new Basket();
            frame1.Content = z;
        }

        private void ord_Click(object sender, RoutedEventArgs e)
        {
            Orders z = new Orders();
            frame1.Content = z;
        }
    }
}
