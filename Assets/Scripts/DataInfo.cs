using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInfo
{
   public string name { get; set; }
   public string email { get; set; }

   public DataInfo(string name, string email)
    {
        this.name = name;
        this.email = email;
    }

}
