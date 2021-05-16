namespace GuestApp.Model
{
    public class EventGuest
    {
        public int EventId { get; set; }
        public int GuestId { get; set; }
        public Event Event { get; set; }
        public Guest Guest { get; set; }
    }
}