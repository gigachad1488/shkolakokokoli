using System;

namespace shkolakokokoli.Models;

public class Client
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string surName { get; set; }
    public int phone { get; set; }
    public DateTime birthday;

    public string Birthday
    {
        get
        {
            return birthday.ToString("dd/MM/yyyy");
        }
    }
    public string lastLanguage { get; set; }
    public string languageLevel { get; set; }
    public string languageNeeds { get; set; }

    public Client()
    { }

    public Client(int id, string firstname, string surName, int phone, DateTime birthday, string lastLanguage, string languageLevel, string languageNeeds)
    {
        this.id = id;
        this.firstName = firstname;
        this.surName = surName;
        this.phone = phone;
        this.birthday = birthday;
        this.lastLanguage = lastLanguage;
        this.languageLevel = languageLevel;
        this.languageNeeds = languageNeeds;
    }
    
    public override string ToString()
    {
        return $"{firstName} {surName}";
    }

}