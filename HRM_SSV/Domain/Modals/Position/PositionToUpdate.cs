namespace Domain.Modals.Position
{
    public class PositionToUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PositionId { get; set; }
    }
}
