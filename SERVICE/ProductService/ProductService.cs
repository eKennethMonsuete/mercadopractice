using DOMAIN;
using DOMAIN.Models.Product;
using Microsoft.EntityFrameworkCore;
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
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductCreateResponseDTO>> Create(ProductCreateDTO input)
        {
            try
            {
                var validationResult = ValidateProductCreateDTO(input);
                if (!validationResult.Success)
                    return Result<ProductCreateResponseDTO>.Fail(validationResult.ErrorMessage);

                var product = new Product
                {
                    Name = input.Name,
                    Price = input.Price,
                    Description = input.Description
                };

                var createdProduct = await _repository.AddAsync(product);

                var response = new ProductCreateResponseDTO
                {
                    Id = createdProduct.Id,
                    Name = createdProduct.Name,
                    Price = createdProduct.Price,
                    Description = createdProduct.Description
                };

                return Result<ProductCreateResponseDTO>.Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar produto: {ex.Message}");
                return Result<ProductCreateResponseDTO>.Fail("Erro inesperado ao criar o produto.");
            }
        }

        private Result<bool> ValidateProductCreateDTO(ProductCreateDTO input)
        {
            if (input == null)
                return Result<bool>.Fail("Produto não pode ser nulo");

            if (input.Price <= 0)
                return Result<bool>.Fail("O preço do produto deve ser maior que zero");

            return Result<bool>.Ok(true);
        }
    }
}
