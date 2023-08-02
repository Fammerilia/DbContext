    using Microsoft.EntityFrameworkCore.Metadata.Builders;
using   Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderId);

        builder.Property(o => o.OrderDate)
               .IsRequired();

        builder.Property(o => o.OrderDescription)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(o => o.OrderAmount)
               .IsRequired();

        builder.Property(o => o.OrderStatus)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasOne(o => o.Client)
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.ClientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.ClientId);

        builder.Property(c => c.EmpName)
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(c => c.EmpSex)
               .IsRequired();

        builder.Property(c => c.Status);
        builder.Property(c => c.OrderCount)
               .HasDefaultValue(0);

        builder.Property(c => c.DiscountType)
               .HasDefaultValue(5);

        builder.HasMany(c => c.ClientContacts)
               .WithOne(cc => cc.Client)
               .HasForeignKey(cc => cc.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.ClientAddresses)
               .WithOne(ca => ca.Client)
               .HasForeignKey(ca => ca.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.ClientEmails)
               .WithOne(ce => ce.Client)
               .HasForeignKey(ce => ce.ClientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ClientContactConfiguration : IEntityTypeConfiguration<ClientContact>
{
    public void Configure(EntityTypeBuilder<ClientContact> builder)
    {
        builder.HasKey(c => c.ClientContactId);

        builder.Property(c => c.EmpContact)
               .IsRequired()
               .HasMaxLength(30)
               .HasAnnotation("RegularExpression", @"^\+?[0-9]*$")
               .HasAnnotation("RegularExpressionErrorMessage", "Invalid phone number format.");

        builder.Property(c => c.Contact_type)
               .IsRequired();
    }
}


public class ClientAddressesConfiguration : IEntityTypeConfiguration<ClientAddresses>
{
    public void Configure(EntityTypeBuilder<ClientAddresses> builder)
    {
        builder.HasKey(c => c.ClientAddressId);

        builder.Property(c => c.EmpAddress)
               .IsRequired()
               .HasMaxLength(30)
               .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9\s]+$")
               .HasAnnotation("RegularExpressionErrorMessage", "Invalid address format.");

        builder.HasOne(c => c.Client)
               .WithMany(c => c.ClientAddresses)
               .HasForeignKey(c => c.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Address_type)
               .IsRequired();
    }
}

public class ClientEmailConfiguration : IEntityTypeConfiguration<ClientEmail>
{
    public void Configure(EntityTypeBuilder<ClientEmail> builder)
    {
        builder.HasKey(c => c.ClientEmailId);

        builder.Property(c => c.EmpEmail)
               .IsRequired()
               .HasMaxLength(30)
               .HasConversion(email => email.ToLower(), email => email)
               .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
               .HasAnnotation("RegularExpressionErrorMessage", "Invalid email format.");

        builder.Property(c => c.Email_type)
               .IsRequired();
    }
}
