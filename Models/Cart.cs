namespace DashBord.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string IdCustomer { get; set; }
        public int IdProducts { get; set; }
        public string ProductsName { get; set; }
        public double price { get; set; }
        public string Image { get; set; }
        public double Total { get; set; }
        public int Qty { get; set; }
        public string Color { get; set; }
        public double Tax { get; set; }
    //    public bool IsPaid { get; set; }

    }
}
