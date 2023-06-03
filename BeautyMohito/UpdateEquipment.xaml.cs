using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace BeautyMohito
{

    public partial class UpdateEquipment : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; EquipmentTableAdapter EquipmentTableAdapter;


        string IdOn = Equipment.IdOn;
        string NameOn = Equipment.NameOn;
        string QuantityOn = Equipment.QuantityOn;
        string CostOn = Equipment.CostOn;
        string IdEOn = Equipment.IdEOn;
        string LifeOn = Equipment.LifeOn;
        string StatusOn = Equipment.StatusOn;


        public UpdateEquipment()
        {
            InitializeComponent(); RefreshData();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            EquipmentTableAdapter = new EquipmentTableAdapter();
            EquipmentTableAdapter.Fill(DbMohito.Equipment);
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
                NameTB.Text = Equipment.NameOn;
                QuantityTB.Text = Equipment.QuantityOn;
                CostTB.Text = Equipment.CostOn;
                IdETB.SelectedItem = Equipment.IdEOn;
                LifeDP.Text = Equipment.LifeOn;
                StatusTB.Text = Equipment.StatusOn;
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
            Equipment Equipment = new Equipment();
            Equipment.Show();
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
                if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(QuantityTB.Text) && !String.IsNullOrWhiteSpace(CostTB.Text) 
                    && !String.IsNullOrWhiteSpace(IdETB.Text) && !String.IsNullOrWhiteSpace(LifeDP.Text) && !String.IsNullOrWhiteSpace(StatusTB.Text) &&
                    NameTB.Text.Length >= 4 && CostTB.Text.Length >= 2 && StatusTB.Text.Length >= 5 && QuantityTB.Text.Length >= 1 && IdETB.Text.Length != -1)
                {
                    new EquipmentTableAdapter().InsertQuery(Convert.ToString(NameTB.Text), Convert.ToInt32(QuantityTB.Text), Convert.ToDecimal(CostTB.Text),
                        Convert.ToInt32(IdETB.SelectedItem), Convert.ToDateTime(LifeDP.Text), Convert.ToString(StatusTB.Text));
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно добавлены";

                    RefreshData();
                    NameTB.Text = null;
                    QuantityTB.Text = null;
                    CostTB.Text = null;
                    IdETB.SelectedItem = null;
                    LifeDP.Text = null;
                    StatusTB.Text = null;
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
                if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(QuantityTB.Text) && !String.IsNullOrWhiteSpace(CostTB.Text)
                            && !String.IsNullOrWhiteSpace(IdETB.Text) && !String.IsNullOrWhiteSpace(LifeDP.Text) && !String.IsNullOrWhiteSpace(StatusTB.Text) &&
                            NameTB.Text.Length >= 4 && CostTB.Text.Length >= 2 && StatusTB.Text.Length >= 5 && QuantityTB.Text.Length >= 1 && IdETB.Text.Length != -1)
                {
                    new EquipmentTableAdapter().UpdateQuery(Convert.ToString(NameTB.Text), Convert.ToInt32(QuantityTB.Text), Convert.ToDecimal(CostTB.Text),
                       Convert.ToInt32(IdETB.SelectedItem), Convert.ToDateTime(LifeDP.Text), Convert.ToString(StatusTB.Text), Convert.ToInt32(IdOn));
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

