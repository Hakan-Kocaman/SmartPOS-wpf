using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Kasa_otomasyonu
{
    /// <summary>
    /// Window1.xaml etkileşim mantığı
    /// </summary>
    public partial class UserMenu : Window
    {
        Employee User;
        Order order;
        List<OrderItem> orderItems = new List<OrderItem>();
        List<Category> categoryList;
        List<Item> itemList;
        CustomerMenu customerMenu;
        public bool isInternalClosing = false;

        public UserMenu(Employee User)
        {
            InitializeComponent();
            CompanyName.Text=Company.Name;
            this.User=User;
            this.order = new Order(0,User.Id, DateTime.Now);
            categoryList = SQLManager.GetCategory();
            itemList = SQLManager.GetItem();
            LoadCategoriesWithItems();
            customerMenu = null;
        }
    


        public void LoadCategoriesWithItems()
        {
            CategoryPanel.Children.Clear();
            foreach (Category category in categoryList)
            {
                TextBlock category_txt = new TextBlock();
                category_txt.Text = category.Title.ToUpper();
                category_txt.FontSize = 30;
                category_txt.Margin = new Thickness(10, 10, 10, 10);
                
                CategoryPanel.Children.Add(category_txt);

                WrapPanel itemPanel = new WrapPanel();
                itemPanel.Margin = new Thickness(10, 0, 10, 10);


                foreach (Item item in itemList)
                {
                    if (item.Category_Id == category.Id)
                    {
                        Button item_btn = new Button();
                        item_btn.Content = item.Name.ToUpper();
                        item_btn.FontSize = 25;
                        item_btn.Background = Brushes.White;
                        item_btn.Margin = new Thickness(20, 20, 20, 20);
                        item_btn.Padding = new Thickness(20, 15, 20, 15);
                        item_btn.Tag = item;
                        item_btn.Click += ItemButton_Click;

                        itemPanel.Children.Add(item_btn);
                    }
                }
                CategoryPanel.Children.Add(itemPanel);

            }
        }
        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Item clickedItem = (Item)btn.Tag;

            AddToCart(clickedItem);

        }

        public void AddToCart(Item item) {

         foreach (OrderItem orderItem in orderItems){
           if (item.Id.Equals(orderItem.ItemId)) {
                orderItem.Quantity++;
                this.order.TotalPrice += item.Price;
                    RefreshCartGUI();
                    return;    
               }
            }    
            this.orderItems.Add(new OrderItem(item.Id, item.Name, 1, item.Price));
            this.order.TotalPrice += item.Price;

            RefreshCartGUI();
            CartList.ScrollIntoView(CartList.Items[CartList.Items.Count - 1]);


        }

        public void CartItem_Click(object sender, RoutedEventArgs e){
           
            TextBlock CartItem_txt = (TextBlock)sender;

            OrderItem clickedOrderItem = (OrderItem)CartItem_txt.Tag;

            DecreaseItemQuantity(clickedOrderItem);
        }

        public void DecreaseItemQuantity(OrderItem orderItem){
            foreach (OrderItem oi in orderItems){
                if (oi.ItemId.Equals(orderItem.ItemId)){
                    oi.Quantity--;
                    this.order.TotalPrice -= oi.Price;
                    if (oi.Quantity == 0){
                        orderItems.Remove(oi);
                    }
                    RefreshCartGUI();
                    return;
                }
            }
        }

        public void RefreshCartGUI(){
            CartList.Items.Clear(); 

            foreach (OrderItem cartItem in orderItems){
                TextBlock itemtxt = new TextBlock();
                itemtxt.Text = cartItem.Quantity + "   " + cartItem.ItemName + "-" + (cartItem.Price * cartItem.Quantity).ToString("C");
                itemtxt.FontSize = 20;
                itemtxt.Margin = new Thickness(5);
                itemtxt.Tag = cartItem;
                itemtxt.MouseLeftButtonDown += CartItem_Click;
                CartList.Items.Add(itemtxt);

            }

            TotalPriceText.Text = "Total: " + order.TotalPrice.ToString("C");

            if (customerMenu != null){
                customerMenu.RefreshCartGUI(orderItems, order.TotalPrice);
            }
        }

        private void EmptyCart(object sender, RoutedEventArgs e)
        {
            order.TotalPrice = 0;
            orderItems.Clear();
            RefreshCartGUI();
        }

        private void ApplyCart(object sender, RoutedEventArgs e)
        {
            if (orderItems.Count == 0)
            {
                MessageBox.Show("Cart is empty!");
                return;
            }
            MessageBox.Show("Order applied successfully!");
            order.EmployeeId = User.Id;
            order.OrderDate = DateTime.Now;
            SQLManager.InsertOrderWithItems(order, orderItems);
            order = new Order(0, User.Id, DateTime.Now);
            orderItems.Clear();
            RefreshCartGUI();

        }

        private void CustomerMenuAvailable_Checked(object sender, RoutedEventArgs e)
        {
            if (customerMenu == null)
            {
                customerMenu = new CustomerMenu(this);
            }

            customerMenu.RefreshCartGUI(orderItems, order.TotalPrice);

            if (!customerMenu.IsVisible)
                customerMenu.Show();
        }

        private void CustomerMenuAvailable_Unchecked(object sender, RoutedEventArgs e)
        {
            if (customerMenu != null)
                customerMenu.Hide();
        }



        private void LogOut(object sender, RoutedEventArgs e)
        {
            if (customerMenu != null)
            {
                customerMenu.isInternalClosing = true;
                customerMenu.Close();
            }

            isInternalClosing = true;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }
        public void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!isInternalClosing && customerMenu != null)
            {
                customerMenu.isInternalClosing = true;
                customerMenu.Close();
            }

        }
    }
}
