using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Open_order
{
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
        internal void Open(string order)
        {
            int sum_dir = 0;
            foreach (var item in lb_dir_list.Items)
            {
                CheckBox checkBox = item as CheckBox;
                if (checkBox.IsChecked == true)
                {
                    if (Directory.Exists(@checkBox.Content.ToString()))
                    {
                        string[] dirs = Directory.GetDirectories(@checkBox.Content.ToString(), "*" + order + "*", SearchOption.TopDirectoryOnly);
                        sum_dir += dirs.Length;
                        foreach (var dir_item in dirs)
                        {
                            ProcessStartInfo processStartInfo = new ProcessStartInfo
                            {
                                FileName = dir_item,
                                WindowStyle = ProcessWindowStyle.Maximized
                            };
                            Process.Start(processStartInfo);

                        }
                    }
                    else
                    {
                        MessageBox.Show(@checkBox.Content.ToString() + " - путь не существует.");
                    }

                }
            }
            if (sum_dir == 0)
            {
                MessageBox.Show("Не удалось найти нужный заказ. Проверьте путь или номер заказа.");
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
            lb_dir_list.SelectedIndex = index;
            Properties.Settings.Default.Save();
        }

        private void b_open_Click(object sender, RoutedEventArgs e)
        {
            Open(tb_order.Text.Trim());
        }
        private void b_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_dir_add.Text.Trim() != "" && Properties.Settings.Default.dir.IndexOf(tb_dir_add.Text.Trim()) == -1 && Directory.Exists(tb_dir_add.Text.Trim()))
            {
                lb_dir_list.Items.Add(Dir_add(tb_dir_add.Text.Trim(), "false", Cb_select_Click));
                Properties.Settings.Default.dir.Add(tb_dir_add.Text.Trim());
                Properties.Settings.Default.dir_select.Add("false");
                Properties.Settings.Default.Save();
            }
            else if (!Directory.Exists(tb_dir_add.Text.Trim()))
            {
                MessageBox.Show("Не верно указан путь, или его не существует.");
            }
        }

        private void b_delete_Click(object sender, RoutedEventArgs e)
        {
            if (lb_dir_list.SelectedIndex != -1)
            {
                Properties.Settings.Default.dir.RemoveAt(lb_dir_list.SelectedIndex);
                Properties.Settings.Default.dir_select.RemoveAt(lb_dir_list.SelectedIndex);
                lb_dir_list.Items.Remove(lb_dir_list.SelectedItem);
                Properties.Settings.Default.Save();
            }
        }

        private void tb_order_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Open(tb_order.Text.Trim());
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        
    }
}
