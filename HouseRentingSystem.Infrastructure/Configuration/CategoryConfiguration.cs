using HouseRentingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystem.Infrastructure.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(CollectionOfCategories());
        }


        private List<Category> CollectionOfCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Cottage"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Single-Family"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Duplex"
                }
            };

            return categories;
        }
    }
}
