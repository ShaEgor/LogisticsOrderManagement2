using System;
using System.Windows;
using LogisticsOrderManagement2;
using System.Windows.Controls;

namespace LogisticsOrderManagement2
{
    public partial class OrderWindow : Window
    {
        public Order OrderData { get; private set; }

        public OrderWindow(Order order = null)
        {
            InitializeComponent();
            if (order != null)
            {
                OrderData = order;
                txtOrderNumber.Text = order.OrderNumber;
                txtCustomerName.Text = order.CustomerName;
                txtPhoneNumber.Text = order.PhoneNumber;
                dpDateAdded.SelectedDate = order.DateAdded;
                txtProductType.Text = order.ProductType;
                txtUnitWeight.Text = order.UnitWeight.ToString();
                txtUnitVolume.Text = order.UnitVolume.ToString();
                txtDistance.Text = order.Distance.ToString();
                txtQuantity.Text = order.Quantity.ToString();
                cmbStatus.SelectedItem = cmbStatus.Items[GetStatusIndex(order.Status)];

                UpdateDeliveryCost();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                if (OrderData == null)
                    OrderData = new Order();

                OrderData.OrderNumber = txtOrderNumber.Text;
                OrderData.CustomerName = txtCustomerName.Text;
                OrderData.PhoneNumber = txtPhoneNumber.Text;
                OrderData.DateAdded = dpDateAdded.SelectedDate ?? DateTime.Now;
                OrderData.ProductType = txtProductType.Text;
                OrderData.UnitWeight = double.Parse(txtUnitWeight.Text);
                OrderData.UnitVolume = double.Parse(txtUnitVolume.Text);
                OrderData.Distance = double.Parse(txtDistance.Text);
                OrderData.Quantity = int.Parse(txtQuantity.Text);
                OrderData.Status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();

                DialogResult = true;
                Close();
            }
        }

        private void UpdateDeliveryCost()
        {
            if (double.TryParse(txtUnitWeight.Text, out double unitWeight) &&
                double.TryParse(txtUnitVolume.Text, out double unitVolume) &&
                double.TryParse(txtDistance.Text, out double distance) &&
                int.TryParse(txtQuantity.Text, out int quantity))
            {
                OrderData.UnitWeight = unitWeight;
                OrderData.UnitVolume = unitVolume;
                OrderData.Distance = distance;
                OrderData.Quantity = quantity;

                double cost = OrderData.CalculateDeliveryCost();
                txtDeliveryCost.Text = $"{cost:F2} руб.";
            }
            else
            {
                txtDeliveryCost.Text = "Ошибка ввода данных!";
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtOrderNumber.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                dpDateAdded.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtProductType.Text) ||
                string.IsNullOrWhiteSpace(txtUnitWeight.Text) ||
                string.IsNullOrWhiteSpace(txtUnitVolume.Text) ||
                string.IsNullOrWhiteSpace(txtDistance.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private int GetStatusIndex(string status)
        {
            switch (status)
            {
                case "В пути": return 0;
                case "На складе": return 1;
                case "Доставлен": return 2;
                default: return 0;
            }
        }

        private void TxtInputChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateDeliveryCost();
        }
    }
}