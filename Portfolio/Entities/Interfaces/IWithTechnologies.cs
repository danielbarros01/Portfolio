using Microsoft.AspNetCore.Mvc;
using Portfolio.Helpers;

namespace Portfolio.Entities.Interfaces
{
    public interface IWithTechnologies
    {
        public List<int> TechnologyIds { get; set; }
    }
}
