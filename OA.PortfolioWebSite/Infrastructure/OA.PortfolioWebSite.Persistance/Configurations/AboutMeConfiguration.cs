using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Persistance.Configurations
{
    public class AboutMeConfiguration : IEntityTypeConfiguration<AboutMe>
    {
        public void Configure(EntityTypeBuilder<AboutMe> builder)
        {
            builder.HasKey(am => am.Id);

            builder.Property(am => am.Introduction)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(am => am.ImageUrl1)
                   .HasMaxLength(255);

            builder.Property(am => am.ImageUrl2)
                   .HasMaxLength(255);

            
        }
    }
}
