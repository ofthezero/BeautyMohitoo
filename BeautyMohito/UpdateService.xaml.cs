using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Input;

namespace BeautyMohito
{

    public partial class UpdateService : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; ServiceTableAdapter ServiceTableAdapter;


        string IdOn = Service.IdOn;
        string NameOn = Service.NameOn;
        string PriceOn = Service.PriceOn;
        string TimeOn = Service.TimeOn;
        string Image = Service.Image;

       


        public UpdateService()
        {
            InitializeComponent(); RefreshData();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            ServiceTableAdapter = new ServiceTableAdapter();
            ServiceTableAdapter.Fill(DbMohito.Service);
        }

        private bool IsMaximize = false;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
            catch { }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
          
           
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
                NameTB.Text = Service.NameOn;
                PriceTB.Text = Service.PriceOn;
                TimeTB.Text = Service.TimeOn;
                

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
            Service service = new Service();
            service.Show();
            this.Close();

            UpdateBtn.Visibility = Visibility.Collapsed;
            UpdateText.Visibility = Visibility.Collapsed;
            InsertBtn.Visibility = Visibility.Visible;
            AddText.Visibility = Visibility.Visible;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(PriceTB.Text)
            //                 && !String.IsNullOrWhiteSpace(TimeTB.Text)  &&
            //                 NameTB.Text.Length >= 4 && PriceTB.Text.Length >= 2 && TimeTB.Text.Length >= 1)
            //    {

            //            new ServiceTableAdapter().InsertQuery(Convert.ToString(NameTB.Text), Convert.ToString(PriceTB.Text),
            //           Convert.ToString(TimeTB.Text));
            //        tb_error.Text = "";
            //        tb_ok.Text = "✔ Данные успешно добавлены";

            //        RefreshData();

            //    }
            //    else
            //    {
            //        tb_ok.Text = "";
            //        tb_error.Text = "⚠ Проверьте правильность  введенных данных";
            //    }
            //}
            //catch
            //{
            //    tb_ok.Text = "";
            //    tb_error.Text = "⚠ Проверьте правильность  введенных данных";
            //}
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(PriceTB.Text)
            //                 && !String.IsNullOrWhiteSpace(TimeTB.Text) &&
            //                 NameTB.Text.Length >= 4 && PriceTB.Text.Length >= 2 && TimeTB.Text.Length >= 1)
            //    {

            //        new ServiceTableAdapter().UpdateQuery(Convert.ToString(NameTB.Text), Convert.ToDecimal(PriceTB.Text),
            //       Convert.ToString(TimeTB.Text), Convert.ToByte(NameTB.Text), Convert.ToInt32(IdOn));
            //        tb_error.Text = "";
            //        tb_ok.Text = "✔ Данные успешно изменены";
            //        RefreshData();
            //    }
            //    else
            //    {
            //        tb_ok.Text = "";
            //        tb_error.Text = "⚠ Проверьте правильность введенных данных";
            //    }
            //}
            //catch
            //{
            //    tb_ok.Text = "";
            //    tb_error.Text = "⚠ Проверьте правильность  введенных данных";
            //}
        }
    }
}

