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

namespace Open_order
{
    public partial class Tray : Window
    {
        public Tray()
        {
            InitializeComponent();
        }
        MainWindow mainWindow = new MainWindow();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Open(tb_order.Text);
        }
        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Activate();
            Show();
            tb_order.Focus();
        }
        private void show_order(object sender, RoutedEventArgs e)
        {
            Activate();
            Show();
            tb_order.Focus();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            if (MainWindow.GetWindow(App.Current.MainWindow).Visibility == Visibility.Visible)
            {
                MainWindow.GetWindow(App.Current.MainWindow).Visibility = Visibility.Hidden;
            }
            else
            {
                MainWindow.GetWindow(App.Current.MainWindow).Visibility = Visibility.Visible;
            }
        }
        private void tb_order_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                mainWindow.Open(tb_order.Text);
            }
            if (e.Key == Key.Escape)
            {
                Hide();
            }
        }
    }
}
