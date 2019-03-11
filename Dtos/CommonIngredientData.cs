using Newtonsoft.Json;

namespace BeerScaler.Dtos {
    /// <summary>
    /// Data that's shared amongst all ingredients.
    /// </summary>
    public sealed class CommonIngredientData {
        /// <summary>
        /// The ingredient itself.
        /// </summary>
        public Ingredient Ingredient { get; set; }
        
        /// <summary>
        /// Indicates by how much this ingredient needs to be scaled.
        /// </summary>
        [JsonIgnore]
        public decimal ScalingFactor { get; set; }
    }
}