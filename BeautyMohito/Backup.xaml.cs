using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace BeautyMohito
{
    public partial class Backup : Window
    {
        SqlConnection con = new SqlConnection();
        public Backup()
        {
            InitializeComponent();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Thebrao.Properties.Settings.ThebraoConnectionString"].ConnectionString.ToString();
            RE.IsEnabled = false;
            Voss.IsEnabled = false;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private static void BckpMohitp(string connection, string mohito, string fileMohito)
        {
            try
            {
                var cmd = "BACKUP DATABASE @Mohito TO DISK = @FileMohito";

                using (var conn = new SqlConnection(connection))
                using (var cmmd = new SqlCommand(cmd, conn))
                {
                    conn.Open();

                    cmmd.Parameters.AddWithValue("@Mohito", mohito);
                    cmmd.Parameters.AddWithValue("@FileMohito", fileMohito);

                    cmmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        private void Brow_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog().ToString().Equals("OK"))
            {
                tb1.Text = dialog.SelectedPath;
                RE.IsEnabled = true;
            }
        }

        private void RE_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text == string.Empty)
            {
                tb_ok.Text = "";
                tb_error.Text = "⚠ Выберите папку для установления резервной копии";
            }
            else
            {
                BckpMohitp("Server=ROBERT;Database=Mohito; Trusted_Connection=True; MultipleActiveResultSets=true", "Mohito", tb1.Text + "\\" + "MohitoDB" + " " + DateTime.Now.ToString("dd.MM.yyyy--HH-mm-ss") + ".bak");
                RE.IsEnabled = false;
                tb1.Text = "";


                tb_error.Text = "";
                tb_ok.Text = "✔ Резервная копия успешно создана";
            }
        }
        private void Brow2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog odialog = new OpenFileDialog();
            
            odialog.Filter = "files .bak|(.bak)";
            odialog.Title = "Database restore";

            if (odialog.ShowDialog().ToString().Equals("OK"))
            {
                tb2.Text = odialog.FileName;
                Voss.IsEnabled = true;
            }
        }

        private void Voss_Click(object sender, RoutedEventArgs e)
        {
            string database = con.Database.ToString();
            con.Open();

            try
            {
                string str1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();

                string str2 = "USE MASTER RESTORE DATABASE [" + database + "]   FROM DISK= '" + tb2.Text + "' WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                cmd2.ExecuteNonQuery();

                string str3 = "ALTER DATABASE [" + database + "]  SET MULTI_USER";
                SqlCommand cmd3 = new SqlCommand(str3, con);
                cmd3.ExecuteNonQuery();

                Voss.IsEnabled = false;
                tb2.Text = "";


                tb_error.Text = "";
                tb_ok.Text = "✔ База данных восстановлено";

                con.Close();
            }
            catch
            {
                tb_ok.Text = "";
                tb_error.Text = "⚠ Проверьте файл .dat - Возможно был выбран не тот файл";
            }
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

        
        private void Roles_Click(object sender, RoutedEventArgs e)
        {
            Roles roles = new Roles();
            roles.Show();
            this.Close();
        }
    }
}
