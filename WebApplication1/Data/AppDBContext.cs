using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Data;

        public class AppDBContext : DbContext
        {
            public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options){ }
    public DbSet <Account> Accounts => Set <Account>();

    public DbSet <Transaction> Transactions => Set <Transaction>();
        }
    

