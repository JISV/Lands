using Newtonsoft.Json;

namespace Lands.Models
{
    public class Currency
    {
        //uso Newtonsoft.json para mantener mapeados
        //los campos del api con el estandar C#
        //vienen en minuscula y se ponen como propiedades en mayusculas

        [JsonProperty(PropertyName="code")]
        public string Code { get; set; }
        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName="symbol")]
        public string Symbol { get; set; }
    }
}
