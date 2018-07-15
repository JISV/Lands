using System;
namespace Lands.ViewModels
{
    public class MainViewModel
    {
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
