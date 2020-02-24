using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeBook.Models
{
    [Table("Recipe")]
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Ingridients { get; set; }
        public string Description { get; set; }

        public byte[] ImageData { get; set; }
    }
}