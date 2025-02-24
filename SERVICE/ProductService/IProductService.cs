using SERVICE.Common;
using SERVICE.ProductService.DTO.Request;
using SERVICE.ProductService.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.ProductService
{
   public interface IProductService
    {

       public Task<Result<ProductCreateResponseDTO>> Create(ProductCreateDTO input);

    }
}
