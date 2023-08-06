using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
        public DbSet<ClientEmail> ClientEmails { get; set; }
        public DbSet<ClientAddresses> ClientAddresses { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("EmployeeDbConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new ClientContactConfiguration());
            modelBuilder.ApplyConfiguration(new ClientEmailConfiguration());
            modelBuilder.ApplyConfiguration(new ClientAddressesConfiguration());
        }
    }