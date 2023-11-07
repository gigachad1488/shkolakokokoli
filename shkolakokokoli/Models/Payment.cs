using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shkolakokokoli.Models
{
    public class Payment
    {
        public int id;
        public string name { get; set; }
        public int price { get; set; }

        public Payment(int id, string name, int price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public Payment() { }

        public override string ToString()
        {
            return name;
        }
    }
}
