using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeBook.Models
{
    [Table("User")]
    public class User : IdentityUser 
    {
        //public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}