using System;
using Newtonsoft.Json;

namespace BeerScaler.Dtos {
    /// <summary>
    /// An ingredient whose weight is determined by the wanted volume.
    /// </summary>
    public sealed class WeightedIngredient {
        /// <summary>
        /// Creates a new weighted ingredient.
        /// </summary>
        /// <param name="ingredient">The ingredient to add</param>
        /// <param name="kilograms">How many kilograms of the ingredient to add.</param>
        public WeightedIngredient(Ingredient ingredient, decimal kilograms) {
            Kilograms = kilograms;

            CommonData = new CommonIngredientData {
                Ingredient = ingredient,
                ScalingFactor = 1
            };
        }

        /// <summary>
        /// Contains data that's shared amongst all other ingredients.
        /// </summary>
        public CommonIngredientData CommonData { get; }

        /// <summary>
        /// The amount required
        /// </summary>
        public decimal Kilograms { get; }

        /// <summary>
        /// How many percent of the total grain mass this ingredient is. Only applicable to malts.
        /// </summary>
        public decimal? PercentageOfGrainBill { get; set; }

        /// <summary>
        /// How many kilograms to use in the recipe. It is based on the requested volume.
        /// </summary>
        public decimal AdjustedKilograms => Math.Round(Kilograms * CommonData.ScalingFactor, 3, MidpointRounding.AwayFromZero);
    }
}