using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kasa_otomasyonu
{
    /// <summary>
    /// AdminMenu.xaml etkileşim mantığı
    /// </summary>
    /// 
    
    public partial class AdminMenu : Window
    {
        private List<Employee> userList;
        private List<Order> orderList;
        private DateTime selectedDate;
        public AdminMenu()
        {
            InitializeComponent();
            CompanyName.Text = Company.Name;
            selectedDate = DateTime.Now;
            RefreshGUI(null, null);
            RefreshDate();

        }

        private void LoadUserList(){
            userList = SQLManager.GetEmployees();
     
            UserList.ItemsSource = userList;
        }

        private void LoadOrderList(){
            orderList = SQLManager.GetOrdersByDay(selectedDate);
            foreach (var order in orderList)
            {
                Expander OrderExpander= new Expander();
                OrderExpander.IsExpanded=false;
                OrderExpander.Expanded += Expander_Expanded;
                OrderExpander.Collapsed += Expander_Collapsed;
                OrderExpander.BorderBrush= Brushes.LightGray;
                OrderExpander.Background= Brushes.White;
                OrderExpander.BorderThickness=new Thickness(0.5);
                OrderExpander.Margin=new Thickness(5);
                OrderExpander.Header = @$"#{order.Id}       User = {order.EmployeeName}           Total Price = {order.TotalPrice:C}           Order Date = {order.OrderDate.ToShortDateString() +"  " +order.OrderDate.ToShortTimeString()}";
                OrderExpander.Tag=order;
                OrderList.Children.Add(OrderExpander);
            }
        }

        private void LoadOrderItems(Expander OrderExpander){
            DataGrid orderItemsGrid = new DataGrid
            {
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(10),
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star),
                CanUserDeleteRows = false,
                CanUserReorderColumns = false,
                CanUserResizeColumns = false,
                CanUserResizeRows = false,
                IsReadOnly = true,
                HeadersVisibility = DataGridHeadersVisibility.Column,
                GridLinesVisibility = DataGridGridLinesVisibility.None,
                SelectionMode = DataGridSelectionMode.Single,
                SelectionUnit = DataGridSelectionUnit.FullRow,
            };
            orderItemsGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Item",
                Binding = new Binding("ItemName")
            });

            orderItemsGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Quantity",
                Binding = new Binding("Quantity")
            });

            orderItemsGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Price",
                Binding = new Binding("Price")
            });

            orderItemsGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Total",
                Binding = new Binding("TotalPrice")
            });

            Order order = (Order)OrderExpander.Tag;
            orderItemsGrid.ItemsSource = order.Items;
            OrderExpander.Content = orderItemsGrid;
        }
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expandedExpander = (Expander)sender;
            if (expandedExpander.Content != null)
                return;
            LoadOrderItems(expandedExpander);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander collapsedExpander = (Expander)sender;

        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void RollBackToYesterday(object sender, RoutedEventArgs e)
        {
            this.selectedDate = this.selectedDate.AddDays(-1);
            RefreshDate();
            RefreshGUI(null, null);
        }

        private void RefreshGUI(object sender, RoutedEventArgs e)
        {
            OrderList.Children.Clear();
            UserList.ItemsSource = null;
            LoadOrderList();
            LoadUserList();
        }
        
        private void RefreshDate()
        {
            selectedDatetime.Text = selectedDate.ToShortDateString();
            if (selectedDate.Date >= DateTime.Now.Date)
            {
                RollForwardTomorrow.IsEnabled = false;
            }
            else
            {
                RollForwardTomorrow.IsEnabled = true;
            }
        }

        private void RollForwardToTomorrow(object sender, RoutedEventArgs e)
        {
            this.selectedDate = this.selectedDate.AddDays(1);
            RefreshDate();
            RefreshGUI(null, null);
        }

        private void BackToToday(object sender, RoutedEventArgs e)
        {
            this.selectedDate = DateTime.Now;
            RefreshDate();
            RefreshGUI(null, null);
        }
    }
}
