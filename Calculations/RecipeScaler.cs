using BeerScaler.Data;
using BeerScaler.Dtos;
using System;

namespace BeerScaler.Calculations {
    public static class RecipeScaler {
        /// <summary>
        /// Scales the recipe based on the number of wanted liters.
        /// </summary>
        /// <param name="recipe">The recipe to scale.</param>
        /// <param name="wantedLiters">The number of liters wanted by the user/</param>
        public static void ScaleRecipe(Recipe recipe, decimal? wantedLiters) {
            SetWantedLiters(recipe, wantedLiters);
            SetAdjustedGrainMass(recipe);
            SetAdjustedIngredientMass(recipe);
            SetAdjustedHopMass(recipe);
            SetSpargeWater(recipe);
        }

        private static void SetWantedLiters(Recipe recipe, decimal? wantedLiters) {
            if (wantedLiters.HasValue) {
                recipe.LitersWanted = wantedLiters.Value;
            } else {
                recipe.LitersWanted = recipe.StaticValues.Liters;
            }
        }

        private static void SetAdjustedGrainMass(Recipe recipe) {
            foreach (var malt in recipe.Malts) {
                malt.CommonData.ScalingFactor = recipe.ScalingFactor;
                recipe.AdjustedGrainMass += malt.AdjustedKilograms;
                recipe.StaticValues.TotalGrainMass += malt.Kilograms;
            }

            foreach (var malt in recipe.Malts) {
                malt.PercentageOfGrainBill = Math.Round(malt.AdjustedKilograms / recipe.AdjustedGrainMass, 2, MidpointRounding.AwayFromZero) * 100;
            }
        }

        private static void SetAdjustedIngredientMass(Recipe recipe) {
            foreach (var ingredient in recipe.OtherIngredients) {
                ingredient.CommonData.ScalingFactor = recipe.ScalingFactor;
            }
        }

        private static void SetAdjustedHopMass(Recipe recipe) {
            foreach (var hop in recipe.Hops) {
                hop.CommonData.ScalingFactor = recipe.ScalingFactor;
            }
        }

        private static void SetSpargeWater(Recipe recipe) {
            var litersEvaporatedDuringCookingPhase = (recipe.StaticValues.CookingTime / 60m) * Constants.LitersEvaporatedPerHour;
            var amountRequiredInPot = litersEvaporatedDuringCookingPhase + recipe.LitersWanted;
            recipe.SpargeWater = Math.Round(amountRequiredInPot - recipe.LitersForMashing + (recipe.AdjustedGrainMass * Constants.AmountOfWater1KilogramOfMaltAbsorbs), 2, MidpointRounding.AwayFromZero);
        }
    }
}