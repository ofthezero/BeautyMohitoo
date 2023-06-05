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
    public partial class Review : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito dbMohito;
        ReviewViewTableAdapter ReviewViewTableAdapter;
        string sizeBooking;

        public static string sizeSave;

        public static string IdOn;
        public static string ReviewOn;
        public static string ReviewTOn;
        public static string DateOn;
       

        public Review()
        {
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Mohito.Properties.Settings.MohitoConnectionString"].ConnectionString.ToString();
            InitializeComponent(); RefreshData(); SaveSize();
        }

        private bool IsMaximize = false;

        public void RefreshData()
        {
            ReviewViewTableAdapter adapter = new ReviewViewTableAdapter();
            DbMohito.ReviewViewDataTable table = new DbMohito.ReviewViewDataTable();
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
            Review booking = new Review();

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
            ReviewViewTableAdapter adapter = new ReviewViewTableAdapter();
            DbMohito.ReviewViewDataTable table = new DbMohito.ReviewViewDataTable();
            adapter.Fill(table);

            DataView dv = new DataView(table);

            if (textBoxFilter.Text != null)
            {
                dv.RowFilter = $@"[Review_discription] LIKE '%{textBoxFilter.Text}%' ";
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
       
        private void UpdateEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateReview updateReview = new UpdateReview();
            updateReview.Show();
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

        private void Cheque_Click(object sender, RoutedEventArgs e)
        {
            Cheque cheque = new Cheque();
            cheque.Show();
            this.Close();
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book();
            book.Show();
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (membersDataGrid.SelectedItem != null)
            {
                UpdateReview updateReview = new UpdateReview();
                updateReview.Show();
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
                    new ReviewTableAdapter().DeleteQuery(Convert.ToInt32((membersDataGrid.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
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
                    ReviewOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (membersDataGrid.SelectedItem != null)
                    ReviewTOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[3].ToString(); if (membersDataGrid.SelectedItem != null)
                    DateOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[4].ToString(); 


          
    }
            else
            {
            

            }
        }

       
    }
}