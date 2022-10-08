using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using InfoLab1.Models;
using Avalonia.Diagnostics;
namespace InfoLab1.Services;
public class LoginService
{
    private List<User> Credentials = new List<User>(); 
    public LoginService()
    {
        Credentials = new List<User>();
        String file;
        try {
            file = File.ReadAllText("data.json");
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    
        if (file != null)
            Credentials = JsonSerializer.Deserialize<List<User>>(file);
    }
}