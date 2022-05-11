using System.Text.Json.Serialization;

namespace XepaCommerce.src.utilidades
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        ADMINISTRADOR
    }
}
