using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class IdentityAppContext : IdentityDbContext<User>
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options)
           : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
 