using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.ProductService.DTO.Request
{
    public class ProductCreateDTO
    {
        
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
