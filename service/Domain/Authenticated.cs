namespace service.Domain
{
    public class Authenticated
    {
        public User user { get; set; }
        public string token { get; set; }
    }
}