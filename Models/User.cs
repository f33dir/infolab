using System;
using System.Security.Cryptography;
using System.Text;
using Chilkat;

namespace InfoLab1.Models;

public class User
{
    private bool _isBanned;
    private bool _validatePassword;

    public bool IsBanned
    {
        get => _isBanned;
        set => _isBanned = value;
    }

    public bool ValidatePassword
    {
        get => _validatePassword;
        set => _validatePassword = value;
    }

    public string Username
    {
        get => _username;
        set => _username = value;
    }

    public string Password
    {
        get => _password;
        set
        {
            var crypt = new Chilkat.Crypt2();
            crypt.HashAlgorithm = "md2";
            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;
            _password = crypt.HashStringENC(value);

        }
    }

    public bool IsAdmin
    {
        get => _isAdmin;
        set => _isAdmin = value;
    }

    private String _username;
    private String _password;
    private Boolean _isAdmin;
    
    
    
    public User(string username, string password, bool isAdmin,bool isNew = false)
    {
        ValidatePassword = true;
        _isBanned = false;
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
        if (isNew)
        {
            _password = null;
        }
    }
}