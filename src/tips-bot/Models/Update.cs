namespace tips_bot.Models
{
    public class Update
    {
        private Update()
        {

        }
        public Update(Message message)
        {
            Message = message;
        }

        public Message Message { get; private set; }
    }
}