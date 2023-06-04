using BeautyMohito.DbMohitoTableAdapters;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BeautyMohito
{
    public partial class Auth : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito;
        UserTableAdapter userTableAdapter;
        string sizeBooking = Booking.sizeSave;

        public static string sizeSave;
        public static string IsFilter;
        public Auth()
        {
            InitializeComponent(); SaveSize();
            //  con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito(); userTableAdapter = new UserTableAdapter(); userTableAdapter.Fill(DbMohito.User);
        }

        private bool IsMaximize = false;
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string importEmail = Email.Text;
            string importPassword = Shif.passwordShif(Password.Password);

            for (int i = 0; i < DbMohito.Tables["User"].Rows.Count; i++)
            {
                if (importEmail == DbMohito.Tables["User"].Rows[i]["Login"].ToString() | 
                    importEmail == DbMohito.Tables["User"].Rows[i]["Email"].ToString() &&
                    importPassword == DbMohito.Tables["User"].Rows[i]["Password"].ToString())
                {
                    string roleID = DbMohito.Tables["User"].Rows[i]["role_ID"].ToString();

                    switch (roleID)
                    {
                        case "1":
                            {
                                Backup admin = new Backup();
                                admin.Show();
                                this.Close();
                                break;
                            }

                        case "3":
                            {
                                Book book = new Book();
                                book.Show();
                                this.Close();
                                break;
                            }

                        case "4":
                            {
                                Employee employee = new Employee();
                                employee.Show();
                                this.Close();
                                break;
                            }

                        case "5":
                            {
                                Equipment pmanager = new Equipment();
                                pmanager.Show();
                                this.Close();
                                break;
                            }

                        case "6":
                            {
                                Statistics statistics = new Statistics();
                                statistics.Show();
                                this.Close();
                                break;
                            }


                        default: break;
                    }
                }
                else { tb_error.Text = "⚠  Проверьте правильность введенных данных"; }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void pass_MouseEnter(object sender, MouseEventArgs e)
        {
            Pro.Text = Password.Password;
            Password.Visibility = Visibility.Collapsed;
            Pro.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Collapsed;
            btnLogin2.Visibility = Visibility.Visible;
        }

        private void btnLogin_Click2(object sender, RoutedEventArgs e)
        {
            string importEmail = Email.Text;
            string importPassword = Shif.passwordShif(Pro.Text);

            for (int i = 0; i < DbMohito.Tables["User"].Rows.Count; i++)
            {

                if (importEmail == DbMohito.Tables["User"].Rows[i]["Login"].ToString() | importEmail == DbMohito.Tables["User"].Rows[i]["Email"].ToString()
                    && importPassword == DbMohito.Tables["User"].Rows[i]["Password"].ToString())
                {
                    string roleID = DbMohito.Tables["User"].Rows[i]["role_ID"].ToString();

                    switch (roleID)
                    {
                        case "1":
                            {
                                Backup admin = new Backup();
                                admin.Show();
                                this.Close();
                                break;
                            }

                        case "3":
                            {
                                Book book = new Book();
                                book.Show();
                                this.Close();
                                break;
                            }

                        case "4":
                            {
                                Employee employee = new Employee();
                                employee.Show();
                                this.Close();
                                break;
                            }

                        case "5":
                            {
                                Equipment pmanager = new Equipment();
                                pmanager.Show();
                                this.Close();
                                break;
                            }

                        case "6":
                            {
                                Statistics statistics = new Statistics();
                                statistics.Show();
                                this.Close();
                                break;
                            }
                        default: break;
                    }
                }
                else { tb_error.Text = "⚠  Проверьте правильность введенных данных"; }
            }
        }

        private void Eyeoff_Click(object sender, RoutedEventArgs e) 
        {
            Pro.Text = Password.Password;
            Password.Visibility = Visibility.Visible;
            Pro.Visibility = Visibility.Collapsed;
            btnLogin.Visibility = Visibility.Visible;
            btnLogin2.Visibility = Visibility.Collapsed;
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
            btnLogin.Visibility = Visibility.Collapsed;
            btnLogin2.Visibility = Visibility.Visible;
            Eye.Visibility = Visibility.Collapsed;
            Eyeoff.Visibility = Visibility.Visible;
            eye.Visibility = Visibility.Collapsed;
            eyeoff.Visibility = Visibility.Visible;
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

        private void ValidateUser_Click(object sender, RoutedEventArgs e)
        {
            AuthEmail authEmail = new AuthEmail();
            authEmail.Show();
            this.Close();
        }
    }
}