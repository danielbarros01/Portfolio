namespace Portfolio.Entities.Interfaces
{
    public interface IWithTechnologyId
    {
        public int TechnologyId { get; set; }
        public int AssociationId { get; set; }
    }
}
