namespace Lands.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Lands.Views;
    using Models;
    using Xamarin.Forms;

    //Esta clase se usa como una truco
    //para mantener el modelo MVVM y no pegar
    //una view a un model y esta hereda de la clase base
    //de la data, esta es un espejo de la clase Land

    public class LandItemViewModel : Land
    {
        #region Commands
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }

        private async void SelectLand()
        {
            //Para pintar la LandDetailPage debemos invocar primero la ViewModel relacionada
            // a la page, es decir la LandDetailViewModel, para ello 
            //invocamos la MainViewModel utilizando el GetIntance() del patron Singleton
            //El objeto es el pais seleccionado (this)
            MainViewModel.GetInstance().Land = new LandDetailViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new LandDetailPage());
        }
        #endregion

        public LandItemViewModel()
        {
            
        }
    }
}
