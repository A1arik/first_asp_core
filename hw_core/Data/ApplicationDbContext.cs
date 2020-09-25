using System;
using System.Collections.Generic;
using System.Text;
using hw_core.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hw_core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
    }
}
