namespace Domain.Modals.ViolationMoney
{
    public class ViolationMoneyToUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Fine { get; set; }
    }
}
