using Ekas.Monitoring.Components.Pages;
using Ekas.Monitoring.Repository;
using Ekas.Monitoring.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace Ekas.Monitoring.Services
{
    public class ProblemService
    {
        private readonly IncidentRepository incidentRepository;

        public ProblemService(IncidentRepository incidentRepository)
        {
            this.incidentRepository = incidentRepository;
        }

        public Task<List<Problem>> GetCurrentProblems(DateTime startDate, DateTime endDate)
        {
            return incidentRepository.GetIncidentCountByNameAndStatus(startDate, endDate);
        }

        public Task<List<ProblemChartInfo>> GetProblemInfoForPeriod(List<DateTime> allDates, DateTime start, DateTime end)
        {
            return incidentRepository.GetIncidentsForProblemFromDatePeriod(allDates, start, end);
        }
    }
}
