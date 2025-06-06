﻿using Microsoft.EntityFrameworkCore;
using PublisherDomain;
using System.Net;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=PubDatabase"
                ); //Integrated Security=True;TrustServerCertificate=True
        }
    }
}
