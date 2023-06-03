using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BeautyMohito
{
    public partial class AuthEmail : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; UserTableAdapter userTableAdapter;

        string randomcode;
        public static string to;
        public AuthEmail()
        {
            InitializeComponent();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DbMohito.User);

            verify.IsEnabled = false;
            tb11.IsEnabled = false;
            tb12.IsEnabled = false;
            tb13.IsEnabled = false;
            tb14.IsEnabled = false;

            codetext.Visibility = Visibility.Collapsed;
            codetext1.Visibility = Visibility.Collapsed;
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

        private void code_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=ROBERT;Initial Catalog=Mohito;Integrated Security=True;Connect Timeout=30;Encrypt=False;
            TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand myCommand = new SqlCommand($"SELECT Email FROM[User] ", conn); conn.Open();
            SqlDataReader myReader = myCommand.ExecuteReader();

            try
            {
                while (myReader.Read())
                {
                    if (myReader["Email"].ToString() == textBox1.Text)
                    {
                        string from, pass, messagebody; Random rand = new Random();
                        randomcode = rand.Next(9999).ToString(); //выбор рандомного числа
                        MailMessage message = new MailMessage();

                        to = textBox1.Text.ToString(); //получение эл. почты получателя (сотрудника)
                        from = "itsatestpo@mail.ru"; //логин эл. почты отправителя (администратора)
                        pass = "UB5RP35GpQTBMZRcthsD"; //пароль эл. почты отправителя (администратора)

                        messagebody = $" Ваш код восстановления пароля: <b> {randomcode} </b> <br> от BRAO <br> <br> <em> Пожалуйста, проигнорируйте" +
                        $" данное письмо, если оно попало к Вам по ошибке. </em>  <br> <br> --<br> С уважением <br> Служба поддержки Mohito"; //сообщение

                        message.To.Add(to);
                        message.From = new MailAddress(from);
                        message.Body = messagebody;
                        message.Subject = "Служба поддержки BRAO"; //заголовок

                        SmtpClient smtp = new SmtpClient("smtp.mail.ru");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(from, pass);

                        message.IsBodyHtml = true;

                        smtp.Send(message); //отправка кода

                        tb_error.Text = ""; //вывод об отправке сообщения

                        UserIcon.Visibility = Visibility.Collapsed;
                        verify.IsEnabled = true;
                        tb11.IsEnabled = true;
                        tb12.IsEnabled = true;
                        tb13.IsEnabled = true;
                        tb14.IsEnabled = true;


                        tb11.Focus();

                        textBox1.Visibility = Visibility.Collapsed;
                        codetext.Visibility = Visibility.Visible;
                        codetext1.Visibility = Visibility.Visible;
                        codetext.Text = "✔ Код подтверждения отправлен на эл. почту";
                        codebtn.Visibility = Visibility.Collapsed;
                        codetext1.Text = textBox1.Text;
                    }
                    con.Close();
                }
            }
            catch
            {
                tb_error.Text = "⚠ Эл. почта не найдена";
            } //вывод об ошибке отправки сообщения
        }
        private void verify_Click(object sender, RoutedEventArgs e) //проверка кода
        {
            var code = tb11.Text + "" + tb12.Text + "" + tb13.Text + "" + tb14.Text +
                "";
            if (randomcode == code.ToString())
            {
                to = textBox1.Text;
                PassRecovery passRecovery = new PassRecovery();
                this.Hide();
                passRecovery.Show();
            }
            else
            {
                tb_error.Text = "⚠ Проверьте правильность ввода кода";
                b1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFC6060");
                b2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFC6060");
                b3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFC6060");
                b4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFC6060");

                tb11.Clear();
                tb12.Clear();
                tb13.Clear();
                tb14.Clear();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void textBox2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        public void RefreshData()
        {
            tb11.Focus();
            b1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#F0F0F0");
            b2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#F0F0F0");
            b3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#F0F0F0");
            b4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#F0F0F0");

            tb11.Clear();
            tb12.Clear();
            tb13.Clear();
            tb14.Clear();
        }

        private void tb11_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            b1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFB9F3B1");
            if (!String.IsNullOrWhiteSpace(tb11.Text))
                tb12.Focus();
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        private void tb12_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            b2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFB9F3B1");
            if (!String.IsNullOrWhiteSpace(tb12.Text))
                tb13.Focus();

            if (e.Key == Key.Back)
            {
                RefreshData();
            }

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tb13_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            b3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFB9F3B1");
            if (!String.IsNullOrWhiteSpace(tb13.Text))
                tb14.Focus();
            b4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFB9F3B1");

            if (e.Key == Key.Back)
            {
                RefreshData();
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void tb14_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            b4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFB9F3B1");

            if (e.Key == Key.Back)
            {
                RefreshData();
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
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
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }
    }
}