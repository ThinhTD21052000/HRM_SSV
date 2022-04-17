namespace Domain.Modals.WageType
{
    public class Wage_TypeToAdd
    {
        public string Title { get; set; } = string.Empty;
        public int BaseSalary { get; set; }
        public string Description { get; set; } = string.Empty;
        public byte[]? Attachment { get; set; }
        public int PositionId { get; set; }
    }
}
