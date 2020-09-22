namespace Battleships.Domain.Game.Player.AI
{
  public interface IPlayerAI
  {
    void PerformNextAction(IGamePlayerManager manager);
  }
}
