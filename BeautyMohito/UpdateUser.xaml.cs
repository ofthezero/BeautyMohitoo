using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace BeautyMohito
{

    public partial class UpdateUser : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; UserTableAdapter userTableAdapter;

        string IdOn = User.IdOn;
        string LoginOn = User.LoginOn;
        string EmailOn = User.EmailOn;
        string RoleOn = User.RoleOn;
        public UpdateUser()
        {
            InitializeComponent(); RefreshData();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            userTableAdapter = new UserTableAdapter();
            userTableAdapter.Fill(DbMohito.User);

            RoleTableAdapter adapterU = new RoleTableAdapter(); //вывод данных - почта
            DbMohito.RoleDataTable tableU = new DbMohito.RoleDataTable();
            adapterU.Fill(tableU); RoleCB.ItemsSource = tableU;
            RoleCB.DisplayMemberPath = "Name_role";
            RoleCB.SelectedValuePath = "ID_role";
        }

        private bool IsMaximize = false;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
            catch { }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }

        public void RefreshData()
        {
            if (LoginOn != null)
            {
                LoginTB.Text = LoginOn;
                EmailTB.Text = EmailOn;
                RoleCB.SelectedValue = RoleOn;
                InsertBtn.Visibility = Visibility.Collapsed;
                AddText.Visibility = Visibility.Collapsed;
                UpdateBtn.Visibility = Visibility.Visible;
                UpdateText.Visibility = Visibility.Visible;
            }
            else if (LoginOn == null)
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
            User user = new User();
            user.Show();
            this.Close();

            UpdateBtn.Visibility = Visibility.Collapsed;
            UpdateText.Visibility = Visibility.Collapsed;
            InsertBtn.Visibility = Visibility.Visible;
            AddText.Visibility = Visibility.Visible;
            LoginOn = null;
            EmailOn = null;
            PasswordTB.Text = null;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(LoginTB.Text) && !String.IsNullOrWhiteSpace(PasswordTB.Text) && !String.IsNullOrWhiteSpace(EmailTB.Text) && LoginTB.Text.Length >= 4 && PasswordTB.Text.Length >= 6 && EmailTB.Text.Length >= 7 && Convert.ToInt32(RoleCB.SelectedValue) > -1)
                {
                    new UserTableAdapter().InsertQuery(Convert.ToString(LoginTB.Text), Shif.passwordShif(PasswordTB.Text), Convert.ToString(EmailTB.Text), Convert.ToInt32(RoleCB.SelectedValue));
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно добавлены";
                    RefreshData();
                    LoginTB.Clear();
                    PasswordTB.Clear();
                    EmailTB.Clear();
                    RoleCB.SelectedValue = -1;
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
                if (!String.IsNullOrWhiteSpace(LoginTB.Text) && !String.IsNullOrWhiteSpace(PasswordTB.Text) && !String.IsNullOrWhiteSpace(EmailTB.Text) && LoginTB.Text.Length >= 4 && PasswordTB.Text.Length >= 6 && EmailTB.Text.Length >= 7 && Convert.ToInt32(RoleCB.SelectedValue) > -1)
                {
                    new UserTableAdapter().UpdateQuery(Convert.ToString(LoginTB.Text), Shif.passwordShif(PasswordTB.Text), Convert.ToString(EmailTB.Text), Convert.ToInt32(RoleCB.SelectedValue), Convert.ToInt32(IdOn));
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

