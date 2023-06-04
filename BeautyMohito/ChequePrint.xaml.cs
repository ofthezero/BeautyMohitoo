using BeautyMohito;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using PrintDialog = System.Windows.Controls.PrintDialog;
using MaterialDesignThemes.Wpf;

namespace BeautyMohito //Менеджер--------------------------------------------------BRAO ПЕЧАТЬ ЧЕКА---------------------------------------------------------
{
    public partial class ChequePrint : Window
    {
        SqlConnection con = new SqlConnection();
        int check = Cheque.to;

        string IdOn = Cheque.IdOn;
        public ChequePrint()
        {
            InitializeComponent(); Times(); ReadCheque();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
                

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Cheque cheque = new Cheque();
            cheque.Show();
            this.Close();
        }

        public void Times()
        {
            var date = DateTime.Now;
            var data1 = DateTime.Now.ToLongDateString();
            var data2 = DateTime.Now.ToShortTimeString();
            vrem.Text = Convert.ToString(data2);
            vrem1.Text = Convert.ToString(data1);
        }

        public void ReadCheque()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=ROBERT;Initial Catalog=Mohito;Integrated Security=True;
            Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand myCommand = new SqlCommand($"SELECT [ID_booking], [Date_cheque], [Status_pay], " +
                $"Price_service, [Name_service]  FROM ChequePay WHERE [ID_cheque] ='" + IdOn + "'", conn); conn.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                NumberBron.Text = myReader["ID_booking"].ToString();
                Pay.Text = myReader["Status_pay"].ToString();
                ChequeNumber.Text = IdOn.ToString();
                ChequeTime.Text = myReader["Date_cheque"].ToString();
                Price.Text = (myReader["Price_service"].ToString() + "" + ".00 ₽");
                Name.Text = myReader["Name_service"].ToString();
            }
        }

       


        //private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        //{
        //    if (e.Key == Key.Escape) back.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //    if (e.Key == Key.P) ppp.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //}
    }
}
