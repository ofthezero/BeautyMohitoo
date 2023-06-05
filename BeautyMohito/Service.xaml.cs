using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;


namespace BeautyMohito
{
    public partial class Service : Window
    {

        SqlConnection con = new SqlConnection();
        DbMohito DbMohito;
        ServiceTableAdapter serviceTableAdapter;

        private DispatcherTimer _timer;
        public static int to;

        string sizeBooking;
        public static string sizeSave;

        public static string IdOn;
        public static string NameOn;
        public static string PriceOn;
        public static string TimeOn;
        public static string Image;

        public Service()
        {
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Mohito.Properties.Settings.MohitoConnectionString"].ConnectionString.ToString();
            InitializeComponent(); RefreshData(); SaveSize();
        }

        private bool IsMaximize = false;

        public void RefreshData()
        {
            ServiceTableAdapter adapter = new ServiceTableAdapter();
            DbMohito.ServiceDataTable table = new DbMohito.ServiceDataTable();
            adapter.Fill(table);
            membersDataGrid.ItemsSource = table;
        }

        public void SaveSize()
        {
            if (sizeBooking == null)
            {
                sizeSave = "1";
            }
            else
            {
                sizeSave = sizeBooking;
            }
        }
       
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    sizeSave = "1";
                    this.WindowState = WindowState.Normal; this.Width = 1080; this.Height = 720;

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
           

            if (sizeSave == "1")
            {

               
                
            }
            else
            {
                
                
            }
            this.Close();
        }



        private void textBoxFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ServiceTableAdapter adapter = new ServiceTableAdapter();
            DbMohito.ServiceDataTable table = new DbMohito.ServiceDataTable();
            adapter.Fill(table);
            membersDataGrid.ItemsSource = table;

            DataView dv = new DataView(table);

            if (textBoxFilter.Text != null)
            {
                dv.RowFilter = $@"[Name_service] LIKE '%{textBoxFilter.Text}%' ";
                membersDataGrid.ItemsSource = dv.ToTable().DefaultView;
            }
            else
            {
            }
        }




        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }
        private void Review_Click(object sender, RoutedEventArgs e)
        {
            Review review = new Review();
            review.Show();
            this.Close();
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            service.Show();
            this.Close();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            client.Show();
            this.Close();
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book();
            book.Show();
            this.Close();
        }
        private void Roles_Click(object sender, RoutedEventArgs e)
        {
            Roles roles = new Roles();
            roles.Show();
            this.Close();
        }
        private void UpdateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateClient updateClient = new UpdateClient();
            updateClient.Show();
            this.Close();
        }
        private void Cheque_Click(object sender, RoutedEventArgs e)
        {
            Cheque cheque = new Cheque();
            cheque.Show();
            this.Close();
        }




        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить эти данные? Данные удалятся навсегда, без возможности восстановления", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {

                }
                else
                {
                    new ServiceTableAdapter().DeleteQuery(Convert.ToInt32((membersDataGrid.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                }
            }
            catch
            {
            }
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (membersDataGrid.SelectedItem != null)
            {
                if (membersDataGrid.SelectedItem != null)
                    IdOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[0].ToString(); if (membersDataGrid.SelectedItem != null)
                    NameOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); if (membersDataGrid.SelectedItem != null)
                    PriceOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (membersDataGrid.SelectedItem != null)
                    TimeOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[3].ToString(); 


    }
            else
            {

            }
        }


    }
}