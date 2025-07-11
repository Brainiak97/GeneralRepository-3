using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolyclinicService.Domain.Models.Entities;

namespace PolyclinicService.DAL.Infrastructure.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="Polyclinic"/> в базе данных.
/// </summary>
internal class PolyclinicEntityTypeConfiguration : IEntityTypeConfiguration<Polyclinic>
{
    public void Configure(EntityTypeBuilder<Polyclinic> builder)
    {
        builder
            .ToTable(
                "polyclinics",
                t => t.HasComment("Поликлиники"))
            .HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasComment("Идентификатор поликлиники")
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();
        
        builder.Property(p => p.Name)
            .HasComment("Наименование поликлиники")
            .HasMaxLength(255)
            .HasColumnName("name")
            .IsRequired();
        
        builder.Property(p => p.Address)
            .HasComment("Адрес")
            .HasMaxLength(255)
            .HasColumnName("address")
            .IsRequired();
        
        builder.Property(p => p.PhoneNumber)
            .HasComment("Номер телефона")
            .HasMaxLength(15)
            .HasColumnName("phone_number")
            .IsRequired();
        
        builder.Property(p => p.Email)
            .HasComment("Адрес электронной почты")
            .HasMaxLength(100)
            .HasColumnName("email");
        
        builder.Property(p => p.Url)
            .HasComment("Ссылка на сайт")
            .HasMaxLength(500)
            .HasColumnName("url");
    }
}