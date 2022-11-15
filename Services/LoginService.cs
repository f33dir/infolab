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
    private byte[] key = Encoding.UTF8.GetBytes("1234567890qwerty");
    private byte[] IV = Encoding.UTF8.GetBytes("1234567890qwertg");
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
        _aes = new AesManaged();
        _aes.Mode = CipherMode.CBC;
        _aes.Padding = PaddingMode.Zeros;
        _aes.Key = key;
        _aes.IV = IV;
        String file;
        byte[] source;
        try {
            source = File.ReadAllBytes("data.json");
            var input = DecryptStringFromBytes(source,key,IV);
            try
            {
                Credentials = new AvaloniaList<User>(JsonSerializer.Deserialize<List<User>>(input) ?? throw new InvalidOperationException());
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
            SaveCredentials();
            DecryptStringFromBytes(File.ReadAllBytes("data.json"),key,IV);
        }
    
    }

   static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
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
        File.WriteAllBytes("data.json",EncryptStringToBytes(Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(Credentials)),key,IV));
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