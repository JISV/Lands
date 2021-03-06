﻿


namespace Lands.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Lands.Views;
    using Xamarin.Forms;
    using Services;
    using Helpers;

    public class LoginViewModel: BaseViewModel
    {



        //Creo una region para los servicios por lo importantes que son
        //el apisService lo instanciamos en el constructor
        #region Services
        private ApiService apiService;
        #endregion

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

            

            //verificamos que el usuario puso un email
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, //Error
                    Languages.EmailValidation,//EmailVaildation
                    Languages.Accept); //Accept
                return;
            };

            //verificamos que puso un password
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,//Error
                    Languages.PasswordValidation, //PasswordValidation
                    Languages.Accept);//Accept
                return;
            };

            //activo el activity indicator y deshabilito el boton
            this.IsRunning = true;
            this.IsEnabled = false;

            //valido si hay conexion a internet
            var connection = await this.apiService.CheckConnection();
            //Si no hay le doy error
            if(!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }
            //Si hay conexion, debemos generar el token
            var token = await this.apiService.GetToken(
                "http://landsdbapi.azurewebsites.net", 
                this.Email, 
                this.Password);
            //valido como llego el obj token
            //primero verifico si es nulo
            if (token==null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.TokenNullValidation, //TokenNullValidation
                    Languages.Accept);
                return;
            }
            //si no es nulo, verfico si el token tiene algun problema (password o user invalido)
            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    token.ErrorDescription,
                    Languages.Accept);
                this.Password = string.Empty; //normalmente el password esta malo y limpio el entry
                return;
            }

            //Aqui ya tenemos el token
            this.IsRunning = false;
            this.IsEnabled = true;

            //pero debemos guardarlo por si el usuario
            //decide cambiar password y hay que hacerlo de
            //forma segura
            //como el token lo necesitamos en memoria
            //y no gaurdarlos en disco a menos que lo encriptemos
            //lo guardamos en la MainViewModel (ver propiedad Token en la MainViewModel)

            //creo un apuntador para el singleton de la mainviewmodel
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;

            this.Email = string.Empty;
            this.Password = string.Empty;

            //elimino la linea de codigo que esta abajo para sustituirla por
            //el apuntador para el singleton que cree un poco antes (var mainViewModel)
            //ya que uso varias veces el singleton
            //MainViewModel.GetInstance().Lands = new LandsViewModel();

            //Antes de instaciar la pagina, uso el singleton para
            //instanciar la nueva ViewModel que voy a vincular a la page, en este caso LandsViewModel
            mainViewModel.Lands = new LandsViewModel();
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
            this.Email = "jisv@me.com";
            this.Password = "123456";

            //instanciamos el servicio que nos va a traer el token
            this.apiService = new ApiService();

            this.IsRemembered = true;
            //por defecto los bool son false por lo que para habilitar
            //los botones tengo que poner IsEnabled true
            this.isEnabled = true;

            //http://restcountries.eu/rest/v2/all
        }
        #endregion
    }
}
