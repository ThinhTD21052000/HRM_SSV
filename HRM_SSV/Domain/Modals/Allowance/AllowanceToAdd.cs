namespace Domain.Modals.Allowance
{
    public class AllowanceToAdd
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalMoney { get; set; }
    }
}
