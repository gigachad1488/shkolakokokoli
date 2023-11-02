namespace shkolakokokoli.Models;

public class Class
{
    public int id { get; set; }
    public string name { get; set; }
    
    public Class()
    { }

    public Class(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
    
    public override string ToString()
    {
        return name;
    }
}