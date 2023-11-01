namespace shkolakokokoli.Models;

public class Language
{
    public int id { get; set; }
    public string name { get; set; }

    public Language()
    { }
    
    public Language(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}