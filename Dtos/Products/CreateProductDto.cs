using System.ComponentModel.DataAnnotations;

namespace crud2_newfeature.Dtos.Products
{
    public class CreateProductDto
    {
       
        
        public string Name { get; set; }
        public int price { get; set; }
        public string Description { get; set; }
    }
}
