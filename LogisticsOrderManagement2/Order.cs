using System;

namespace LogisticsOrderManagement2
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateAdded { get; set; }
        public string ProductType { get; set; }
        public double UnitWeight { get; set; }
        public double UnitVolume { get; set; }
        public double Distance { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        public double CalculateDeliveryCost()
        {
            const double weightCoefficient = 3.0;
            const double volumeCoefficient = 1.5;

            double costByWeight = UnitWeight * Distance * weightCoefficient;
            double costByVolume = UnitVolume * Distance * volumeCoefficient;

            return (costByWeight + costByVolume) * Quantity;
        }

        public override string ToString()
        {
            return $"Заказ {OrderNumber}: {CustomerName} - {Status}";
        }
    }
}