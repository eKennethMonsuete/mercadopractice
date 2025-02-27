using AutoMapper;
using DOMAIN.Models.Product;
using SERVICE.ProductService.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.Mapping
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<ProductUpdateDTO, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
