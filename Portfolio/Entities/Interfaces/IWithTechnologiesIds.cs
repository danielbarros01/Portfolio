using Microsoft.AspNetCore.Mvc;
using Portfolio.Helpers;

namespace Portfolio.Entities.Interfaces
{
    public interface IWithTechnologiesIds
    {
        public List<int> TechnologyIds { get; set; }
    }
}
