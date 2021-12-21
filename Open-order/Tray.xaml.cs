using System;
using System.Windows;
using System.Windows.Input;

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
            Show();
            Activate();
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
            tbi_order.Visibility = Visibility.Hidden;
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
