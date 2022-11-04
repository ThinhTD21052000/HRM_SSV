namespace Server.Entities
{
    public class LaborContract
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        //contract drafting date
        public string CDD { get; set; } = string.Empty;
        public bool IsSigned { get; set; }
        public byte[]? Attachment { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = new();
    }
}
