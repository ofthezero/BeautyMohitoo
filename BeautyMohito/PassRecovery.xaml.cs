using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace BeautyMohito
{

    public partial class PassRecovery : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito;
        UserTableAdapter userTableAdapter;
        string sizeBooking = Booking.sizeSave;

        public static string sizeSave;
        public static string IsFilter;
        string Login = AuthEmail.to;
        public PassRecovery()
        {
            InitializeComponent(); SaveSize();

            DbMohito = new DbMohito(); userTableAdapter = new UserTableAdapter(); userTableAdapter.Fill(DbMohito.User);
        }

        private bool IsMaximize = false;

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onePreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
        private void onePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 @.-_".IndexOf(e.Text) < 0;
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e) //ПЕРВЫЙ
        {
            string password = Shif.passwordShif(Password.Password);
            try
            {
                if (((Password.Password == Passwords.Password || Pro.Text == Passwords.Password || Pro.Text == Pros.Text || Password.Password == Pros.Text) &&
               (Password.Password.Length >= 6 || Pro.Text.Length >= 6) && Password.Password != null && Passwords.Password != null) || (Pro.Text != null && Pros.Text != null))
                {
                    SqlConnection conn = new SqlConnection(@"Data Source=ROBERT;Initial Catalog=Mohito;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    string q = "update [User] set [Password]='" + password + "' where Email='" + Login + "'";
                    SqlCommand cmd = new SqlCommand(q, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    tb_error.Text = "";
                    this.Close();

                    MainWindow R = new MainWindow();
                    R.Show();
                    this.Close();
                }
                else
                {
                    tb_error.Text = "⚠ Пароли не совподают или не соответствуют требованиям";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateOn_Click(object sender, RoutedEventArgs e) //ВТОРОЙ 
        {
            string password = Shif.passwordShif(Pro.Text);

            try
            {
                if (Password.Password == null || Passwords.Password == null || Pro.Text == null || Pros.Text == null)
                {
                    tb_error.Text = "⚠ Поля не заполнены";
                }
                else
                {
                    if ((Password.Password == Passwords.Password || Pro.Text == Passwords.Password || Pro.Text == Pros.Text || Password.Password == Pros.Text) &&
                (Password.Password.Length >= 6 || Pro.Text.Length >= 6))

                    {
                        SqlConnection conn = new SqlConnection(@"Data Source=ROBERT;Initial Catalog=Mohito;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                        string q = "update [User] set [Password]='" + password + "' where Email='" + Login + "'";
                        SqlCommand cmd = new SqlCommand(q, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        tb_error.Text = "";
                        this.Close();

                        MainWindow R = new MainWindow();
                        R.Show();
                        this.Close();
                    }
                    else
                    {
                        tb_error.Text = "⚠ Пароли не совподают или не соответствуют треjбованиям";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

        private void Eyeoff_Click(object sender, RoutedEventArgs e) 
        {
            Pro.Text = Password.Password;
            Password.Visibility = Visibility.Visible;
            Pro.Visibility = Visibility.Collapsed;
            UpdateOn.Visibility = Visibility.Visible;
            Update.Visibility = Visibility.Collapsed;
            Eye.Visibility = Visibility.Visible;
            Eyeoff.Visibility = Visibility.Collapsed;
            eye.Visibility = Visibility.Visible;
            eyeoff.Visibility = Visibility.Collapsed;
        }

        private void Eye_Click(object sender, RoutedEventArgs e) 
        {
            Pro.Text = Password.Password;
            Password.Visibility = Visibility.Collapsed;
            Pro.Visibility = Visibility.Visible;
            UpdateOn.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Visible;
            Eye.Visibility = Visibility.Collapsed;
            Eyeoff.Visibility = Visibility.Visible;
            eye.Visibility = Visibility.Collapsed;
            eyeoff.Visibility = Visibility.Visible;
        }

        private void Eyeoffs_Click(object sender, RoutedEventArgs e)
        {
            Pros.Text = Passwords.Password;
            Passwords.Visibility = Visibility.Visible;
            Pros.Visibility = Visibility.Collapsed;

            Eyes.Visibility = Visibility.Visible;
            Eyeoff.Visibility = Visibility.Collapsed;
            eyes.Visibility = Visibility.Visible;
            eyeoffs.Visibility = Visibility.Collapsed;
        }

        private void Eyes_Click(object sender, RoutedEventArgs e)
        {
            Pros.Text = Passwords.Password;
            Passwords.Visibility = Visibility.Collapsed;
            Pros.Visibility = Visibility.Visible;

            Eyes.Visibility = Visibility.Collapsed;
            Eyeoffs.Visibility = Visibility.Visible;
            eyes.Visibility = Visibility.Collapsed;
            eyeoffs.Visibility = Visibility.Visible;
        }
    }
}