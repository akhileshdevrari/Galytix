namespace CountryGwp.Models
{
    /// <summary>
    /// GWP schema
    /// </summary>
    public class GwpItem
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Year of record
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Country of record
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Line of Business
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        /// GWP amount
        /// </summary>
        public double Amount { get; set; }
    }
}
