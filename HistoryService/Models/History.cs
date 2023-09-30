namespace AddService.Models
{
    public class History
    {
        public int Id { get; set; }
        
        public DateTime timestamp { get; set; }
        public long inputone { get; set; }
        public long inputtwo { get; set; }
        public long output { get; set; }
        public string operation { get; set; }
    }
}
