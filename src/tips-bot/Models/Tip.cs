namespace tips_bot.Models
{
  public class Tip
  {
    public Tip(int id, string message, int chatId)
    {
      Id = id;
      Message = message;
      ChatId = chatId;
    }

    public int Id {get;set;}
    public string Message { get; set; }
    public int ChatId { get; set; }
  }
}