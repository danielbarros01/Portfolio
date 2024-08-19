using Portfolio.Entities.Interfaces;

namespace Portfolio.Entities
{
    public class Category : IId
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
