namespace CountryGwp.Models
{
    /// <summary>
    /// Input Model for GwpAvgController
    /// </summary>
    public class GwpAvgRequest
    {
        /// <summary>
        /// Country filter
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Businesses filter
        /// </summary>
        public List<string> Businesses { get; set; } = new List<string>();
    }
}
