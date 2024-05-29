using Ekas.Monitoring.Repository;
using Ekas.Monitoring.ViewModels;
using Ekas.Monitoring.Models;

namespace Ekas.Monitoring.Services
{
    public class IncidentService
    {
        private readonly IncidentRepository incidentRepository;

        public IncidentService(IncidentRepository incidentRepository)
        {
            this.incidentRepository = incidentRepository;
        }

        public Incident GetIncident(int incidentId)
        {
            return incidentRepository.GetIncidentById(incidentId);
        }

        public void ChangeStatus(Incident incident)
        {
            if (incident.Status == Status.Open)
            {
                incident.Status = Status.Inwork;
            }
            else incident.Status = Status.Close;
            incidentRepository.UpdateIncident(incident);
        }
    }
}
