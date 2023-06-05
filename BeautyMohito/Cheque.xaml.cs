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
    public partial class Cheque : Window
    {

        SqlConnection con = new SqlConnection();
        DbMohito DbMohito;
        ChequeOnViewTableAdapter chequeOnViewTableAdapter;

        private DispatcherTimer _timer;
        public static int to;

        string sizeBooking;
        public static string sizeSave;

        public static string IdOn;
        public static string DateOn;
        public static string IdBOn;
        public static string IdGOn;
        public Cheque()
        {
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Mohito.Properties.Settings.MohitoConnectionString"].ConnectionString.ToString();
            InitializeComponent(); RefreshData(); SaveSize();

            DbMohito = new DbMohito(); chequeOnViewTableAdapter = new ChequeOnViewTableAdapter();
            chequeOnViewTableAdapter.Fill(DbMohito.ChequeOnView);

            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        public void changeColor()
        {
            //AllUsersFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            //ClientsFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            //EmployeeFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            //AdminFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
        }

        private bool IsMaximize = false;

        public void RefreshData()
        {
            ChequeOnViewTableAdapter adapter = new ChequeOnViewTableAdapter();
            DbMohito.ChequeOnViewDataTable table = new DbMohito.ChequeOnViewDataTable();
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
            //UserViewTableAdapter adapter = new UserViewTableAdapter();
            //DbMohito.UserViewDataTable table = new DbMohito.UserViewDataTable();
            //adapter.Fill(table);

            //DataView dv = new DataView(table);

            //if (textBoxFilter.Text != null)
            //{
            //    dv.RowFilter = $@"[Login] LIKE '%{textBoxFilter.Text}%' ";
            //    membersDataGrid.ItemsSource = dv.ToTable().DefaultView;
            //}
            //else
            //{
            //}
        }


        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Close();
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
            UpdateCheque updateCheque = new UpdateCheque();
            updateCheque.Show();
            this.Close();
        }
        private void Cheque_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            ChequePrint chequePrint = new ChequePrint();
            chequePrint.Show();
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
                    new UserTableAdapter().DeleteQuery(Convert.ToInt32((membersDataGrid.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                }
            }
            catch
            {
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            tb2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        }
        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (membersDataGrid.SelectedItem != null)
            {
                if (membersDataGrid.SelectedItem != null)
                    IdOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[0].ToString(); if (membersDataGrid.SelectedItem != null)
                    DateOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); if (membersDataGrid.SelectedItem != null)
                    IdBOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (membersDataGrid.SelectedItem != null)
                    IdGOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[3].ToString();
            }
            else
            {
              
            }
        }

      
    }
}