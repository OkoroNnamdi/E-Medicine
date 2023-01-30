using System.Collections.Generic;

namespace EMedicine.Model
{
    public class Response
    {
        public int status { get; set; }
        public string message { get; set; }

        public List<User> Listusers { get; set; }
        public User User { get; set; }

        public List<Medicines >ListMedicines { get; set; }
        public Medicines medicines { get; set; }    
        public List <Cart > ListCarts { get; set; }
        public Cart cart { get; set; }

        public List<Orders> Listorders { get; set; }
        public Orders orders { get; set; }

        public List<OrderItems> Listorderitems { get; set; }
        public OrderItems orderitems { get; set; }

    }
}
