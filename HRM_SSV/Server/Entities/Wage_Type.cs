namespace Server.Entities
{
    public class Wage_Type
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal MoneyLevel { get; set; }
        public string Description { get; set; }
        public byte[] Attachment { get; set; }
    }
}
