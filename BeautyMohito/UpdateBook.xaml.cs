using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace BeautyMohito
{

    public partial class UpdateBook : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; BookingTableAdapter BookingTableAdapter;


        string IdOn = Book.IdOn;
        string DateOn = Book.DateOn;
        string IdEOn = Book.IdEOn;
        string IdCOn = Book.IdCOn;
        string IdSOn = Book.IdSOn;
        string StatusOn = Book.StatusOn;
        string StatusPayOn = Book.StatusPayOn;


        public UpdateBook()
        {
            InitializeComponent(); RefreshData();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            BookingTableAdapter = new BookingTableAdapter();
            BookingTableAdapter.Fill(DbMohito.Booking);

            NameEmployeeViewTableAdapter adapterU = new NameEmployeeViewTableAdapter(); //вывод данных - 
            DbMohito.NameEmployeeViewDataTable tableU = new DbMohito.NameEmployeeViewDataTable();
            adapterU.Fill(tableU); IdETB.ItemsSource = tableU;
            IdETB.DisplayMemberPath = "Names";
            IdETB.SelectedValuePath = "ID_employee";

            NameClientViewTableAdapter adapter2 = new NameClientViewTableAdapter(); //вывод данных - 
            DbMohito.NameClientViewDataTable table2 = new DbMohito.NameClientViewDataTable();
            adapter2.Fill(table2); IdCTB.ItemsSource = table2;
            IdCTB.DisplayMemberPath = "Names";
            IdCTB.SelectedValuePath = "ID_client";

            ServiceTableAdapter adapter3 = new ServiceTableAdapter(); //вывод данных -
            DbMohito.ServiceDataTable table3 = new DbMohito.ServiceDataTable();
            adapter3.Fill(table3); IdSTB.ItemsSource = table3;
            IdSTB.DisplayMemberPath = "Name_service";
            IdSTB.SelectedValuePath = "ID_service";

        }

        private bool IsMaximize = false;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
            catch { }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow R = new MainWindow();
            R.Show();
            this.Close();
        }

        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        public void RefreshData()
        {
            if (IdOn != null)
            {
                DateDP.Text = Book.DateOn;
                IdETB.SelectedItem = Book.IdEOn;
                IdCTB.SelectedItem = Book.IdCOn;
                IdSTB.SelectedItem = Book.IdSOn;
                InsertBtn.Visibility = Visibility.Collapsed;
                AddText.Visibility = Visibility.Collapsed;
                UpdateBtn.Visibility = Visibility.Visible;
                UpdateText.Visibility = Visibility.Visible;
            }
            else if (IdOn == null)
            {
                InsertBtn.Visibility = Visibility.Visible;
                AddText.Visibility = Visibility.Visible;
                UpdateBtn.Visibility = Visibility.Collapsed;
                UpdateText.Visibility = Visibility.Collapsed;
            }
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
            Book book = new Book();
            book.Show();
            this.Close();

            UpdateBtn.Visibility = Visibility.Collapsed;
            UpdateText.Visibility = Visibility.Collapsed;
            InsertBtn.Visibility = Visibility.Visible;
            AddText.Visibility = Visibility.Visible;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(DateDP.Text) && Convert.ToInt32(IdETB.SelectedValue) > -1 && Convert.ToInt32(IdCTB.SelectedValue) > -1 && Convert.ToInt32(IdSTB.SelectedValue) > -1)
                {
                    new BookingTableAdapter().InsertQuery(Convert.ToString(DateDP.Text),Convert.ToInt32(IdETB.SelectedItem), Convert.ToInt32(IdCTB.SelectedItem), Convert.ToInt32(IdSTB.SelectedItem), "", "Не оплачен");
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно добавлены";

                    RefreshData();
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

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(DateDP.Text) && Convert.ToInt32(IdETB.SelectedValue) > -1 && Convert.ToInt32(IdCTB.SelectedValue) > -1 && Convert.ToInt32(IdSTB.SelectedValue) > -1)
                {
                    new BookingTableAdapter().UpdateQuery(Convert.ToString(DateDP.Text), Convert.ToInt32(IdETB.SelectedItem), Convert.ToInt32(IdCTB.SelectedItem), Convert.ToInt32(IdSTB.SelectedItem), "", "Не оплачен", Convert.ToInt32(IdOn));
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно изменены";
                    RefreshData();
                }
                else
                {
                    tb_ok.Text = "";
                    tb_error.Text = "⚠ Проверьте правильность введенных данных";
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

