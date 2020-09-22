using Battleships.Domain.Game.Player;
using Battleships.Domain.Game.Session;

namespace Battleships.Domain.Game.Events
{
  public interface IGameEventBus
  {
    void OnRoundSkipped(GamePlayer opponent);

    void OnShipSunk(GamePlayer opponent);

    void OnAllShipsSunk(GamePlayer opponent);

    void OnShipHit(GamePlayer opponent);

    void OnShipMissed(GamePlayer opponent);

    void OnFireRequested(GamePlayer opponent, int xCord, int yCord);

    void OnGameSessionStarted(IGameSession session);

    void OnGameSessionFinished(IGameSession session, GamePlayer winner);
  }
}
