﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using BounceBall.Command;
using BounceBall.Manager;
using BounceBall.Models;

namespace BounceBall.ViewModels
{
    public class LoginAndSignUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private UserManager _userManager;
        public Action<User> _onLoginSuccess;

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        public LoginAndSignUpViewModel(UserManager userManager, Action<User> onLoginSuccess)
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
            SignUpCommand = new RelayCommand(Signup, CanSignUp);
            _userManager = userManager;
            _onLoginSuccess = onLoginSuccess;
        }

        private bool CanSignUp(object obj)
        {
            return true;
        }

        private bool CanLogin(object obj)
        {
            return true;
        }


        private void Signup(object obj)
        {
            try
            {
                if (Password == ConfirmPassword)
                {
                    _userManager.AddUser(new User(UserName, Password));
                    MessageBox.Show($"Account for {UserName} created !!");
                }
                else
                {
                    MessageBox.Show("Error : Cannot create account");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Login(object obj)
        {
            if (_userManager.ValidateUser(UserName, Password))
            {
                User currentUser = _userManager.GetUser(UserName, Password);
                _onLoginSuccess?.Invoke(currentUser);

            }
            else
            {
                MessageBox.Show("Log In Failed !!");

            }
        }

        public string UserName
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
