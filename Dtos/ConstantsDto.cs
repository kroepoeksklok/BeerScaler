using BeerScaler.Data;

namespace BeerScaler.Dtos {
    /// <summary>
    /// This Dto can be used to expose constants to clients.
    /// </summary>
    public sealed class ConstantsDto {
        /// <summary>
        /// Indicates a default mash thickness.
        /// </summary>
        public decimal DefaultLitersPerKiloMalt => Constants.DefaultLitersPerKiloMalt;
        
        /// <summary>
        /// Indicates the volume of 1 kilogram of malt, in liters.
        /// </summary>
        public decimal VolumeOf1KilogramOfMalt => Constants.VolumeOf1KilogramOfMalt;

        /// <summary>
        /// How many liters a single kilogram of malt absorbs.
        /// </summary>
        public decimal AmountOfWater1KilogramOfMaltAbsorbs => Constants.AmountOfWater1KilogramOfMaltAbsorbs;

        /// <summary>
        /// How many liters are evaporated per hour during the boil phase.
        /// </summary>
        public decimal LitersEvaporatedPerHour => Constants.LitersEvaporatedPerHour;

        /// <summary>
        /// Grams per oz.
        /// </summary>
        public decimal GramsPerOunce => Constants.GramsPerOunce;

        /// <summary>
        /// A variable that can be put in text and replaced.
        /// </summary>
        public string PostCookVolume => Constants.PostCookVolume;
    }
}