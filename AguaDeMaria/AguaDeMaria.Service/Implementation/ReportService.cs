using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model.Report;

namespace AguaDeMaria.Service.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IRepository<DailySummary> dailySummaryRepository;

        public ReportService(IRepository<DailySummary> dailySummaryRepository)
        {
            this.dailySummaryRepository = dailySummaryRepository;
        }

        public IList<DailySummary> GetDailySummary()
        {
            var result = dailySummaryRepository.Get(x => x.Date.HasValue, z => z.OrderBy(t => t.Date));
            return result.ToList();
        }
    }
}