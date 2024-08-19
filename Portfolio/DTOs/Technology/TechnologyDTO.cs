using Portfolio.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.DTOs.Technology
{
    public class TechnologyDTO : IId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
