using BeerScaler.Dtos;

namespace BeerScaler.Dtos {
    /// <summary>
    /// Attributes of a recipe that don't alter based on the wanted volume.
    /// </summary>
    public sealed class StaticRecipeValues {
        /// <summary>
        /// Id of the recipe
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the recipe
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The total amount of time the wort needs to cook in minutes.
        /// </summary>
        public decimal CookingTime { get; set; }

        /// <summary>
        /// Number of liters according to the source recipe
        /// </summary>
        public decimal Liters { get; set; }

        /// <summary>
        /// The number of liters per kilogram of malt
        /// </summary>
        public decimal LitersPerKiloOfMalt { get; set; }

        /// <summary>
        /// Total mass of all of the grains, in kilograms.
        /// </summary>
        public decimal TotalGrainMass { get; set; } 

        /// <summary>
        /// Description on how to perform the mash
        /// </summary>
        public string MashStepDescription { get; set; }

        /// <summary>
        /// The yeast to use. Does not take the number of cells required into account.
        /// </summary>
        public Ingredient Yeast { get; set; }

        /// <summary>
        /// How to perform the cooking step.
        /// </summary>
        public string CookingStepDescription { get; set; }

        /// <summary>
        /// The specific weight of the wort prior to fermentation.
        /// </summary>
        public decimal BeginSpecificWeight { get; set; }

        /// <summary>
        /// The specific weight of the wort after fermentation.
        /// </summary>
        public decimal EndSpecificWeight { get; set; }

        /// <summary>
        /// How to perform the fermentation
        /// </summary>
        public string FermentationStepDescription { get; set; }

        /// <summary>
        /// What to do during the bottling phase.
        /// </summary>
        public string BottlingDescription { get; set; }

        /// <summary>
        /// The minimum volume of CO₂ that this beer should target.
        /// </summary>
        /// <value></value>
        public decimal MinimumCO2 { get; set; }


        /// <summary>
        /// The maximum volume of CO₂ that this beer should target.
        /// </summary>
        public decimal MaximumCO2 { get; set; }

        /// <summary>
        /// Approximation of how many liters will evaporate during the boil phase.
        /// </summary>
        /// <value></value>
        public decimal LitersEvaporatedAfterCooking { get; set; }
    }
}