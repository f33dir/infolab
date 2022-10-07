using System;

namespace InfoLab1.Models;

public class UserM
{
    public String UserName;
    private String HashedPassword { get; set; }
    private String Password;
    public Boolean IsAdmin;
}