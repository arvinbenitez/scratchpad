using System.Collections;
using System.Collections.Generic;
using AguaDeMaria.Model.Report;

namespace AguaDeMaria.Service
{
    public interface IReportService
    {
        IList<DailySummary> GetDailySummary();
    }
}
