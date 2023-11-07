using Avalonia.Controls;
using Avalonia.Interactivity;
using HarfBuzzSharp;
using shkolakokokoli.Models;
using System;

namespace shkolakokokoli.Views
{
    public partial class AddPaymentWindow : Window
    {
        public AddPaymentWindow()
        {
            InitializeComponent();
            addButton.Click += delegate { AddPayment(); };
            cancelButton.Click += delegate { Close(null); };
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            rootGrid.Opacity = 1;
        }

        public AddPaymentWindow(Payment payment) 
        {
            InitializeComponent();

            windowText.Text = "Изменение услуги";
            addButtonText.Text = "Изменить";
            addButton.Click += delegate { ChangePayment(payment.id); };
            cancelButton.Click += delegate { Close(null); };

            nameText.Text = payment.name;
            priceText.Text = payment.price.ToString();
        }

        private void ChangePayment(int id)
        {
            Payment payment = GetData();
            payment.id = id;
            if (payment == null)
            {
                return;
            }

            Db.ChangePayment(payment);
            Close(null);
        }

        private void AddPayment()
        {
            Payment payment = GetData();

            if (payment == null)
            {
                return;
            }

            Db.AddPayment(payment);
            Close(null);
        }

        private Payment GetData()
        {
            Payment payment = new Payment();
            int price = 0;
            if (nameText.Text == string.Empty && Int32.TryParse(priceText.Text, out price))
            {
                return null;
            }

            payment.name = nameText.Text;
            payment.price = Convert.ToInt32(priceText.Text);
            return payment;
        }
    }
}
