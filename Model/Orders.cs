namespace EMedicine.Model
{
    public class Orders
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string OrderNo { get; set; }
        public int OrderTotal { get; set; }
        public string orderStatus { get; set; }

    }
}
