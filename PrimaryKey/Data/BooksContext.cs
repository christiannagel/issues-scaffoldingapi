using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimaryKey;

    public class BooksContext : DbContext
    {
        public BooksContext (DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<PrimaryKey.Book> Book { get; set; } = default!;
    }
