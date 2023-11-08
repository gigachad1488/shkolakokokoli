using Avalonia.Controls;
using Avalonia.Interactivity;
using shkolakokokoli.Models;

namespace shkolakokokoli.Views
{
    public partial class AddCPaymentWindow : Window
    {
        public AddCPaymentWindow()
        {
            InitializeComponent();

            addButton.Click += delegate { AddCPayment(); };
            cancelButton.Click += delegate { Close(null); };
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            rootGrid.Opacity = 1;
        }

        public AddCPaymentWindow(ClientPayment cpayment)
        {
            InitializeComponent();

            windowText.Text = "????????? ??????";
            addButtonText.Text = "????????";
            addButton.Click += delegate { ChangeCPayment(cpayment.id); };
            cancelButton.Click += delegate { Close(null); };


            clientsBox.Items.CollectionChanged += delegate
            {
                for (int i = 0; i < clientsBox.Items.Count; i++)
                {
                    Client c = clientsBox.Items[i] as Client;
                    if (c.id == cpayment.client.id)
                    {
                        clientsBox.SelectedIndex = i;
                        return;
                    }
                }
            };
            //Console.Write("TITER = " + MainWindowViewModel.Teachers.Select(x => x.id).First(x => x == course.teacher.id));
            paymentsBox.Items.CollectionChanged += delegate
            {
                for (int i = 0; i < paymentsBox.Items.Count; i++)
                {
                    Payment c = paymentsBox.Items[i] as Payment;
                    if (c.id == cpayment.payment.id)
                    {
                        paymentsBox.SelectedIndex = i;
                        return;
                    }
                }
            };

            paidcheck.IsCancel = cpayment.isPaid;
        }

        private void ChangeCPayment(int id)
        {
            ClientPayment cpayment = GetData();
            cpayment.id = id;
            if (cpayment == null)
            {
                return;
            }

            Db.ChangeClientPayment(cpayment);
            Close(null);
        }

        private void AddCPayment()
        {
            ClientPayment cpayment  = GetData();

            if (cpayment == null)
            {
                return;
            }

            Db.AddClientPayment(cpayment);
            Close(null);
        }

        private ClientPayment GetData()
        {
            ClientPayment cpay = new ClientPayment();

            if (clientsBox.SelectedIndex == -1 || paymentsBox.SelectedIndex == -1)
            {
                return null;
            }

            cpay.isPaid = (bool)paidcheck.IsChecked;
            cpay.client = (Client)clientsBox.SelectedItem;
            cpay.payment = (Payment)paymentsBox.SelectedItem;

            return cpay;
        }
    }
}
