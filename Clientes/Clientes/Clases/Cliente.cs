using System;
using System.Collections.Generic;
using System.Text;

namespace Clientes.Clases
{
    public class Cliente
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }
        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
        [Newtonsoft.Json.JsonProperty("userId")]
        public string UserId { get; set; }
        public DateTime DateUtc { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }      
        public int Celular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
    }

}
