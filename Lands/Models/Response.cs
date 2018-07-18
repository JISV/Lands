namespace Lands.Models
{
    public class Response
    {
        //Esta clase me indica si la comunicacion fue exitosa
        //si viene success true entonces me trae el objeto eesult
        //si algo fallo me trae el mensage de error

        public bool IsSuccess
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public object Result
        {
            get;
            set;
        }
    }
}
