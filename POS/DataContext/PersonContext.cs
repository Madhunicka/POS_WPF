using Microsoft.EntityFrameworkCore;
using POS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DataContext
{
    public class PersonContext: DbContext
    {
        public DbSet<Person> Passwords { get; set; }
        private readonly string _path = @"C:\Users\user'\Desktop\POS\passwords.db";


        protected override void
            OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data source={_path}");
    }
}
