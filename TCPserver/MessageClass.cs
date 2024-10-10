namespace TCPserver
{
    public class Message
    {
        public string Command { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public Message()
        {
        }

        public override string ToString()
        {
            return $"{nameof(Command)}={Command}, {nameof(Value1)}={Value1}, {nameof(Value2)}={Value2}";
        }
    }
}
