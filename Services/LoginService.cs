using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using InfoLab1.Models;
using Avalonia.Diagnostics;
namespace InfoLab1.Services;

public enum LoginResult
{
    Success,NoUser,WrongPassword,NoPassword,DifferentPasswords,AlreadyRegistered,BadPassword
}
public class LoginService
{
    private static LoginService _loginService;
    public List<User> Credentials = new List<User>();
    public User CurrentUser;
             
    public static LoginService get()
    {
        if (_loginService == null)
        {
            _loginService = new LoginService();
            _loginService.Init(); 


            return _loginService;
        }
        else
        {
            return _loginService;
        }
    }

    private LoginService()
    {
        
    }

    public LoginResult Auth(User user)
    {
        var i = 0;
        while ( i< Credentials.Count && Credentials[i].Username  != user.Username )
            i++;
        if (i == Credentials.Count)
        {
            return LoginResult.NoUser;
        }

        if (Credentials[i].Password != null)
        {
            if (user.Password == Credentials[i].Password)
            {
                CurrentUser = Credentials[i];
                return LoginResult.Success;
            }
            else
            {
                return LoginResult.WrongPassword;
            }
        }
        else
        {
            return LoginResult.NoPassword;
        }
    }

    public int AddUser(User user)
    {
        Credentials.Add(user);
        return 0;
    }

    public int ChangePassword(String username, String password)
    {
        var i = 0;
        while ( i< Credentials.Count && Credentials[i].Username  != username )
            i++;
        if (i > Credentials.Count)
        {
            return 1;
        }
        Credentials[i].Password = password;
        return 0;
    }

    public int SetAdmin(User user)
    {
        var u = Credentials.Find(a => a.Username == user.Username);
        if (u == null)
        {
            return 1;
        }

        u.IsAdmin = user.IsAdmin;
        return 0;
    }

    private void Init()
    {
        Credentials = new List<User>();
        Credentials.Add(new User("ADMIN","",true,true));
        String file;
        try {
            file = File.ReadAllText("data.json");
            Credentials = JsonSerializer.Deserialize<List<User>>(file) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            var s= JsonSerializer.Serialize<List<User>>(Credentials);
            File.WriteAllText("data.json",s);
            Console.WriteLine(e);
        }
    
    }

    public LoginResult RegisterUser(String username,String password,String password2)
    {
        
        var u = Credentials.Find(a => a.Username == username);
        if (u == null)
        {
            return LoginResult.NoUser;
        }

        if (!String.IsNullOrEmpty(u.Password))
        {
            return LoginResult.AlreadyRegistered;
        }

        if (password == password2)
        {
            if (password.Length == 0)
                return LoginResult.BadPassword;
            bool valid= true;
            bool current = false;
            char[] arr = password.ToCharArray();
            if (Char.IsNumber(arr[0]))
                current = true;
            for (int i = 1; i < password.Length; i++)
            {
                if (current && Char.IsNumber(arr[i]))
                {
                    valid = false;
                    continue;
                }
                 if (!current && Char.IsLetter(arr[i]))
                {
                    valid = false;
                    continue;                   
                }

                current = !current;
            }

            if (!valid) return LoginResult.BadPassword;
            this.ChangePassword(username,password);
            return LoginResult.Success;
        }
        else
        {
            return LoginResult.DifferentPasswords;
        }
    }

    public void SaveCredentials()
    {
        var file = File.Create("data.json");
        file.Write(JsonSerializer.SerializeToUtf8Bytes(Credentials));
    }
}