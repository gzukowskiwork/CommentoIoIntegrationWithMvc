using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace CommentoIntegrationTest.Models.Config
{
    public class PeopleConfig : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder)
        {
            builder.HasData(
                new People
                {
                    Id = new Guid("cebcff66-9a78-4194-b284-1871bab13a22"),
                    Name = "Grzegorz",
                    Age = 36
                },
                new People
                {
                    Id = new Guid("fd9b9060-69d3-4646-89ab-3f455dff759f"),
                    Name = "Aga",
                    Age = 34
                });

           
        }
    }
}
