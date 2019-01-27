using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreEF
{
    class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public decimal Summa { get; set; }
        public DateTime Date { get; set; }
        
        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}
