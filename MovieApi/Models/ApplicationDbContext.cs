﻿using Microsoft.EntityFrameworkCore;

namespace MovieApi.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }

        public DbSet<Genre> Genres { get; set; }   
        public DbSet<Movie> Movies { get; set; }
    }
}
