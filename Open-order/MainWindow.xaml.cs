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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;

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
            Load();
        }
        private void Load()
        {
            StringCollection dir = Properties.Settings.Default.dir;
            StringCollection dir_select = Properties.Settings.Default.dir_select;
            for (int i = 0; i < dir.Count; i++)
            {
                lb_dir_list.Items.Add(Dir_add(dir[i], dir_select[i], Cb_select_Click));
            }
        }
        private void Open()
        {
            foreach (var item in lb_dir_list.Items)
            {
                CheckBox checkBox = item as CheckBox;
                if (checkBox.IsChecked == true)
                {
                    string[] dirs = Directory.GetDirectories(@checkBox.Content.ToString(), tb_order.Text, SearchOption.TopDirectoryOnly);
                    foreach (var dir_item in dirs)
                    {
                        Process.Start(dir_item);
                    }
                }
            }
            return;
        }

        private CheckBox Dir_add(string dir, string dir_select, RoutedEventHandler routedEventHandler)
        {
            CheckBox cb_select = new CheckBox
            {
                Content = dir,
                VerticalAlignment = VerticalAlignment.Center,
                IsChecked = Convert.ToBoolean(dir_select)
            };
            cb_select.Click += routedEventHandler;
            return cb_select;
        }

        private void Cb_select_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox= sender as CheckBox;
            int index = Properties.Settings.Default.dir.IndexOf(checkBox.Content.ToString());
            Properties.Settings.Default.dir_select[index] = "true";
        }

        private void b_open_Click(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void b_add_Click(object sender, RoutedEventArgs e)
        {
            lb_dir_list.Items.Add(Dir_add(tb_dir_add.Text, "false", Cb_select_Click));
            Properties.Settings.Default.dir.Add(tb_dir_add.Text);
            Properties.Settings.Default.dir_select.Add("false");
        }

        private void b_delete_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.dir.RemoveAt(lb_dir_list.SelectedIndex);
            Properties.Settings.Default.dir_select.RemoveAt(lb_dir_list.SelectedIndex);
            lb_dir_list.Items.Remove(lb_dir_list.SelectedItem);
        }
    }
}
