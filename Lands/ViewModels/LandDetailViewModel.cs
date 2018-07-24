namespace Lands.ViewModels
{
    using Lands.Models;
        
    public class LandDetailViewModel
    {

        //Eliminamos el atributo privado
        //private LandItemViewModel landItemViewModel;
        //y creamos una propiedad Land

        #region Properties
        public Land Land
        {
            get;
            set;
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
        }
        #endregion
    }
}
