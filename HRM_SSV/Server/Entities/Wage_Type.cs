namespace Server.Entities
{
    public class Wage_Type
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int BaseSalary { get; set; }
        public string Description { get; set; } = string.Empty;
        public byte[]? Attachment { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; } = new();
        public virtual List<Wage> Wages { get; set; } = new();
    }
}
