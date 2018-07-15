


namespace Lands.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Lands.Views;
    using Xamarin.Forms;

    public class LoginViewModel: BaseViewModel
    {
        
        //En los atributos pongo las propiedades privadas para los
        //campos que se tengan que refrescar en los controles
        //las pongo con el mismo nombre que las publicas pero en minuscula
        #region Atributes
            private string email;
            private string password;
            private bool isRunning;
            private bool isEnabled;
            private bool isRemembered;
        #endregion


        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set { SetValue(ref isRunning, value); }
        }

        public bool IsRemembered
        {
            get { return isRemembered; }
            set { SetValue(ref isRemembered, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetValue(ref isEnabled, value); }
        }
        #endregion

        //Los comandos son propiedades especiales y les creamos una region para ellos
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
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            };

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");
                return;
            };

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "jisv@gmail.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Invalid email or password.",
                    "Accept");
                //limpio el campo
                this.Password = string.Empty;
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

            //Antes de instaciar la pagina, uso el singleton para
            //instanciar la nueva ViewModel que voy a vincular a la page, en este caso LandsViewModel
            MainViewModel.GetInstance().Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
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
            //por defecto los bool son false por lo que para habilitar
            //los botones tengo que poner IsEnabled true
            this.isEnabled = true;

            //http://restcountries.eu/rest/v2/all
        }
        #endregion
    }
}
