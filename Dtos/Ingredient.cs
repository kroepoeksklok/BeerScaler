namespace BeerScaler.Dtos {
    /// <summary>
    /// A generic ingredient.
    /// </summary>
    public sealed class Ingredient {
        /// <summary>
        /// Creates a new ingredient with the specified name.
        /// </summary>
        /// <param name="name">The name of the ingredient</param>
        public Ingredient(string name) {
            Name = name;
        }

        /// <summary>
        /// Creates a new ingredient with the specified name and manufacturer.
        /// </summary>
        /// <param name="name">The name of the ingredient</param>
        /// <param name="manufacturer">The manufacturer of the ingredient</param>
        public Ingredient(string name, string manufacturer) : this(name) {
            Manufacturer = manufacturer;
        }

        /// <summary>
        /// The name of the ingredient
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The manufacturer of the ingredient
        /// </summary>
        public string Manufacturer { get; set; }
    }
}