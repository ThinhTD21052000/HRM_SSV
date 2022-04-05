namespace Server.Entities
{
    public class LaborContract
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }

        //contract drafting date
        public string CDD { get; set; }
        public bool IsSigned { get; set; }
        public byte[] Attachment { get; set; }
    }
}
