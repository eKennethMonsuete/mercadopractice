﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.ProductService.DTO.Response
{
    public class ProductCreateResponseDTO
    {
       

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
