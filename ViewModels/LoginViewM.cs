using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Interactivity;
using InfoLab1.Models;
using InfoLab1.Services;
using ReactiveUI;

namespace InfoLab1.ViewModels;

public class LoginViewM : ReactiveObject
{
    
    private LoginService _loginService;
    private String _password;
    private String _username;
    private bool _islogged;
    
    public LoginViewM()
    {
        IsLogged = false;
        _loginService = LoginService.get();
    }
    
    public String Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    public String Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public bool IsLogged
    {
        get => _islogged;
        set => this.RaiseAndSetIfChanged(ref _islogged, value);
    }

    public LoginResult DoLogin()
    {
        var u = new User(_username, _password, false);
        var result =_loginService.Auth(u);
        if (result == LoginResult.Success)
        {
            
        }

        return result;
    }

    public ReactiveCommand<Unit, Unit> Login { get; }
}
