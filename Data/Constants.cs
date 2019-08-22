namespace BeerScaler.Data {
    /// <summary>
    /// Provides a list of constants. 
    /// </summary>
    public static class Constants {
        /// <summary>
        /// Indicates a default mash thickness. Default is 3.5 liters per kilo.
        /// </summary>
        public const decimal DefaultLitersPerKiloMalt = 3.5m;

        /// <summary>
        /// Indicates the volume of 1 kilogram of malt, in liters.
        /// </summary>
        public const decimal VolumeOf1KilogramOfMalt = 0.66689m;

        /// <summary>
        /// How many liters a single kilogram of malt absorbs.
        /// </summary>
        public const decimal AmountOfWater1KilogramOfMaltAbsorbs = 0.9478673m;

        /// <summary>
        /// How many liters are evaporated per hour during the boil phase.
        /// </summary>
        public const decimal LitersEvaporatedPerHour = 2.98m;

        /// <summary>
        /// Grams per oz.
        /// </summary>
        public const decimal GramsPerOunce = 28.3495m;

        /// <summary>
        /// A variable that can be put in text and replaced.
        /// </summary>
        public const string PostCookVolume = "$POST_COOK_VOLUME$";
    }
}