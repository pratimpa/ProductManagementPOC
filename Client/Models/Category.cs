namespace Client.Models
{
    using System.Text.Json.Serialization;

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }



}
