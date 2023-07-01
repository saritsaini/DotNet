using System.Text.Json.Serialization;

namespace DotNet.Models
{

[JsonConverter(typeof(JsonStringEnumConverter))]    
public enum RPG
    {
        Knight=1,
        Dice=2

    }
}