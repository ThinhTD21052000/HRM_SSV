namespace Domain.Modals.Menu
{
    public class MenuToUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Position { get; set; }
    }
}
