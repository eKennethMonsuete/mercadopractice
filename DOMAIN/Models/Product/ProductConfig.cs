using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.Product
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));

            // Define a chave primária
            builder.HasKey(p => p.Id);

            // Configuração das propriedades
            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Price);
            builder.Property(p => p.Description);



        }
    }
}
