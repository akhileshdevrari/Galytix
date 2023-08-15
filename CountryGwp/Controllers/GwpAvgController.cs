using CountryGwp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CountryGwp.Controllers
{
    /// <summary>
    /// Controller to process GwpItems and calculate average GWP
    /// </summary>
    [Route("server/api/gwp/avg")]
    [ApiController]
    public class GwpAvgController : ControllerBase
    {
        private readonly GwpContext _context;
        private readonly IMemoryCache _cache;
        private const int StartYear = 2008;
        private const int EndYear = 2015;
        private const int CacheExpirationInHours = 1;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Database context object</param>
        /// <param name="cache">In memory cache</param>
        public GwpAvgController(GwpContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        /// <summary>
        /// Process the average GWP with given filters
        /// </summary>
        /// <param name="request">Request body of the POST method</param>
        /// <returns>Response of the POST call</returns>
        [HttpPost]
        public async Task<ActionResult<GwpAvgResponse>> PostAvg(GwpAvgRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<GwpAvgResponse> responses = new List<GwpAvgResponse>();
            foreach (string business in request.Businesses)
            {
                GwpAvgResponse response = await GetGwpAvg(request.Country, business);
                responses.Add(response);
            }

            return Ok(responses);
        }

        /// <summary>
        /// Get average GWP for a given business in the given country
        /// </summary>
        /// <param name="country">Country of business</param>
        /// <param name="business">Line of business</param>
        /// <returns>Average Gwp for the given LOB in the given country</returns>
        private async Task<GwpAvgResponse> GetGwpAvg(string country, string business)
        {
            // There can be corner cases where the string country_business might not uniquely define the set {country, business}
            // This can be fixed if we ensure that '_' is not allowed in the country or business names. Or we can use some other special character
            string cacheKey = $"{country}_{business}";
            if (!_cache.TryGetValue(cacheKey, out GwpAvgResponse response))
            {
                var query = _context.GwpItems
                .Where(item => item.Country == country &&
                               item.Business == business &&
                               item.Year >= StartYear &&
                               item.Year <= EndYear);

                double averageAmount = 0.0;
                bool itemsExist = await query.AnyAsync();
                if (itemsExist)
                {
                    averageAmount = await query.AverageAsync(item => item.Amount);
                }

                response = new GwpAvgResponse()
                {
                    Business = business,
                    AverageGwp = averageAmount
                };

                _cache.Set(cacheKey, response, TimeSpan.FromHours(CacheExpirationInHours));
            }

            return response;
        }
    }
}
