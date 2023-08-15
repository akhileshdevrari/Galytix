namespace CountryGwp.Models
{
    /// <summary>
    /// Response model for GwpAvgController
    /// </summary>
    public class GwpAvgResponse
    {
        /// <summary>
        /// Line of Business
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        /// Average GWP in given timeframe
        /// </summary>
        public double AverageGwp { get; set; }
    }
}
