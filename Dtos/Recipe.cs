using System;
using System.Collections.Generic;
using BeerScaler.Data;

namespace BeerScaler.Dtos {
    /// <summary>
    /// Is a recipe. Values in the recipe will be scaled linearly.
    /// </summary>
    public sealed class Recipe {
        private Queue<MashStep> _mashSteps = new Queue<MashStep>();

        /// <summary>
        /// Creates a new recipe. This sets the thickness of the mass to the value
        /// specified in Constants.DefaultLitersPerKiloMalt.
        /// </summary>
        /// <param name="id">The id of the recipe.</param>
        /// <param name="name">The name of the recipe.</param>
        /// <param name="litersInRecipe">For how many liters this recipe is.</param>
        /// <param name="cookingTime">How long the wort should cook.</param>
        public Recipe(int id, string name, decimal litersInRecipe, decimal cookingTime) : this(id, name, litersInRecipe, cookingTime, Constants.DefaultLitersPerKiloMalt) { }

        /// <summary>
        /// Creates a new recipe.
        /// </summary>
        /// <param name="id">The id of the recipe.</param>
        /// <param name="name">The name of the recipe.</param>
        /// <param name="litersInRecipe">For how many liters this recipe is.</param>
        /// <param name="cookingTime">How long the wort should cook.</param>
        /// <param name="litersPerKiloMalt">The thickness of the mash.</param>
        public Recipe(int id, string name, decimal litersInRecipe, decimal cookingTime, decimal litersPerKiloMalt) {
            Malts = new List<WeightedIngredient>();
            Hops = new List<HopIngredient>();
            OtherIngredients = new List<WeightedIngredient>();

            StaticValues = new StaticRecipeValues {
                Liters = litersInRecipe,
                LitersPerKiloOfMalt = litersPerKiloMalt,
                Id = id,
                Name = name,
                CookingTime = cookingTime,
                LitersEvaporatedAfterCooking = cookingTime / 60m * Constants.LitersEvaporatedPerHour
            };
        }

        /// <summary>
        /// The values of the recipe that don't change based on the wanted volume.
        /// </summary>
        public StaticRecipeValues StaticValues { get; set; }

        /// <summary>
        /// The number of liters required to mash the malt.
        /// </summary>
        public decimal LitersForMashing { get; set; }

        /// <summary>
        /// How many liters are wanted.
        /// </summary>
        public decimal LitersWanted { get; set; }

        /// <summary>
        /// Indicates by how much the values in the recipe need to be scaled.
        /// </summary>
        public decimal ScalingFactor { get; set; }

        /// <summary>
        /// How much sparge water is required to reach the target, pre-boil volume.
        /// </summary>
        public decimal SpargeWater { get; set; }

        /// <summary>
        /// The grain mass, when scaled by the ScalingFactor.
        /// </summary>
        public decimal AdjustedGrainMass { get; private set; }

        /// <summary>
        /// Volume of the malt when dry
        /// </summary>
        public decimal MaltVolume { get; set; }

        /// <summary>
        /// The list of malts this recipe needs.
        /// </summary>
        public ICollection<WeightedIngredient> Malts { get; }

        /// <summary>
        /// The list of hops for this recipe.
        /// </summary>
        public ICollection<HopIngredient> Hops { get; }

        /// <summary>
        /// Other ingredients for this recipe.
        /// </summary>
        public ICollection<WeightedIngredient> OtherIngredients { get; }

        /// <summary>
        /// The mash steps for this recipe
        /// </summary>
        public IEnumerable<MashStep> MashSteps => _mashSteps;

        /// <summary>
        /// Adds a mashing step. 
        /// </summary>
        /// <param name="mashStep"></param>
        public void AddMashStep(MashStep mashStep) {
            _mashSteps.Enqueue(mashStep);
        }

        public void SetWantedLiters(decimal wantedLiters) {
            LitersWanted = wantedLiters;
            ScalingFactor = LitersWanted / StaticValues.Liters;

            foreach (var malt in Malts) {
                malt.CommonData.ScalingFactor = ScalingFactor;
                AdjustedGrainMass += malt.AdjustedKilograms;
                StaticValues.TotalGrainMass += malt.Kilograms;
            }

            foreach(var ingredient in OtherIngredients) {
                ingredient.CommonData.ScalingFactor = ScalingFactor;
            }

            foreach(var malt in Malts) {
                malt.PercentageOfGrainBill = Math.Round(malt.AdjustedKilograms / AdjustedGrainMass, 2, MidpointRounding.AwayFromZero) * 100;
            }

            foreach (var hop in Hops) {
                hop.CommonData.ScalingFactor = ScalingFactor;
            }

            LitersForMashing = Math.Round(AdjustedGrainMass * StaticValues.LitersPerKiloOfMalt, 2, MidpointRounding.AwayFromZero);

            MaltVolume = AdjustedGrainMass * Constants.VolumeOf1KilogramOfMalt;

            DetermineSpargeWater();
        }

        /// <summary>
        /// Determines how much sparge water is required to reach the required pre-boil volume.
        /// </summary>
        private void DetermineSpargeWater() {
            var litersEvaporatedDuringCookingPhase = (StaticValues.CookingTime / 60m) * Constants.LitersEvaporatedPerHour;
            var amountRequiredInPot = litersEvaporatedDuringCookingPhase + LitersWanted;
            SpargeWater = Math.Round(amountRequiredInPot - LitersForMashing + (AdjustedGrainMass * Constants.AmountOfWater1KilogramOfMaltAbsorbs), 2, MidpointRounding.AwayFromZero);
        }
    }
}