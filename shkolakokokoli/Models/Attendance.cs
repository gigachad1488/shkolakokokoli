using System.Collections.Generic;

namespace shkolakokokoli.Models;

public class Attendance
{
    public int id;
    
    public int client;
    
    public int lesson;

    public bool attendance;
    
    public Attendance()
    {}

    public Attendance(int id, int client, int lesson, bool attendance)
    {
        this.id = id;
        this.client = client;
        this.lesson = lesson;
        this.attendance = attendance;
    }
}