namespace AddService.Models
{
    public class History
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public long Inputone { get; set; }
        public long Inputtwo { get; set; }
        public long Output { get; set; }
        public enum Operation
        {
            Addition,
            Subtraction,
            Multiplication
        };
        private Operation operation { get; set; }
        public override string ToString()
        {
            return 
                "-Id: "+Id+
                ", Timestamp: " + Timestamp+
                ", Inputone: " + Inputone+
                ", Inputtwo: " + Inputtwo +
                ", Output: " + Output +
                ", Operation: "+operation.ToString();
                ;
        }
    }
}
