namespace Domain.Modals.LaborContract
{
    public class LaborContractToGet
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string CDD { get; set; } = string.Empty;
        public bool IsSigned { get; set; }
        public byte[]? Attachment { get; set; }
        public byte[]? AttachmentPDF { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
