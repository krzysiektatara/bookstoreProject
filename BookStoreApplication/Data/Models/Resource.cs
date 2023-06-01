using System.Text.Json.Serialization;


namespace BookStoreApplicationAPI.Data.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
