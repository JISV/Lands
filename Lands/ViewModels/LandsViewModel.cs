﻿

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
        private ObservableCollection<Land> lands;
        private string filter;
        private List<Land> landsList;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
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
            this.Lands = new ObservableCollection<Land>(this.landsList);
            this.IsRefreshing = false;
        }

        private void Search()
        {
            if(string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<Land>(
                    this.landsList);
            }
            else
            {
                //busco por nombre del pais o de la capital
                this.Lands = new ObservableCollection<Land>(
                    this.landsList.Where(l =>l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                                         l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }

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
