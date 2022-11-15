using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Avalonia.Collections;
using InfoLab1.Models;
using Avalonia.Diagnostics;
namespace InfoLab1.Services;

public enum LoginResult
{
    Success,NoUser,WrongPassword,NoPassword,DifferentPasswords,AlreadyRegistered,BadPassword
}
public class LoginService
{
    private byte[] key = Encoding.ASCII.GetBytes("1234567890qwerty");
    private static LoginService _loginService;
    public AvaloniaList<User> Credentials;
    public User CurrentUser;
    private Aes _aes;
    public bool IsBroken = false;
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

    public User Find(String Username)
    {
        int index = -1;
        var i = 0;
        while ( i< Credentials.Count && Credentials[i].Username  != Username )
            i++;
        if (i < Credentials.Count)
        {
            return Credentials[i];
        }
        return null;
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
        var u = Find(user.Username);
        if (u == null)
        {
            return 1;
        }

        u.IsAdmin = user.IsAdmin;
        return 0;
    }

    private void Init()
    {
        Credentials = new AvaloniaList<User>();
        Credentials.Add(new User("ADMIN","",true,true));
        Aes
        String file;
        byte[] source;
        try {
            source = File.ReadAllBytes("data.json");
            this.DecryptStringFromBytes_Aes(source,key);
            try
            {
                Credentials = new AvaloniaList<User>(JsonSerializer.Deserialize<List<User>>(source) ?? throw new InvalidOperationException());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                IsBroken = true;
                throw;
            }
        }
        catch (Exception e)
        {
            var s= Encoding.ASCII.GetBytes(JsonSerializer.Serialize(Credentials));
            File.WriteAllText("data.json",DecryptStringFromBytes_Aes(s,key));
            Console.WriteLine(e);
        }
    
    }

    private string? DecryptStringFromBytes_Aes(byte[] source, byte[] bytes)
    {
        Aes
    }

    public LoginResult RegisterUser(String username,String password,String password2)
    {

        var u = Find(username);
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
            SaveCredentials();
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
        file.Close();
    }

    ~LoginService()
    {
        SaveCredentials();
    }

    public void UpdateUsers(AvaloniaList<User> list)
    {
        this.Credentials = list;
    }
    
}