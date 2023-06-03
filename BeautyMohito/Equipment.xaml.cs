using BeautyMohito.DbMohitoTableAdapters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BeautyMohito
{
    public partial class Equipment : Window
    {
        SqlConnection con = new SqlConnection();
        DbMohito dbMohito;
        EquipmentViewTableAdapter equipmentViewTableAdapter;

        string sizeBooking = Booking.sizeSave;

        public static string sizeSave;

        public static string IdOn;
        public static string NameOn;
        public static string QuantityOn;
        public static string CostOn;
        public static string IdEOn;
        public static string BuyOn;
        public static string LifeOn;
        public static string StatusOn;

        public Equipment()
        {
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Mohito.Properties.Settings.MohitoConnectionString"].ConnectionString.ToString();
            InitializeComponent(); RefreshData(); SaveSize();
        }

        private bool IsMaximize = false;

        public void RefreshData()
        {
            EquipmentViewTableAdapter adapter = new EquipmentViewTableAdapter();
            DbMohito.EquipmentViewDataTable table = new DbMohito.EquipmentViewDataTable();
            adapter.Fill(table);
            membersDataGrid.ItemsSource = table;
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

        private void textBoxFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            EquipmentViewTableAdapter adapter = new EquipmentViewTableAdapter();
            DbMohito.EquipmentViewDataTable table = new DbMohito.EquipmentViewDataTable();
            adapter.Fill(table);

            DataView dv = new DataView(table);

            if (textBoxFilter.Text != null)
            {
                dv.RowFilter = $@"[Name_of_equipment] LIKE '%{textBoxFilter.Text}%' ";
                membersDataGrid.ItemsSource = dv.ToTable().DefaultView;
            }
            else
            {
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

      
        private void Goods_Click(object sender, RoutedEventArgs e)
        {
            Goods goods = new Goods();
            goods.Show();
            this.Close();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (membersDataGrid.SelectedItem != null)
            {
                UpdateGoods update = new UpdateGoods();
                update.Show();
                this.Close();
            }
        }
        private void UpdateGoodsBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateGoods updateGoods = new UpdateGoods();
            updateGoods.Show();
            this.Close();
        }

        private void Equipment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)//
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить эти данные? Данные удалятся навсегда, " +
                    "без возможности восстановления", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                }
                else
                {
                    new EquipmentTableAdapter().DeleteQuery(Convert.ToInt32((membersDataGrid.SelectedItems[0] as DataRowView).Row.ItemArray[0]));
                    RefreshData();
                }
            }
            catch
            {
            }
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (membersDataGrid.SelectedItem != null)
            {
                if (membersDataGrid.SelectedItem != null)
                    IdOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[0].ToString(); if (membersDataGrid.SelectedItem != null)
                    NameOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[1].ToString(); if (membersDataGrid.SelectedItem != null)
                    QuantityOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[2].ToString(); if (membersDataGrid.SelectedItem != null)
                    CostOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[3].ToString(); if (membersDataGrid.SelectedItem != null)
                    IdEOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[4].ToString(); if (membersDataGrid.SelectedItem != null)
                    BuyOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[6].ToString(); if (membersDataGrid.SelectedItem != null)
                    LifeOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[7].ToString(); if (membersDataGrid.SelectedItem != null)
                    StatusOn = (membersDataGrid.SelectedItem as DataRowView).Row.ItemArray[8].ToString();
            }
            else
            {
                IdOn = null;
                NameOn = null;
                QuantityOn = null;
                CostOn = null;
                IdEOn = null;
                BuyOn = null;
                LifeOn = null;
                StatusOn = null;
            }
        }

        
    }
}