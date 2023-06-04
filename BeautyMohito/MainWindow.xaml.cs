using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BeautyMohito.DbMohitoTableAdapters;
using System.Windows.Controls;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;

namespace BeautyMohito
{

    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito dbMohito;
       //  UserTableAdapter userTableAdapter;
        UserViewTableAdapter userViewTableAdapter;
        string sizeBooking = Booking.sizeSave;
        public static string sizeSave;
        public static string IsFilter;
        public MainWindow()
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
            UserViewTableAdapter adapter = new UserViewTableAdapter();
            DbMohito.UserViewDataTable table = new DbMohito.UserViewDataTable();
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
        
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //UserManipulation userManipulation = new UserManipulation();
            //userManipulation.Show();
        }

        
        private void FilterClick(object sender, RoutedEventArgs e)
        {
            UserViewTableAdapter adapter = new UserViewTableAdapter();
            DbMohito.UserViewDataTable table = new DbMohito.UserViewDataTable();
            adapter.Fill(table);
            membersDataGrid.ItemsSource = table;
            DataView dv = new DataView(table);
            changeColor();


            if (sender == AdminFilter) 
            {
                dv.RowFilter = $@"[Name_role] LIKE  'Админ'";
                AdminFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
                IsFilter = "Админ";
            }


            if (sender == ClientsFilter)
            {
                dv.RowFilter = $@"[Name_role] LIKE  'Клиент'";
                ClientsFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
                IsFilter = "Клиент";
                
            }

            if (sender == EmployeeFilter)
            {
                dv.RowFilter = $@"[Name_role] LIKE  'Сотрудник'";
                EmployeeFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
                IsFilter = "Сотрудник";
            }

            membersDataGrid.ItemsSource = dv.ToTable().DefaultView;

            if (sender == AllUsersFilter)
            {
                AllUsersFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
                dv.RowFilter = $@"[Login] LIKE '%{textBoxFilter.Text}%' ";
                IsFilter = "Все";
            }

           

           
        }



        private void textBoxFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
           
            UserViewTableAdapter adapter = new UserViewTableAdapter();
            DbMohito.UserViewDataTable table = new DbMohito.UserViewDataTable();
            adapter.Fill(table);
            
            DataView dv = new DataView(table);
           

            if (textBoxFilter.Text != null)
            {

                dv.RowFilter = $@"[Login] LIKE '%{textBoxFilter.Text}%' ";


                membersDataGrid.ItemsSource = dv.ToTable().DefaultView;
            }
            
            else
            {
              
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Close();
        }

        private void asdfdd_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PassRecovery passRecovery = new PassRecovery();
            passRecovery.Show();
            this.Close();
        }

       
        private void addDD_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = new Equipment();
            equipment.Show();
            this.Close();
        }

        private void ad_Copy_Click(object sender, RoutedEventArgs e)
        {
            //Statistics statistics = new Statistics();
            //statistics.Show();
            //this.Close();

            Cheque equipment = new Cheque();
            equipment.Show();
            this.Close();
        }

        private void ad_Copcy_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics();
            statistics.Show();
            this.Close();
        }
    }
}