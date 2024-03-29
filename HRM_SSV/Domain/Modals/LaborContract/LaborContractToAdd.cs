﻿namespace Domain.Modals.LaborContract
{
    public class LaborContractToAdd
    {
        public int Number { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string CDD { get; set; } = string.Empty;
        public bool IsSigned { get; set; }
        public byte[]? Attachment { get; set; }
        public Guid UserId { get; set; }
    }
}
