namespace BeerScaler.Dtos {
    /// <summary>
    /// A mash step is a single step used during the mashing phase.
    /// </summary>
    public sealed class MashStep {
        public MashStep(int durationInMinutes, int degreesCelsius) {
            DurationInMinutes = durationInMinutes;
            DegreesCelsius = degreesCelsius;
        }
        
        /// <summary>
        /// How long this mash step is supposed to take.
        /// </summary>
        public int DurationInMinutes { get; }

        /// <summary>
        /// The temperature of this mash step
        /// </summary>
        public int DegreesCelsius { get; }
    }
}