using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BeautyMohito
{
    public partial class Client : Window
    {

        SqlConnection con = new SqlConnection();
        DbMohito DbMohito;
        ClientTableAdapter clientTableAdapter;

        private DispatcherTimer _timer;
        public static int to;

        string sizeBooking = Booking.sizeSave;
        public static string sizeSave;

        public static string IdOn;
        public static string SurnameOn;
        public static string NameOn;
        public static string PetronymicOn;
        public static string NumberOn;
        public static string NotesOn;
        public static string EmailOn;
       
        public Client()
        {
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Mohito.Properties.Settings.MohitoConnectionString"].ConnectionString.ToString();
            InitializeComponent(); RefreshData(); SaveSize();
        }

        private bool IsMaximize = false;

        public void RefreshData()
        {
            ClientTableAdapter adapter = new ClientTableAdapter();
            DbMohito.ClientDataTable table = new DbMohito.ClientDataTable();
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
            Booking booking = new Booking();

            if (sizeSave == "1")
            {

                booking.WindowState = WindowState.Normal; booking.Width = 1080; booking.Height = 720; IsMaximize = false;
                booking.Show();
            }
            else
            {
                booking.WindowState = WindowState.Maximized; IsMaximize = true;
                booking.Show();
            }
            this.Close();
        }



        private void textBoxFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ClientTableAdapter adapter = new ClientTableAdapter();
            DbMohito.ClientDataTable table = new DbMohito.ClientDataTable();
            adapter.Fill(table);
            membersDataGrid.ItemsSource = table;

            DataView dv = new DataView(table);

            if (textBoxFilter.Text != null)
            {
                dv.RowFilter = $@"[Surname] LIKE '%{textBoxFilter.Text}%' ";
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (membersDataGrid.SelectedItem != null)
            {
                UpdateClient updateClient = new UpdateClient();
                updateClient.Show();
                this.Close();
            }
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
                    new ClientTableAdapter().DeleteQuery(Convert.ToInt32((membersDataGrid.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
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
                    SurnameOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); if (membersDataGrid.SelectedItem != null)
                    NameOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (membersDataGrid.SelectedItem != null)
                    NumberOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[3].ToString(); if (membersDataGrid.SelectedItem != null)
                    NotesOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[4].ToString(); if (membersDataGrid.SelectedItem != null)
                    EmailOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[5].ToString(); 

    }
            else
            {

            }
        }

       
    }
}