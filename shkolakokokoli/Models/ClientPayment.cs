using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shkolakokokoli.Models
{
    public class ClientPayment
    {
        public int id;
        public Client client;
        public Payment payment;

        public string Client
        {
            get
            {
                return client.ToString();
            }
        }

        public string Payment
        {
            get
            {
                return payment.ToString();
            }
        }

        public bool isPaid { get; set; }
    }
}
