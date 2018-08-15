namespace Lands.Helpers
{
    using Xamarin.Forms;

    using Interfaces;


    using Resources;



    public static class Languages

    {

        static Languages()

        {

            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

            Resource.Culture = ci;

            DependencyService.Get<ILocalize>().SetLocale(ci);

        }



        public static string Accept

        {

            get { return Resource.Accept; }

        }



        public static string Error

        {

            get { return Resource.Error; }

        }



        public static string EmailValidation

        {

            get { return Resource.EmailValidation; }

        }



        public static string PasswordValidation

        {

            get { return Resource.PasswordValidation; }

        }

        public static string TokenNullValidation

        {

            get { return Resource.TokenNullValidation; }

        }

        public static string RememberMe

        {

            get { return Resource.RememberMe; }

        }

        public static string EmailPlaceholder

        {

            get { return Resource.EmailPlaceholder; }

        }


        public static string Email

        {

            get { return Resource.Email; }

        }

        public static string Password

        {

            get { return Resource.Password; }

        }

        public static string EnterPassword

        {

            get { return Resource.EnterPassword; }

        }

        public static string Register

        {

            get { return Resource.Register; }

        }

        public static string InternetSettings

        {
            //InternetConnection
            get { return Resource.InternetSettings; }

        }

        public static string InternetConnection

        {
            
            get { return Resource.InternetConnection; }

        }

        public static string Countries

        {

            get { return Resource.Countries; }

        }


        public static string SearchPlaceholder

        {

            get { return Resource.SearchPlaceholder; }

        }
    }
}
