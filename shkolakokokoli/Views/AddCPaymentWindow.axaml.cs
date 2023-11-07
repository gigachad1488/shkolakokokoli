using Avalonia.Controls;
using shkolakokokoli.Models;

namespace shkolakokokoli.Views
{
    public partial class AddCPaymentWindow : Window
    {
        public AddCPaymentWindow()
        {
            InitializeComponent();
        }

        public AddCPaymentWindow(ClientPayment clientPayment) 
        {
            InitializeComponent();
        }
    }
}
