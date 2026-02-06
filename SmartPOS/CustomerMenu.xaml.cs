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

namespace Kasa_otomasyonu
{
    /// <summary>
    /// CustomerMenu.xaml etkileşim mantığı
    /// </summary>
    public partial class CustomerMenu : Window
    {

        UserMenu owner;
        public bool isInternalClosing = false;
        public CustomerMenu(UserMenu owner)
        {
            InitializeComponent();
            this.owner = (UserMenu)owner;
        }

        public void RefreshCartGUI(List<OrderItem> orderItems, Decimal totalPrice)
        {
            CartList.Items.Clear();


            foreach (OrderItem cartItem in orderItems)
            {
                Grid row = new Grid
                {
                    Margin = new Thickness(10, 5, 10, 5),
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) }); // isim
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // adet
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) }); // fiyat


                TextBlock name = new TextBlock
                {
                    Text = cartItem.ItemName,
                    FontSize = 30
                };
                Grid.SetColumn(name, 0);

                TextBlock qty = new TextBlock
                {
                    Text = cartItem.Quantity.ToString(),
                    FontSize = 30,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetColumn(qty, 1);

                TextBlock price = new TextBlock
                {
                    Text = (cartItem.Price * cartItem.Quantity).ToString("C"),
                    FontSize = 30,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    TextAlignment = TextAlignment.Right
                };
                Grid.SetColumn(price, 2);

                row.Children.Add(name);
                row.Children.Add(qty);
                row.Children.Add(price);
                row.Background = Brushes.Transparent;


                CartList.Items.Add(row);


            }
            TotalPrice.Text = "Total: " + totalPrice.ToString("C");




        }

        public void Window_Closing(object sender, CancelEventArgs e)
        {

            if (!isInternalClosing && owner != null)
            {
                owner.isInternalClosing = true;
                owner.Close();
            }
        }

}
}
