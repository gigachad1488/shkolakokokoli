using System;

namespace shkolakokokoli.Models;

public class Client
{
    public int id { get; set; }
    public string firstname { get; set; }
    public string surName { get; set; }
    public int phone { get; set; }
    public DateTime birthday { get; set; }
    public string lastLanguage { get; set; }
    public string languageLevel { get; set; }
    public string languageNeeds { get; set; }

    public Client()
    { }

    public Client(int id, string firstname, string surName, int phone, DateTime birthday, string lastLanguage, string languageLevel, string languageNeeds)
    {
        this.id = id;
        this.firstname = firstname;
        this.surName = surName;
        this.phone = phone;
        this.birthday = birthday;
        this.lastLanguage = lastLanguage;
        this.languageLevel = languageLevel;
        this.languageNeeds = languageNeeds;
    }
}