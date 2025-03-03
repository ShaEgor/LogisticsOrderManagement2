using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LogisticsOrderManagement2
{
    public partial class MainWindow : Window
    {
        private List<Order> orders = new List<Order>();

        public MainWindow()
        {
            InitializeComponent();
            RefreshOrderList();
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            if (orderWindow.ShowDialog() == true)
            {
                orders.Add(orderWindow.OrderData);
                RefreshOrderList();
            }
        }

        private void BtnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (lstOrders.SelectedItem is Order selectedOrder)
            {
                OrderWindow orderWindow = new OrderWindow(selectedOrder);
                if (orderWindow.ShowDialog() == true)
                {
                    int index = orders.IndexOf(selectedOrder);
                    if (index >= 0)
                    {
                        orders[index] = orderWindow.OrderData;
                    }
                    RefreshOrderList();
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshOrderList()
        {
            lstOrders.ItemsSource = null;
            lstOrders.ItemsSource = orders;
        }
    }
}