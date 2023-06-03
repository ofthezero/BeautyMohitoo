using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BeautyMohito.DbMohitoTableAdapters;
using System.Windows.Controls;
using System;
using System.Configuration;

namespace BeautyMohito
{

    public partial class Booking : Window
    {
        SqlConnection con = new SqlConnection();
        DataSet DataSet;
        UserTableAdapter userTableAdapter;
        MainWindow main = new MainWindow();


        string newSave = MainWindow.sizeSave;
        public static string sizeSave;
        public Booking()
        {
          //  con.ConnectionString = ConfigurationManager.ConnectionStrings["Mohito.Properties.Settings.MohitoConnectionString"].ConnectionString.ToString();

            InitializeComponent(); RefreshData(); sizeSave = newSave;
        }

        

        private bool IsMaximize = false;

        public void RefreshData()
        {
            //UserTableAdapter adapter = new UserTableAdapter(); 
            //DataSet.UserDataTable table = new DataSet.UserDataTable();
            //adapter.Fill(table);
            //membersDataGrid.ItemsSource = table;
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    sizeSave = "1";
                    this.WindowState = WindowState.Normal; this.Width = 1080;  this.Height = 720;
                   
                    IsMaximize = false;
                }
                else
                {
                    sizeSave = "2";
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textBoxFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
           // UserTableAdapter adapter = new UserTableAdapter();
           // DataSet.UserDataTable table = new DataSet.UserDataTable();
           // adapter.Fill(table);

           // DataView dv = new DataView(table); 

           // if (textBoxFilter.Text != null)
           // {
           //     dv.RowFilter = $@"[Login] LIKE '%{textBoxFilter.Text}%' ";
           //     membersDataGrid.ItemsSource = dv.ToTable().DefaultView; 
           // }
           //else { }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (sizeSave == "1")
            {
                main.WindowState = WindowState.Normal; main.Width = 1080; main.Height = 720; IsMaximize = false;
                main.Show();
            }
            else
            {
                main.WindowState = WindowState.Maximized; IsMaximize = true;
                main.Show();
            }
            this.Close();
        }
    }
}