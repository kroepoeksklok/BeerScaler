using BeerScaler.Data;
using BeerScaler.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using BeerScaler.Calculations;

namespace BeerScaler.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RecipesController : ControllerBase {
        private readonly Recipes _recipes = new Recipes();


        [HttpGet()]
        public ActionResult GetRecipes() {
            // Since we're only interested in the name and Id in order to populate a combo box,
            // returning an simple anonymous type will suffice
            return Ok(_recipes.OrderBy(r => r.StaticValues.Name).Select(r => new {
                Id = r.StaticValues.Id,
                Name = r.StaticValues.Name
            }).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipe(int id) {
            return GetRecipeFromList(id);
        }

        [HttpGet("{id}/{wantedLiters}")]
        public ActionResult<Recipe> GetRecipe(int id, decimal wantedLiters) {
            return GetRecipeFromList(id, wantedLiters);
        }

        private ActionResult<Recipe> GetRecipeFromList(int id, decimal? wantedLiters = null) {
            var recipe = _recipes.GetRecipe(id);

            if (recipe == null) {
                return NotFound();
            }

            RecipeScaler.ScaleRecipe(recipe, wantedLiters);

            return recipe;
        }
    }
}
