using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace BeautyMohito
{

    public partial class UpdateReview : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; ReviewTableAdapter ReviewTableAdapter;


        string IdOn = Review.IdOn;
        string ReviewOn = Review.ReviewOn;
        string ReviewTOn = Review.ReviewTOn;
        string DateOn = Review.DateOn;
        


        public UpdateReview()
        {
            InitializeComponent(); RefreshData();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            ReviewTableAdapter = new ReviewTableAdapter();
            ReviewTableAdapter.Fill(DbMohito.Review);
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
                ReviewTB.Text = Review.ReviewOn;
                ReviewTextTB.Text = Review.ReviewTOn;
               
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
            Review Review = new Review();
            Review.Show();
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
                if (!String.IsNullOrWhiteSpace(ReviewTB.Text) && Convert.ToInt32(IdETB.SelectedValue) > -1)
                {
                    new ReviewTableAdapter().InsertQuery( Convert.ToInt32(IdETB.SelectedValue), Convert.ToString(ReviewTB.Text), Convert.ToString(ReviewTextTB.Text), DateTime.Now);
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
                if (!String.IsNullOrWhiteSpace(ReviewTB.Text) && Convert.ToInt32(IdETB.SelectedValue) > -1)
                {
                    new ReviewTableAdapter().UpdateQuery(Convert.ToInt32(IdETB.SelectedValue), Convert.ToString(ReviewTB.Text), Convert.ToString(ReviewTextTB.Text), Convert.ToDateTime(DateOn), Convert.ToInt32(IdOn));
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

