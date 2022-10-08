using System;
using Microsoft.EntityFrameworkCore;

namespace InfoLab1.Models;

public class User 
{
    public String Username;
    public String PasswordHash;
    public Boolean IsAdmin;
}