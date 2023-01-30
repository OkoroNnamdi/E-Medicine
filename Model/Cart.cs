namespace EMedicine.Model
{
    public class Cart
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public decimal unitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int MedicineID { get; set; }
    }
}
