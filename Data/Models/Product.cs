using System.ComponentModel.DataAnnotations;

namespace crudAPI.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int price { get; set; }

        public string? Description { get; set; }

        internal object Adapt<T>()
        {
            throw new NotImplementedException();
        }
    }

}
