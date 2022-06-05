namespace Server.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Logo { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string ListPhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        //Date of incorporation
        public DateTime DOI { get; set; }
    }
}
