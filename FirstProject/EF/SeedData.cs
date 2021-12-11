using System.Collections.Generic;
using System.Linq;
using FirstProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;

namespace FirstProject.EF
{
    public class SeedData
    {
        private readonly CalculationContext _context;
        private readonly ILogger<SeedData> _logger;

        private static readonly Dictionary<int, long> Data = new Dictionary<int, long>
        {
            {1, 1}, {2, 3}, {3, 6}, {4, 10}, {5, 15}, {6, 21}, {7, 28}, {8, 36}, {9, 45}, {10, 55}
        };

        public SeedData(CalculationContext context, ILogger<SeedData> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void SeedDatabase()
        {
            _context.Database.Migrate();
            if (!_context.Calculations.Any())
            {
                _logger.LogInformation("Preparing to seed database");

                _context.Calculations!.AddRange(Data.Select(kvp=> 
                    new Calculation{Count = kvp.Key, Result = kvp.Value}));

                _context.SaveChanges();

                _logger.LogInformation("Database seeded");
            }
            else
            {
                _logger.LogInformation("Database not seeded");
            }
        }
    }
}