namespace tips_bot.Models
{
    public class Message
    {

        private Message()
        {
            
        }
        public Message(int id, User from, int date, string text)
        {
            Id = id;
            From = from;
            Date = date;
            Text = text;
        }

        public int Id { get; private set; }
        public User From { get; private set; }
        public int Date { get; private set; }
        public string Text { get; private set; }

    }
}