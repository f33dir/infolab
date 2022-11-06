using System;
using System.Collections;
using System.Collections.Generic;
using InfoLab1.Models;
using InfoLab1.Services;
using ReactiveUI;

namespace InfoLab1.ViewModels;

public class MainViewM : ReactiveObject
{
    public void OnSelection(User u)
    {
        SelectedUser = u;
    }

    private User _selectedUser ;
    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = this.RaiseAndSetIfChanged(ref _selectedUser, value);
            if (_selectedUser != null)
            {
                ShowEditPanel = true;
            }
            else
            {
                ShowEditPanel = false;
            }
        }
    }

    private LoginService _loginService;
    private List<User> _users;
    public List<User> Users
    {
        get => _users;
        set => _users = this.RaiseAndSetIfChanged(ref _users, value);
    }
    public LoginService LoginService
    {
        get => _loginService;
        set => this.RaiseAndSetIfChanged(ref _loginService, value);
    }
    public User CurrentUser
    {
        get => _currentUser;
        set => this.RaiseAndSetIfChanged(ref _currentUser, value);
    }
    private User _currentUser;
    public MainViewM()
    {
        _loginService = LoginService.get();
        _users = _loginService.Credentials;
        CurrentUser = _loginService.CurrentUser;
    }

    private Boolean _showEditPanel = false;

    public bool ShowEditPanel
    {
        get => _showEditPanel;
        set => this.RaiseAndSetIfChanged(ref _showEditPanel, value);
    }
}