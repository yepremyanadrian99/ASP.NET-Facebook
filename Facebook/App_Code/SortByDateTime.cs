using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SortByDateTime
{
    public static List<Photo> SortDescending(List<Photo> arr)
    {
        arr.Sort((a, b) => b.Date.CompareTo(a.Date));
        return arr;
    }
}