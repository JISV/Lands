namespace Lands.ViewModels
{
using System.Collections.Generic;
using Lands.Models;

    public class MainViewModel
    {

        #region Propierties

        //esto era un atributo de la landsviewmodel
        //ahora lo convierto en una propiedad
        //publica y le cambio el nombre a LandsList

        public  List<Land> LandsList
        {
            get;
            set;
        }
        //creo una propieddad que guardara
        //el token durante la sesion
        public TokenResponse Token
        {
            get;
            set;
        }

        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public LandsViewModel Lands
        {
            get;
            set;
        }
        //LandDetailViewModel es la clase para mostrar
        //el detalle del pais
        public LandDetailViewModel Land
        {
            get;
            set;
        }
        #endregion

        //Singleton es un patron que se usa para instanciar la MainViewModel 
        //solo una vez y optimizar el uso de memoria
        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance==null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion
    }
}
