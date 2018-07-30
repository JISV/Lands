namespace Lands.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Lands.Models;

    public class LandDetailViewModel:BaseViewModel
    {

        #region Attributes
        //Eliminamos el atributo privado
        //private LandItemViewModel landItemViewModel;
        //y creamos una propiedad Land

        private ObservableCollection<Border> borders;
        //creamos el observablecollection para currencies
        //a fin de pintalo en una list view pq el objeto
        //currency del modelo no es una collection
        private ObservableCollection<Currency> currencies;
        //lo mismo para Languages
        private ObservableCollection<Language> languages;

        #endregion

        #region Properties
        public Land Land
        {
            get;
            set;
        }

        public ObservableCollection<Border> Borders
        {
            get { return this.borders; }
            set { this.SetValue(ref this.borders, value); }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return this.currencies; }
            set { this.SetValue(ref this.currencies, value); }
        }

        public ObservableCollection<Language> Languages
        {
            get { return this.languages; }
            set { this.SetValue(ref this.languages, value); }
        }
        #endregion

        #region Methods
        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach(var border in this.Land.Borders)
            {
                var land = MainViewModel.GetInstance().LandsList.
                                        Where(l => l.Alpha3Code == border).
                                        FirstOrDefault();

                if (land != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name,
                    });
                }
            }

           
        }
        #endregion


        #region Constructors
        //Al constructor le estoy pasando una LandItemViewModel
        //pero la puedo sustituir por una Land por que 
        //herendan de la misma clase
        //public LandDetailViewModel(LandItemViewModel landItemViewModel)

        public LandDetailViewModel(Land land)
        {
            //cambiamos el constructor y le pasamos
            //a la viewmodel el pais por el constructor
            //this.landItemViewModel = landItemViewModel;
            this.Land = land;
            this.LoadBorders();
            //cargamos directamente las currencies sin necesidad
            //de un metodo, solo creamos la ObsColl y le pasamos
            //el parametro de las currencies del obj Land
            this.Currencies = new ObservableCollection<Currency>(this.Land.Currencies);

            this.Languages = new ObservableCollection<Language>(this.Land.Languages);

        }


        #endregion
    }
}
