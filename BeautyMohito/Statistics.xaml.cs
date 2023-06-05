using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BeautyMohito
{
    public partial class Statistics : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito dbMohito;
        ChequePayTableAdapter chequePayTableAdapter;
        string sizeBooking;

        public static string sizeSave;
        public static string IsFilter;
        public static string LoginOn;
        public static string IdOn;
        public static string EmailOn;
        public static string RoleOn;

        public Statistics()
        {
           // con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            InitializeComponent(); RefreshData(); SaveSize();

          
        }


        public void changeColor()
        {
            OneFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            TwoFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            //ThreeFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            //ForsFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");
            //FiveFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#dcdcdc");

        }

        private bool IsMaximize = false;

        public void RefreshData()
        {
            ChequePayTableAdapter adapter = new ChequePayTableAdapter();
            DbMohito.ChequePayDataTable table = new DbMohito.ChequePayDataTable();
            adapter.Fill(table);
           
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

       

        private void FilterClick(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ROBERT;Initial Catalog=Mohito;Integrated Security=True;Encrypt=False;
            TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            DateTime dateN = DateTime.Now;
            dateN = dateN.AddDays(-7);

            DateTime dateD = DateTime.Now;
            dateD = dateD.AddDays(-1);


            changeColor();

            if (sender == OneFilter)
            {
                OneFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
                IsFilter = "2022";

                BookTB.Text = "5"; 
                ProcVir.Text = ""; 

                CashTB.Text = "79000 ₽"; 
                CardTB.Text = "5000 ₽"; 

                PribTB.Text = "1200000 ₽"; 
                ProcPrib.Text = ""; 

                VirTB.Text = "60000 ₽";
                ProcVir.Text = ""; 

                Arrow1.Visibility = Visibility.Collapsed;
                Arrow2.Visibility = Visibility.Collapsed;
                Arrow3.Visibility = Visibility.Collapsed;
                ProcBook.Visibility = Visibility.Collapsed;
                ProcPrib.Visibility = Visibility.Collapsed;
                ProcVir.Visibility = Visibility.Collapsed;
            }


            if (sender == TwoFilter)
            {
                TwoFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
                IsFilter = "2023 год";

                SqlCommand myCommand = new SqlCommand($"Select SUM(Price_service) AS Count from ChequePay", con);
                SqlCommand myCommand1 = new SqlCommand($"Select SUM(Price_service) AS Summ FROM ChequePay GROUP BY Status_pay HAVING(MAX(Status_pay) = 'Эл. платеж')", con);
                SqlCommand myCommand2 = new SqlCommand($"Select SUM(Price_service) AS Summ FROM ChequePay GROUP BY Status_pay HAVING(MAX(Status_pay) = 'Наличные')", con);
                SqlCommand myCommand3 = new SqlCommand($"select round (SUM(Price_service)*0.5, -1) AS Count from ChequePay", con);
                SqlCommand myCommand4 = new SqlCommand($"Select Count(Price_service) AS Count from ChequePay ", con);

                SqlDataReader myReader = null;
                SqlDataReader myReader1 = null;
                SqlDataReader myReader2 = null;
                SqlDataReader myReader3 = null;
                SqlDataReader myReader4 = null;

                con.Open();
                myReader = myCommand.ExecuteReader();
               

                while (myReader.Read())
                {
                    VirTB.Text = (myReader["Count"].ToString() + "₽");
                }
                con.Close();

                con.Open();
                myReader1 = myCommand1.ExecuteReader();
               
                while (myReader1.Read())
                {
                    CardTB.Text = (myReader1["Summ"].ToString() + "₽");
                }
                con.Close();


                con.Open();
                myReader2 = myCommand2.ExecuteReader();

                while (myReader2.Read())
                {
                    CashTB.Text = (myReader2["Summ"].ToString() + "₽");
                }
                con.Close();

                con.Open();
                myReader3 = myCommand3.ExecuteReader();

                while ( myReader3.Read())
                {
                    PribTB.Text = (myReader3["Count"].ToString() + "₽");
                }
                con.Close();

                con.Open();
                myReader4 = myCommand4.ExecuteReader();

                while (myReader4.Read())
                {
                    BookTB.Text = (myReader4["Count"].ToString());
                }
                con.Close();




                ProcBook.Text = "На 5 % больше чем в 2022"; //процент
                ProcPrib.Text = "На 78 % меньше чем в 2022";
                ProcVir.Text = "На 78 % меньше чем в 2022";

                Arrow1.Visibility = Visibility.Visible;
                Arrow2.Visibility = Visibility.Visible;
                Arrow3.Visibility = Visibility.Visible;
                ProcBook.Visibility = Visibility.Visible;
                ProcPrib.Visibility = Visibility.Visible;
                ProcVir.Visibility = Visibility.Visible;
            }

            //if (sender == ThreeFilter)
            //{
            //    DateTime date = DateTime.Now;
            //    date = date.AddMonths(-1);

            //    ThreeFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
            //    IsFilter = "Месяц";

            //    SqlCommand myCommand5 = new SqlCommand($"Select SUM(Price_service) AS Count from ChequePay WHERE [Date_cheque] > '" + date + "' ", con); 
            //    SqlCommand myCommand6 = new SqlCommand($"Select SUM(Price_service) AS Summ FROM ChequePay GROUP BY Status_pay HAVING(MAX(Status_pay) = 'Эл. платеж') WHERE [Date_cheque] > '" + date + "' ", con);
            //    SqlCommand myCommand7 = new SqlCommand($"Select SUM(Price_service) AS Summ FROM ChequePay GROUP BY Status_pay HAVING(MAX(Status_pay) = 'Наличные') WHERE [Date_cheque] > '" + date + "' ", con);
            //    SqlCommand myCommand8 = new SqlCommand($"select (SUM(Price_service)/2 ) AS Count from ChequePay WHERE [Date_cheque] > '" + date + "' ", con);
            //    SqlCommand myCommand9 = new SqlCommand($"Select Count(Price_service) AS Count from ChequePay WHERE (Date_cheque) > '" + date + "' ", con);

            //    SqlDataReader myReader5 = null;
            //    SqlDataReader myReader6 = null;
            //    SqlDataReader myReader7 = null;
            //    SqlDataReader myReader8 = null;
            //    SqlDataReader myReader9 = null;

            //    con.Open();
            //    myReader5 = myCommand5.ExecuteReader();


            //    while (myReader5.Read())
            //    {
            //        VirTB.Text = (myReader5["Count"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader6 = myCommand6.ExecuteReader();

            //    while (myReader6.Read())
            //    {
            //        CardTB.Text = (myReader6["Summ"].ToString());
            //    }
            //    con.Close();


            //    con.Open();
            //    myReader7 = myCommand7.ExecuteReader();

            //    while (myReader7.Read())
            //    {
            //        CashTB.Text = (myReader7["Summ"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader8 = myCommand8.ExecuteReader();

            //    while (myReader8.Read())
            //    {
            //        PribTB.Text = (myReader8["Count"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader9 = myCommand9.ExecuteReader();

            //    while (myReader9.Read())
            //    {
            //        BookTB.Text = (myReader9["Count"].ToString());
            //    }
            //    con.Close();




            //    ProcBook.Text = "На 5 % больше чем в 2022"; //процент
            //    ProcPrib.Text = "На 78 % меньше чем в 2022";
            //    ProcVir.Text = "На 78 % меньше чем в 2022";
            //}


            //if (sender == ForsFilter)
            //{
            //    ForsFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
            //    IsFilter = "Неделя";

            //    SqlCommand myCommand = new SqlCommand($"Select SUM(Price_service) AS Count from ChequePay WHERE (Date_cheque) > '" + dateN + "' ", con);
            //    SqlCommand myCommand1 = new SqlCommand($"Select SUM(Price_service) AS Summ FROM ChequePay GROUP BY Status_pay HAVING(MAX(Status_pay) = 'Эл. платеж') WHERE (Date_cheque) > '" + dateN + "' ", con);
            //    SqlCommand myCommand2 = new SqlCommand($"Select SUM(Price_service) AS Summ FROM ChequePay GROUP BY Status_pay HAVING(MAX(Status_pay) = 'Наличные') WHERE (Date_cheque) > '" + dateN + "' ", con);
            //    SqlCommand myCommand3 = new SqlCommand($"select (SUM(Price_service)/2 ) AS Count from ChequePay WHERE (Date_cheque) > '" + dateN + "' ", con);
            //    SqlCommand myCommand4 = new SqlCommand($"Select Count(Price_service) AS Count from ChequePay WHERE (Date_cheque) > '" + dateN + "' ", con);

            //    SqlDataReader myReader = null;
            //    SqlDataReader myReader1 = null;
            //    SqlDataReader myReader2 = null;
            //    SqlDataReader myReader3 = null;
            //    SqlDataReader myReader4 = null;

            //    con.Open();
            //    myReader = myCommand.ExecuteReader();


            //    while (myReader.Read())
            //    {
            //        VirTB.Text = (myReader["Count"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader1 = myCommand1.ExecuteReader();

            //    while (myReader1.Read())
            //    {
            //        CardTB.Text = (myReader1["Summ"].ToString());
            //    }
            //    con.Close();


            //    con.Open();
            //    myReader2 = myCommand2.ExecuteReader();

            //    while (myReader2.Read())
            //    {
            //        CashTB.Text = (myReader2["Summ"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader3 = myCommand3.ExecuteReader();

            //    while (myReader3.Read())
            //    {
            //        PribTB.Text = (myReader3["Count"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader4 = myCommand4.ExecuteReader();

            //    while (myReader4.Read())
            //    {
            //        BookTB.Text = (myReader4["Count"].ToString());
            //    }
            //    con.Close();




            //    ProcBook.Text = "На 5 % больше чем в 2022"; //процент
            //    ProcPrib.Text = "На 78 % меньше чем в 2022";
            //    ProcVir.Text = "На 78 % меньше чем в 2022";
            //}

            //if (sender == FiveFilter)
            //{
            //    FiveFilter.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#d5bea9");
            //    IsFilter = "День";

            //    SqlCommand myCommand = new SqlCommand($"Select SUM(Price_service) AS Count from ChequePay WHERE (Date_cheque) > '" + dateD + "' ", con);
            //    SqlCommand myCommand1 = new SqlCommand($"Select SUM(Price_service) AS Summ FROM ChequePay GROUP BY Status_pay HAVING(MAX(Status_pay) = 'Эл. платеж') WHERE (Date_cheque) > '" + dateD + "' ", con);
            //    SqlCommand myCommand2 = new SqlCommand($"Select SUM(Price_service) AS Summ FROM ChequePay GROUP BY Status_pay HAVING(MAX(Status_pay) = 'Наличные') WHERE (Date_cheque) > '" + dateD + "' ", con);
            //    SqlCommand myCommand3 = new SqlCommand($"select (SUM(Price_service)/2 ) AS Count from ChequePay WHERE (Date_cheque) > '" + dateD + "' ", con);
            //    SqlCommand myCommand4 = new SqlCommand($"Select Count(Price_service) AS Count from ChequePay WHERE (Date_cheque) > '" + dateD + "' ", con);

            //    SqlDataReader myReader = null;
            //    SqlDataReader myReader1 = null;
            //    SqlDataReader myReader2 = null;
            //    SqlDataReader myReader3 = null;
            //    SqlDataReader myReader4 = null;

            //    con.Open();
            //    myReader = myCommand.ExecuteReader();


            //    while (myReader.Read())
            //    {
            //        VirTB.Text = (myReader["Count"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader1 = myCommand1.ExecuteReader();

            //    while (myReader1.Read())
            //    {
            //        CardTB.Text = (myReader1["Summ"].ToString());
            //    }
            //    con.Close();


            //    con.Open();
            //    myReader2 = myCommand2.ExecuteReader();

            //    while (myReader2.Read())
            //    {
            //        CashTB.Text = (myReader2["Summ"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader3 = myCommand3.ExecuteReader();

            //    while (myReader3.Read())
            //    {
            //        PribTB.Text = (myReader3["Count"].ToString());
            //    }
            //    con.Close();

            //    con.Open();
            //    myReader4 = myCommand4.ExecuteReader();

            //    while (myReader4.Read())
            //    {
            //        BookTB.Text = (myReader4["Count"].ToString());
            //    }
            //    con.Close();




            //    ProcBook.Text = "На 5 % больше чем в 2022"; //процент
            //    ProcPrib.Text = "На 78 % меньше чем в 2022";
            //    ProcVir.Text = "На 78 % меньше чем в 2022";


            //}
        }

      

      

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

        
        private void UpdateUserBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

      

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (MessageBox.Show("Вы уверены, что хотите удалить эти данные? Данные удалятся навсегда, без возможности восстановления", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            //    {

            //    }
            //    else
            //    {
            //        new UserTableAdapter().DeleteQuery(Convert.ToInt32((membersDataGrid.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
            //        RefreshData();
            //    }
            //}
            //catch
            //{
            //}
        }
        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (membersDataGrid.SelectedItem != null)
            //{
            //    if (membersDataGrid.SelectedItem != null)
            //        IdOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[0].ToString(); if (membersDataGrid.SelectedItem != null)
            //        LoginOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); if (membersDataGrid.SelectedItem != null)
            //        EmailOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (membersDataGrid.SelectedItem != null)
            //        RoleOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[4].ToString();
            //}
            //else
            //{
            //    IdOn = null;
            //    LoginOn = null;
            //    EmailOn = null;
            //}
        }

        private void Cheque_Click(object sender, RoutedEventArgs e)
        {
            OneCheque cheque = new OneCheque();
            cheque.Show();
            this.Close();
        }
    }
}