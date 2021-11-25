using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Drawing; // Добавить ссылку
using System.Windows.Forms; // Добавить ссылку, необходимо использовать

namespace Open_order
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
        void Open()
        {
            string[] dirs = Directory.GetDirectories(@"c:\", tb_order.Text, SearchOption.TopDirectoryOnly);
            foreach (var item in dirs)
            {
                Process.Start(item);
            }
            return;
        }

        private void b_open_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }
    }
}
