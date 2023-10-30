using Avalonia.Controls;
using shkolakokokoli.Models;

namespace shkolakokokoli.Views
{
    public partial class ClientUserControl : UserControl
    {
        public ClientUserControl(Client client)
        {
            InitializeComponent();
            
            firstNameText.Text = client.firstname;
            surNameText.Text = client.surName;
            phoneText.Text = client.phone.ToString();
            dateText.Text = client.birthday.ToString();
            langLevelText.Text = client.languageLevel;
            lastLangText.Text = client.lastLanguage;
            needLevelText.Text = client.languageNeeds;
            
        }
    }
}
