using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.ViewModels
{
    public class RecipeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано название")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Не указаны ингридиенты")]
        [Display(Name = "Игридиенты")]
        public string Ingridients { get; set; }
        [Required(ErrorMessage = "Не указано описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDb ImageData { get; set; }
    }

    public class BufferedSingleFileUploadDb
    {
        [Display(Name = "Фото")]
        public IFormFile FormFile { get; set; }
    }   
}
