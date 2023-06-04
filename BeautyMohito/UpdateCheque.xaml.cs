using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BeautyMohito
{

    public partial class UpdateCheque : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; ChequeTableAdapter chequeTableAdapter;

        string IdOn = Roles.IdOn;
        string NameOn = Roles.NameOn;

        private DispatcherTimer _timer;
        public static int to;

        public UpdateCheque()
        {
            InitializeComponent(); 
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            DbMohito = new DbMohito(); chequeTableAdapter = new ChequeTableAdapter();
            chequeTableAdapter.Fill(DbMohito.Cheque);

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();

            BookingTableAdapter adapterU = new BookingTableAdapter(); //вывод данных - 
            DbMohito.BookingDataTable tableU = new DbMohito.BookingDataTable();
            adapterU.Fill(tableU); IdBTB.ItemsSource = tableU;
            IdBTB.DisplayMemberPath = "DateTime";
            IdBTB.SelectedValuePath = "ID_booking";

            GoodsTableAdapter adapter2 = new GoodsTableAdapter(); //вывод данных - 
            DbMohito.GoodsDataTable table2 = new DbMohito.GoodsDataTable();
            adapter2.Fill(table2); IdGTB.ItemsSource = table2;
            IdGTB.DisplayMemberPath = "Name_goods";
            IdGTB.SelectedValuePath = "ID_goods";
        }

        private bool IsMaximize = false;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
            catch { }
        }

        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

      

        private void tb11_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Cheque cheque = new Cheque();
            cheque.Show();
            this.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string status = (radioButton_Карта.IsChecked == true) ? "Эл. платеж" : "Наличные";
                if ((Convert.ToInt32(IdBTB.SelectedValue) != -1 || Convert.ToInt32(IdGTB.SelectedValue) != -1) && radioButton_Карта.IsChecked == true || radioButton_Наличные.IsChecked == true)
                {
                    new ChequeTableAdapter().InsertQuery(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")), Convert.ToInt32(IdBTB.SelectedValue), Convert.ToInt32(IdGTB.SelectedValue));
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Оплачено";

                    //изменение статуса комнаты в таблице Register
                    new BookingTableAdapter().UpdateQuery1(status, (int)IdBTB.SelectedValue);
                    
                    IdBTB.SelectedValue = -1;
                    radioButton_Карта.IsChecked = false;
                    radioButton_Наличные.IsChecked = false;


                    if (radioButton_Наличные.IsChecked == true && radioButton_Карта.IsChecked == false)
                    {
                        new BookingTableAdapter().UpdateQuery1("Наличные", (int)IdBTB.SelectedValue);

                    }
                    if (radioButton_Карта.IsChecked == true && radioButton_Наличные.IsChecked == false)
                    {
                        new BookingTableAdapter().UpdateQuery1("Эл. платеж", (int)IdBTB.SelectedValue);

                    }
                    else { }
                }
                else
                {
                    tb_ok.Text = "";
                    tb_error.Text = "⚠ Проверьте правильность  введенных данных";
                }
            }
            catch
            {
                tb_ok.Text = "";
                tb_error.Text = "⚠ Проверьте правильность  введенных данных";
            }
        }
    }
}

