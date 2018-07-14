


namespace Lands.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class LoginViewModel
    {
        #region Properties
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool IsRunning
        {
            get;
            set;
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        #endregion

        //Los ciomandos son propiedades especiales y les creamos una region para ellos
        //y son del tipo interface
        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);            
            }


        }
        //Aqui validamos lo que el usuario escribe para el login
        private void Login()
        {
            
        }

        public ICommand RegisterCommand
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
        }
        #endregion
    }
}
