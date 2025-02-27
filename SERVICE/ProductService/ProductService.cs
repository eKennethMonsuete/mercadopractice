using AutoMapper;
using DOMAIN;
using DOMAIN.Models.Product;
using Microsoft.AspNetCore.Mvc;
using SERVICE.ProductService.DTO.Request;
using SERVICE.ProductService.DTO.Response;

namespace SERVICE.ProductService
{
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

       

        public async Task<IEnumerable<ProductGetAllResponseDTO>> FindAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductGetAllResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
               // Description = p.Description
            });
        }

        public async Task<ProductFindByIdResponse> FindByIdAsync(long id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return null; // Retorna null se o produto não for encontrado
            }

            return (new ProductFindByIdResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description

            });
        }

        public async Task UpdateAsync(long id ,ProductUpdateDTO input )
        {
            try
            {
                var existingProduct = await _repository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    // Lança a exceção corretamente, em vez de tentar retornar ela
                    throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");
                }

                // Mapeia os dados do DTO para a entidade existente
                _mapper.Map(input, existingProduct);

                // Atualiza o produto no repositório
                 await _repository.UpdateAsync(id, existingProduct);

                // Retorna uma resposta de sucesso (HTTP 200) com o produto atualizado
                
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
           
        }
        public void DeleteAsync(long id)
        {
            try
            {
                _repository.DeleteAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException("A entidade com o ID fornecido não foi encontrada.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Falha ao tentar deletar a entidade.", ex);
            }
        }
    }
}
