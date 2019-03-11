using System;

namespace BeerScaler.Dtos {
    /// <summary>
    /// A hop ingredient resembles the hop that must be used in the recipe.
    /// </summary>
    public sealed class HopIngredient {
        /// <summary>
        /// Creates a new hop ingredient
        /// </summary>
        /// <param name="hop">The hop</param>
        /// <param name="aau">How much hop must be added, in AAU</param>
        /// <param name="cookingTime">The cooking time of the hop, in minutes</param>
        public HopIngredient(Ingredient hop, decimal aau, int cookingTime) {
            CommonData = new CommonIngredientData {
                Ingredient = hop,
                ScalingFactor = 1
            };

            AAU = aau;
            CookingTime = cookingTime;
        }

        /// <summary>
        /// Contains data that's shared amongst all other ingredients.
        /// </summary>
        public CommonIngredientData CommonData { get; }

        /// <summary>
        /// The amount of hop, in AAU
        /// </summary>
        /// <value></value>
        public decimal AAU { get; }

        /// <summary>
        /// The cooking time of this hop, in minutes
        /// </summary>
        public int CookingTime { get; }

        /// <summary>
        /// The AAU to use in the recipe, based on the scaling factor.
        /// </summary>
        public decimal AdjustedAAU => Math.Round(AAU * CommonData.ScalingFactor, 2, MidpointRounding.AwayFromZero);
    }
}