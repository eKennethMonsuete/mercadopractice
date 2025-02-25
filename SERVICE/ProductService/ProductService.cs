using DOMAIN;
using DOMAIN.Models.Product;
using SERVICE.ProductService.DTO.Request;
using SERVICE.ProductService.DTO.Response;

namespace SERVICE.ProductService
{
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductCreateResponseDTO> Create(ProductCreateDTO input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Price <= 0)
            {
                throw new ArgumentException("O preço deve ser maior que 0,01", nameof(input.Price));
            }

            try
            {
                var product = new Product
                {
                    Name = input.Name,
                    Price = input.Price,
                    Description = input.Description
                };

                var createdProduct = await _repository.AddAsync(product);

                return new ProductCreateResponseDTO
                {
                    Id = createdProduct.Id,
                    Name = createdProduct.Name,
                    Price = createdProduct.Price,
                    Description = createdProduct.Description
                };
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
