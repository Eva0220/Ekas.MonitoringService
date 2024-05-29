using Ekas.Common;
using Ekas.Monitoring.Models;
using Ekas.Monitoring.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ekas.Monitoring.Repository
{
    public class IncidentRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
  
        public IncidentRepository(ApplicationDbContext applicationContext)
        {
            this.applicationDbContext = applicationContext;
        }

        public async Task AddIncident(Incident incident)
        {
            await applicationDbContext.AddAsync(incident);
            await applicationDbContext.SaveChangesAsync();
        }
        public Incident GetIncidentById(int incidentId)
        {

            return applicationDbContext.Incidents.FirstOrDefault(o => o.Id == incidentId);

        }

        public List<Incident> GetOpenIncidents()
        {
            return applicationDbContext.Incidents.Where(i => i.Status != Status.Close).ToList();
        }

        public List<Incident> GetCloseIncidents()
        {
            return applicationDbContext.Incidents.Where(i => i.Status == Status.Close).ToList();
        }

        public void UpdateIncident(Incident incident)
        {
            applicationDbContext.Incidents.Update(incident);
            applicationDbContext.SaveChanges();
        }

        public async Task<List<Problem>> GetIncidentCountByNameAndStatus(DateTime date1, DateTime date2)
        {
            return await applicationDbContext.Incidents
                .Where(i => i.CreatedDate >= date1 && i.CreatedDate <= date2)
               .GroupBy(i => i.Name)
                .Select(g => new Problem
                {
                    Name = g.Key,
                    OpenCount = g.Count(i => i.Status == Status.Open),
                    InworkCount = g.Count(i => i.Status == Status.Inwork),
                    ClosedCount = g.Count(i => i.Status == Status.Close)
                })
                .ToListAsync();
        }

        public Task<List<ProblemChartInfo>> GetIncidentsForProblemFromDatePeriod(List<DateTime> allDates, DateTime date1, DateTime date2)
        {
            return applicationDbContext.Incidents
                .Where(i => i.CreatedDate >= date1 && i.CreatedDate <= date2)
                .GroupBy(i => new { i.Name, i.CreatedDate.Date }) 
                .Select(g => new ProblemChartInfo
                {
                    Name = g.Key.Name,
                    Date = g.Key.Date,
                    StringDate = allDates.Count <= 7 ? g.Key.Date.ToString("d") : g.Key.Date.ToString("dd"),
                    Count = g.Count()
                })
                .OrderBy(p => p.Date)
                .ThenBy(p => p.Name)
                .ToListAsync();
        }
    }
}
