using System.Threading;

namespace Battleships.Domain.Game.Player.AI
{
  public class DummyPlayerAI : IPlayerAI
  {
    public void PerformNextAction(IGamePlayerManager manager)
    {
      Thread.Sleep(2500); // simulate thinking delay

      // simply skip the round
      manager.SkipRound();
    }
  }
}
