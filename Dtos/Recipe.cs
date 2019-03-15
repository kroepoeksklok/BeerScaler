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
        public decimal LitersForMashing => Math.Round(AdjustedGrainMass * StaticValues.LitersPerKiloOfMalt, 2, MidpointRounding.AwayFromZero);

        /// <summary>
        /// How many liters are wanted.
        /// </summary>
        public decimal LitersWanted { get; set; }

        /// <summary>
        /// Indicates by how much the values in the recipe need to be scaled.
        /// </summary>
        public decimal ScalingFactor => LitersWanted / StaticValues.Liters;

        /// <summary>
        /// How much sparge water is required to reach the target, pre-boil volume.
        /// </summary>
        public decimal SpargeWater { get; set; }

        /// <summary>
        /// The grain mass, when scaled by the ScalingFactor.
        /// </summary>
        public decimal AdjustedGrainMass { get; set; }

        /// <summary>
        /// Volume of the malt when dry
        /// </summary>
        public decimal MaltVolume => AdjustedGrainMass * Constants.VolumeOf1KilogramOfMalt;

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
    }
}