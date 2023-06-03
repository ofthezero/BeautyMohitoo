using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace BeautyMohito
{

    public partial class UpdateEmployee : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; EmployeeTableAdapter EmployeeTableAdapter;


        string IdOn = Employee.IdOn;
        string NameOn = Employee.NameOn;
        string SurnameOn = Employee.SurnameOn;
        string NumberOn = Employee.NumberOn;
        string DateOn = Employee.DateOn;
        string IdUOn = Employee.IdUOn;
        string IdPOn = Employee.IdPOn;


        public UpdateEmployee()
        {
            InitializeComponent(); RefreshData();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            EmployeeTableAdapter = new EmployeeTableAdapter();
            EmployeeTableAdapter.Fill(DbMohito.Employee);
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
            if (NameOn != null)
            {
                NameTB.Text = Employee.NameOn;
                SurnameTB.Text = Employee.SurnameOn;
                PetronymicTB.Text = Employee.PetronymicOn;
                NumberTB.Text = Employee.NumberOn;
                DateDP.Text = Employee.DateOn;
                IdUTB.SelectedItem = Employee.IdUOn;
                IdPTB.SelectedItem = Employee.IdPOn;
                InsertBtn.Visibility = Visibility.Collapsed;
                AddText.Visibility = Visibility.Collapsed;
                UpdateBtn.Visibility = Visibility.Visible;
                UpdateText.Visibility = Visibility.Visible;
            }
            else if (NameOn == null)
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
            Employee Employee = new Employee();
            Employee.Show();
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
                if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(SurnameTB.Text)
                             && !String.IsNullOrWhiteSpace(NumberTB.Text) && !String.IsNullOrWhiteSpace(DateDP.Text) &&
                             NameTB.Text.Length >= 4 && SurnameTB.Text.Length >= 2 && NumberTB.Text.Length >= 7 && Convert.ToInt32(IdUTB.SelectedValue) > -1 && Convert.ToInt32(IdPTB.SelectedValue) > -1)
                {
                    new EmployeeTableAdapter().InsertQuery(Convert.ToString(NameTB.Text), Convert.ToString(SurnameTB.Text), Convert.ToString(PetronymicTB.Text),
                       Convert.ToString(NumberTB.Text), Convert.ToDateTime(DateDP.Text), Convert.ToInt32(IdUTB.SelectedItem), Convert.ToInt32(IdPTB.SelectedItem));
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
                if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(SurnameTB.Text)
                            && !String.IsNullOrWhiteSpace(NumberTB.Text) && !String.IsNullOrWhiteSpace(DateDP.Text) &&
                            NameTB.Text.Length >= 4 && SurnameTB.Text.Length >= 2 && NumberTB.Text.Length >= 7 && Convert.ToInt32(IdUTB.SelectedValue) > -1 && Convert.ToInt32(IdPTB.SelectedValue) > -1)
                {
                    new EmployeeTableAdapter().UpdateQuery(Convert.ToString(NameTB.Text), Convert.ToString(SurnameTB.Text), Convert.ToString(PetronymicTB.Text),
                       Convert.ToString(NumberTB.Text), Convert.ToDateTime(DateDP.Text), Convert.ToInt32(IdUTB.SelectedItem), Convert.ToInt32(IdPTB.SelectedItem), Convert.ToInt32(IdOn));
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

