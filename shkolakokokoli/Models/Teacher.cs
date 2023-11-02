namespace shkolakokokoli.Models;

public class Teacher
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string surName { get; set; }

    public Teacher()
    { }
    
    public Teacher(int id, string firstName, string surName)
    {
        this.id = id;
        this.firstName = firstName;
        this.surName = surName;
    }

    public override string ToString()
    {
        return $"{firstName} {surName}";
    }
}