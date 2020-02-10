using Xunit;
using tips_bot.Interfaces;
using System.Threading.Tasks;
using tips_bot.Models;

public class TipsRepositoryTest : IClassFixture<MongoIntegrationTest.MongoIntegrationTest>
{
  private readonly ITipsRepository repository;
private Tip tip;
  public TipsRepositoryTest(MongoIntegrationTest.MongoIntegrationTest integrationTest)
  {

  }

  [Fact]
  public async Task Should_Insert_Tip()
  {
    try
    {
      tip = new Tip
      (
        message:"uma nota muito bonita",
        chatId:123,
        id:1
      );
}
    catch (System.Exception)
    {

        throw;
    }
    finally
    {
      //sempre executa
    }
    await Task.Delay(1);
  }
}