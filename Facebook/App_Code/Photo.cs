using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class Photo
{
    public string Title { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public string Path { get; set; }

    public Photo() { }

    public Photo(string Title, string Location, DateTime Date, string Path)
    {
        this.Title = Title;
        this.Location = Location;
        this.Date = Date;
        this.Path = Path;
    }
}