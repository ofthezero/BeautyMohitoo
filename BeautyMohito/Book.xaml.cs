using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static BeautyMohito.DbMohito;

namespace BeautyMohito
{
    public partial class Book : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito dbMohito;
        BookingViewTableAdapter bookingViewTableAdapter;
        string sizeBooking = Booking.sizeSave;

        public static string sizeSave;
        public static string IsFilter;

        public static string IdOn;
        public static string DateOn;
        public static string IdEOn;
        public static string IdCOn;
        public static string IdSOn;
        public static string StatusOn; 
        public static string StatusPayOn;

        public Book()
        {
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Mohito.Properties.Settings.MohitoConnectionString"].ConnectionString.ToString();
            InitializeComponent(); RefreshData(); SaveSize();
        }

        public void changeColor()
        {
            AllUsersFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            ClientsFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            EmployeeFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            AdminFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
        }

        private bool IsMaximize = false;

        public void RefreshData()
        {
            BookingViewTableAdapter adapter = new BookingViewTableAdapter();
            DbMohito.BookingViewDataTable table = new DbMohito.BookingViewDataTable();
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

        private void FilterClick(object sender, RoutedEventArgs e)
        {
            //BookingViewTableAdapter adapter = new BookingViewTableAdapter();
            //DbMohito.BookingViewDataTable table = new DbMohito.BookingViewDataTable();
            //adapter.Fill(table);
            //membersDataGrid.ItemsSource = table;
            //DataView dv = new DataView(table);
            //changeColor();


            //if (sender == AdminFilter)
            //{
            //    dv.RowFilter = $@"[Name_role] LIKE  'Админ'";
            //    AdminFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
            //    IsFilter = "Админ";
            //}


            //if (sender == ClientsFilter)
            //{
            //    dv.RowFilter = $@"[Name_role] LIKE  'Клиент'";
            //    ClientsFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
            //    IsFilter = "Клиент";

            //}

            //if (sender == EmployeeFilter)
            //{
            //    dv.RowFilter = $@"[Name_role] LIKE  'Сотрудник'";
            //    EmployeeFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
            //    IsFilter = "Сотрудник";
            //}

            //membersDataGrid.ItemsSource = dv.ToTable().DefaultView;

            //if (sender == AllUsersFilter)
            //{
            //    AllUsersFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
            //    dv.RowFilter = $@"[Login] LIKE '%{textBoxFilter.Text}%' ";
            //    IsFilter = "Все";
            //}
        }

        private void textBoxFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            BookingViewTableAdapter adapter = new BookingViewTableAdapter();
            DbMohito.BookingViewDataTable table = new DbMohito.BookingViewDataTable();
            adapter.Fill(table);

            DataView dv = new DataView(table);

            if (textBoxFilter.Text != null)
            {
                dv.RowFilter = $@"[ClientName] LIKE '%{textBoxFilter.Text}%' ";
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

        private void Cheque_Click(object sender, RoutedEventArgs e)
        {
            Cheque cheque = new Cheque();
            cheque.Show();
            this.Close();
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            Review review = new Review();
            review.Show();
            this.Close();
        }
        private void UpdateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateBook updateBook = new UpdateBook();
            updateBook.Show();
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (membersDataGrid.SelectedItem != null)
            {
                UpdateBook updateBook = new UpdateBook();
                updateBook.Show();
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
                    new BookingTableAdapter().DeleteQuery(Convert.ToInt32((membersDataGrid.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
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
                    DateOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); if (membersDataGrid.SelectedItem != null)
                    IdEOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (membersDataGrid.SelectedItem != null)
                    IdCOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[4].ToString(); if (membersDataGrid.SelectedItem != null)
                    IdSOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[6].ToString(); if (membersDataGrid.SelectedItem != null)
                    StatusOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[8].ToString(); if (membersDataGrid.SelectedItem != null)
                    StatusPayOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[9].ToString(); 
            }
            else
            {
               
            }
        }

       
    }
}