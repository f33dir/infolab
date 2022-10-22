using System;
using InfoLab1.Services;
using ReactiveUI;

namespace InfoLab1.ViewModels;

public class RegisterViewM : ReactiveObject
{
    public string Password
    {
        get => _password;
        set => _password = this.RaiseAndSetIfChanged(ref _password, value);
    }

    public string Username
    {
        get => _username;
        set => _username = this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string PasswordRepeat
    {
        get => _passwordRepeat;
        set => _passwordRepeat = this.RaiseAndSetIfChanged(ref _passwordRepeat, value);
    }

    private String _password;
    private String _username;
    private String _passwordRepeat;
    private LoginService _loginService;
    public RegisterViewM()
    {
        _loginService = LoginService.get();
    }

    public LoginResult Register()
    {
        var result = _loginService.RegisterUser(_username, _password, _passwordRepeat);
    }
}