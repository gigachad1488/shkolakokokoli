namespace shkolakokokoli.Models;

public class Class
{
    public int id { get; set; }
    public int name { get; set; }
    
    public Class()
    { }

    public Class(int id, int name)
    {
        this.id = id;
        this.name = name;
    }
}