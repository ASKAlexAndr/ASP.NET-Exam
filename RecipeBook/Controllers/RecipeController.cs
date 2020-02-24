using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;
using RecipeBook.ViewModels;

namespace RecipeBook.Controllers
{
    public class RecipeController : Controller
    {
        private IdentityAppContext _context;
        public RecipeController(IdentityAppContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(IFormFile formFile, RecipeModel model)
        {
            if (ModelState.IsValid)
            {
                byte[] file = null;
                if (formFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        if (memoryStream.Length < 2097152)
                        {
                            file = memoryStream.ToArray();                      
                        }
                        else
                        {
                            throw new Exception("The file is too large.");
                        }
                    }
                }
                Recipe recipe = new Recipe
                {
                    Description = model.Description,
                    Ingridients = model.Ingridients,
                    Title = model.Title,
                    ImageData = file,
                };
                try
                {
                    await _context.Recipes.AddAsync(recipe);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("RecipeList");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RecipeList(string filterTitle = null, string sortDir = "asc", int page = 1)
        {
            int pageSize = 10;

            IQueryable<Recipe> list = _context.Recipes;
            if (filterTitle != null)
            {
                list = list.Where(r => r.Title.Contains(filterTitle));
            }
            if (sortDir == "desc")
            {
                list = list.OrderByDescending(r => r.Title);
            } else
            {
                list = list.OrderBy(r => r.Title);
            }
            int count = await list.CountAsync();
            var items = await list.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            RecipeListViewModel viewModel = new RecipeListViewModel
            {
                Recipes = items,
                PageViewModel = new RecipePageViewModel(count, page, pageSize),
                FilterTitle = filterTitle,
                SortDir = sortDir
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            Recipe recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null)
            {
                return View();
            }
            return View(recipe);
        }
        [Authorize(Roles="admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Recipe recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            return View(recipe);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(IFormFile formFile, Recipe model)
        {
            //await _context.SaveChangesAsync();
            //return RedirectToAction("Index", "Recipe", model.Id);
            if (ModelState.IsValid)
            {
                Recipe recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == model.Id);
                
                if (formFile != null)
                {
                    byte[] file = null;
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        if (memoryStream.Length < 2097152)
                        {
                            file = memoryStream.ToArray();
                        }
                        else
                        {
                            throw new Exception("The file is too large.");
                        }
                    }
                    recipe.ImageData = file;
                }
                recipe.Title = model.Title;
                recipe.Ingridients = model.Ingridients;
                recipe.Description = model.Description;
                try
                {
                    _context.Recipes.Update(recipe);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Recipe", new { id = model.Id});
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Recipe recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("RecipeList", "Recipe");
        }
    }
}