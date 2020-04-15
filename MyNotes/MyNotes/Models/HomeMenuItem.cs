namespace MyNotes.Models
{
    public enum MenuItemType
    {
        Notes,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
