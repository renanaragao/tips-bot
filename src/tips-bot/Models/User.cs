namespace tips_bot.Models
{
    public class User
    {
        private User()
        {
            
        }
        public User(int id, string username, string firstName, string lastName)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}