
using Microsoft.AspNetCore.Mvc;
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

       public Task<ProductCreateResponseDTO> Create(ProductCreateDTO input);
        public  Task<IEnumerable<ProductGetAllResponseDTO>> FindAllAsync();

        public Task<ProductFindByIdResponse> FindByIdAsync(long id);

         public Task UpdateAsync( long id, ProductUpdateDTO input);

        public void DeleteAsync(long id);

    }
}
