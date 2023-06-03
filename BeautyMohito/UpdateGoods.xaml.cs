using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace BeautyMohito
{

    public partial class UpdateGoods : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito DbMohito; GoodsTableAdapter goodsTableAdapter;


        string IdOn = Goods.IdOn;
        string NameOn = Goods.NameOn;
        string CostOn = Goods.CostOn;
        string PriceOn = Goods.PriceOn;
        string QuantityOn = Goods.QuantityOn;
        string DateOn = Goods.DateOn;
        
        public UpdateGoods()
        {
            InitializeComponent(); RefreshData();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["BeautyMohito.Properties.Settings.BeautyMohitoConnectionString"].ConnectionString.ToString();
            DbMohito = new DbMohito();
            goodsTableAdapter = new GoodsTableAdapter();
            goodsTableAdapter.Fill(DbMohito.Goods);
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
                NameTB.Text = Goods.NameOn;
                CostTB.Text = Goods.CostOn;
                PriceTB.Text = Goods.PriceOn;
                QuantityTB.Text = Goods.QuantityOn;
                DateDP.Text = Goods.DateOn;
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
            Goods goods = new Goods();
            goods.Show();
            this.Close();

            UpdateBtn.Visibility = Visibility.Collapsed;
            UpdateText.Visibility = Visibility.Collapsed;
            InsertBtn.Visibility = Visibility.Visible;
            AddText.Visibility = Visibility.Visible;
            NameTB.Text = null;
            CostTB.Text = null;
            PriceTB.Text = null;
            QuantityTB.Text = null;
            DateDP.Text = null;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(CostTB.Text) && !String.IsNullOrWhiteSpace(PriceTB.Text) && !String.IsNullOrWhiteSpace(QuantityTB.Text) 
                    && !String.IsNullOrWhiteSpace(PriceTB.Text) && !String.IsNullOrWhiteSpace(DateDP.Text) &&
                    NameTB.Text.Length >= 4 && CostTB.Text.Length >= 2 && PriceTB.Text.Length >= 2 && QuantityTB.Text.Length >= 1)
                {
                    new GoodsTableAdapter().InsertQuery(Convert.ToString(NameTB.Text), Convert.ToDecimal(CostTB.Text), Convert.ToDecimal(PriceTB.Text), 
                        Convert.ToInt32(QuantityTB.Text), Convert.ToDateTime(DateDP.Text));
                    tb_error.Text = "";
                    tb_ok.Text = "✔ Данные успешно добавлены";
                   
                    RefreshData();
                    NameTB.Text = null;
                    CostTB.Text = null;
                    PriceTB.Text = null;
                    QuantityTB.Text = null;
                    DateDP.Text = null;
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
                if (!String.IsNullOrWhiteSpace(NameTB.Text) && !String.IsNullOrWhiteSpace(CostTB.Text) && !String.IsNullOrWhiteSpace(PriceTB.Text) && !String.IsNullOrWhiteSpace(QuantityTB.Text)
                   && !String.IsNullOrWhiteSpace(PriceTB.Text) && !String.IsNullOrWhiteSpace(DateDP.Text) &&
                   NameTB.Text.Length >= 4 && CostTB.Text.Length >= 2 && PriceTB.Text.Length >= 2 && QuantityTB.Text.Length >= 1)
                {
                    new GoodsTableAdapter().UpdateQuery(Convert.ToString(NameTB.Text), Convert.ToDecimal(CostTB.Text), Convert.ToDecimal(PriceTB.Text),
                        Convert.ToInt32(QuantityTB.Text), Convert.ToDateTime(DateDP.Text), Convert.ToInt32(IdOn)); tb_error.Text = "";
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

