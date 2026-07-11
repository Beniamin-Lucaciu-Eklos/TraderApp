using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;

namespace TraderApp.FinancialApi.Services
{
    public class MockMajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndexAsync(MajorIndexType indexType)
        {
            var faker = new Faker<MajorIndex>()
                .RuleFor(m => m.Price, f => f.Random.Double(1000, 40000))
                .RuleFor(m => m.Changes, f => f.Random.Double(-500, 500))
                .RuleFor(m => m.Type, f => indexType);

            await Task.Delay(1500); // Simulate async operation

            return faker.Generate(1).Single();
        }
    }
}
