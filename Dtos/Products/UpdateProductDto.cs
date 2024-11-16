using crudAPI.Data.Models;

namespace crud_api2.Dtos.Products
{
    public class UpdateProductDto
    {
        public string Name { get; set; }

        public int price { get; set; }
        public string Description { get; set; }

        internal void Adapt(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
