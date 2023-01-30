namespace EMedicine.Model
{
    public class OrderItems
    {
        public int ID { get; set; }
        public string OrderID { get; set; }
        public int MedicineID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
