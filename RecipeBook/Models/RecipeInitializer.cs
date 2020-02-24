using AngleSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class RecipeInitializer
    {
        public static async Task InitializeAsync(IdentityAppContext dbContext)
        {
            if (await dbContext.Recipes.CountAsync() == 0)
            {
                Console.WriteLine("Start PARSING");
                var config = Configuration.Default.WithDefaultLoader();
                List<string> addresses= new List<string> {
                    "https://povar.ru/recipes/salat_cezar_s_kuricei_i_suharikami-304.html",
                    "https://povar.ru/recipes/shashlyk_iz_govyadiny_v_soevom_souse-76094.html",
                    "https://povar.ru/recipes/salat_gribnaya_polyana-1423.html",
                    "https://povar.ru/recipes/kurinaya_pechen_s_gribami-1358.html",
                    "https://povar.ru/recipes/semga_v_vine-9422.html",
                    "https://povar.ru/recipes/kurinyi_shashlyk_v_soevom_marinade_s_maslinami-9502.html",
                    "https://povar.ru/recipes/shashlyk_po-tatarski-15871.html",
                    "https://povar.ru/recipes/mcvadi-22353.html",
                    "https://povar.ru/recipes/borsh_na_kefire-40808.html",
                    "https://povar.ru/recipes/ahoblanko-22023.html",
                    "https://povar.ru/recipes/klubnichnyi_gaspacho-11707.html",
                    "https://povar.ru/recipes/tort_pancho-813.html",
                    "https://povar.ru/recipes/tort_snikers-7407.html",
                    "https://povar.ru/recipes/tort_muraveinik-818.html",
                    "https://povar.ru/recipes/medovyi_biskvit-8985.html",
                    "https://povar.ru/recipes/makovyi_tort_s_iogurtovym_mussom_i_belym_shokoladom-70933.html",
                    "https://povar.ru/recipes/krabovyi_salat_iz_krabovyh_palochek-823.html",
                    "https://povar.ru/recipes/salat_podsolnuh-821.html",
                    "https://povar.ru/recipes/kurochka_s_dymkom-53191.html",
                    "https://povar.ru/recipes/pasta_pomodoro-9576.html",
                    "https://povar.ru/recipes/karbonara-54279.html",
                    "https://povar.ru/recipes/bukatini_s_vetchinoi-68869.html",
                    "https://povar.ru/recipes/sous_k_paste-47620.html",
                    "https://povar.ru/recipes/pasta_s_brokkoli_i_orehami-71681.html",
                    "https://povar.ru/recipes/mumii_na_hellouin-2065.html",
                    "https://povar.ru/recipes/svinina_po-valliiski-72907.html",
                    "https://povar.ru/recipes/tykva_s_pecheniu-53844.html",
                    "https://povar.ru/recipes/chanahi_klassicheskie-45953.html",
                    "https://povar.ru/recipes/kurinaya_skoblyanka-62714.html",
                    "https://povar.ru/recipes/myaso-garmoshka_v_duhovke-29248.html",
                    "https://povar.ru/recipes/niurnbergskaya_zapechennaya_svinina-73271.html",
                    "https://povar.ru/recipes/utka_farshirovannaya_kliukvoi_i_yablokami-34816.html",
                    "https://povar.ru/recipes/utka_v_duhovke_s_kartofelem-33834.html",
                    "https://povar.ru/recipes/kurica_po-domashnemu_s_travami-66686.html",
                    "https://povar.ru/recipes/myaso_po-gruzinski-31130.html",
                    "https://povar.ru/recipes/salat_sushi-73573.html",
                    "https://povar.ru/recipes/salat_olive_klassicheskii_s_kolbasoi-41597.html",
                    "https://povar.ru/recipes/teplyi_salat_s_kuricei-10850.html",
                    "https://povar.ru/recipes/salat_s_gribami_i_fasoliu-37185.html",
                    "https://povar.ru/recipes/sup_s_kuricei_risom_i_plavlenym_syrom-70673.html",
                    "https://povar.ru/recipes/naggetsy_kak_v_makdonaldse-15749.html",
                    "https://povar.ru/recipes/sup_s_syrnymi_ruletikami-73402.html",
                    "https://povar.ru/recipes/salat_novogodnyaya_svinka-72255.html",
                    "https://povar.ru/recipes/kurica_zapechennaya_s_adjikoi-72996.html",
                    "https://povar.ru/recipes/chahohbili_iz_kuricy_po-gruzinski-42022.html",

                };
                var context = BrowsingContext.New(config);
                foreach (string address in addresses)
                {

                    var document = await context.OpenAsync(address);
                    //Selectors
                    var titleSelector = "h1[itemprop=name]";
                    var indigrientsSelector = "li[itemprop=ingredients]";
                    var recipeTextSelector = "div.detailed_step_description_big";
                    var imageSelector = "img[itemprop=image]";
                    //Cells
                    var titleCell = document.QuerySelector(titleSelector);
                    var indigrientsCells = document.QuerySelectorAll(indigrientsSelector);
                    var recipeCells = document.QuerySelectorAll(recipeTextSelector);
                    var imageCell = document.QuerySelector(imageSelector);
                    //Data
                    var title = titleCell.TextContent;
                    var indigrientsList = indigrientsCells.Select(r => r.TextContent);
                    var recipeText = recipeCells.Select(r => r.TextContent);
                    var imageUrl = imageCell.GetAttribute("src");
                    var ingridients = $"{String.Join("\n", indigrientsList)}";
                    var desc = $"{String.Join("\n", recipeText)}";
                    try
                    {
                        var webClient = new WebClient();
                        byte[] imageBytes = webClient.DownloadData(imageUrl);
                        Recipe recipe = new Recipe
                        {
                            Title = title,
                            ImageData = imageBytes,
                            Description = desc,
                            Ingridients = ingridients
                        };
                        dbContext.Recipes.Add(recipe);
                    } catch (Exception ex)
                    {
                        
                    }
                    dbContext.SaveChanges();
                }
                Console.WriteLine("END PARSING");
            }
        }
    }
}
