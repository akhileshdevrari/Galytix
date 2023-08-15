using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CountryGwp.Models
{
    /// <summary>
    /// Database context for GwpItems
    /// </summary>
    public class GwpContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">DB context options</param>
        public GwpContext(DbContextOptions<GwpContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// GwpItems in the database
        /// </summary>
        public DbSet<GwpItem> GwpItems { get; set; } = null!;
    }
}