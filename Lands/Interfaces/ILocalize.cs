namespace Lands.Interfaces
{

    //esta interfaz se implementa en cada
    //plataforma porque es diferente en cada ios, android y UWP
    using System.Globalization;
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);


    }
}
