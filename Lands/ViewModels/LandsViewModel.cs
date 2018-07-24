

namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion


        #region Attributes

        private bool isRefreshing;
        //Sustituyo la clase del modelo Land por la del espejo
        //LandItemViewModel para no alterar el patron MVVM
        //private ObservableCollection<Land> lands;
        private ObservableCollection<LandItemViewModel> lands;
        private string filter;
        private List<Land> landsList;
        #endregion

        #region Properties
        //Sustituyo la clase del modelo Land por la del espejo
        //LandItemViewModel para no alterar el patron MVVM
        //public ObservableCollection<Land> Lands;
        public ObservableCollection<LandItemViewModel> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set { 
                SetValue(ref this.filter, value);
                //pongo esta instruccion aqui para que me vaya 
                //actualizando la busqueda del searchbar cada vez que 
                //coloque un caracter
                this.Search();
            }
        }
        #endregion

        #region Constructors
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();

        }

        #endregion


        #region Methods
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                "Error",
                connection.Message,
                "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                "Error",
                response.Message,
                "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            //Si llega hasta aqui es que tengo la lista
            //Asi que creo una var lista y la casteo a un objeto tipo lista
            //luego lo convieto en un obsevablecollection

            this.landsList = (List<Land>)response.Result;

            //Sustituyo la clase del modelo Land por la del espejo
            //LandItemViewModel para no alterar el patron MVVM
            //this.Lands = new ObservableCollection<Land>(this.landsList)

            //la forma de armar la lista cambio porque inicialmente
            //una nacia de la clase Land y ahora de otra collection
            //diferente que es del tipo LandItemViewModel
            //ambas heredan de la misma pero no son compatibles los tipos de objetos


            //Hay que hacer una transformacion de listas de una de tipo 
            //Land por una de LandItemViewModel

            this.Lands = new ObservableCollection<LandItemViewModel>(
                //creamos un nuevo metodo ToLandItemViewModel para hacer 
                //la transformacion de listas
                //
                //Adicionalmente como landsList es una propiedad global no
                //se necesita pasarla como parametro
                //this.Lands = new IEnumerable<LandItemViewModel> ToLandItemViewModel(List<Land> landsList)


                ToLandItemViewModel());
            this.IsRefreshing = false;
        }



        private void Search()
        {
            if(string.IsNullOrEmpty(this.Filter))
            {
                //cambio el filtro para poder leer una lista
                //de tipo LandsItenmViewModel con el metodo ToLandItemViewModel
                //this.Lands = new ObservableCollection<LandItemViewModel>(
                //  this.landsList();
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    ToLandItemViewModel());
            }
            else
            {
                //busco por nombre del pais o de la capital

                //En el filtro tambien sustituyo la clase del modelo Land por la del espejo
                //LandItemViewModel para no alterar el patron MVVM
                //this.Lands = new ObservableCollection<Land>(


                // this.Lands = new ObservableCollection<LandItemViewModel>(
                //    this.tolandsList().Where

                //lo que esta mas arriba lo cambie por esta instruccion que usa 
                //el metodo ToLandItemViewModel()
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel().Where(l =>l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                                         l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }

        }
        //creamos un nuevo metodo ToLandItemViewModel para hacer 
        //la transformacion de listas
        //como landsList es una propiedad global no se necesita pasarla como parametro
        //private IEnumerable<LandItemViewModel> ToLandItemViewModel(List<Land> landsList)

        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
           //Usamos linq para hacer el cambio en una sola instruaccion
            return this.landsList.Select(l => new LandItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }



        #endregion
    }
}
