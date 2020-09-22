using Battleships.Domain.Game.Player;
using Battleships.Domain.Game.Session;
using System;
using Battleships.Domain.Game.Session.Round;

namespace Battleships.Domain.Game.Events
{
  public class GameEventBus : IGameEventBus, IGameEvents
  {
    public event Action<GamePlayer, GameSessionRoundResult> RoundFinished;

    public event Action<GamePlayer, int, int> FireRequested;

    public event Action<IGameSession> GameSessionStarted;

    public event Action<IGameSession, GamePlayer> GameSessionFinished;

    public void OnRoundSkipped(GamePlayer opponent)
    {
      RoundFinished?.Invoke(opponent, GameSessionRoundResult.Skipped());
    }

    public void OnShipSunk(GamePlayer opponent)
    {
      RoundFinished?.Invoke(opponent, GameSessionRoundResult.Sunk());
    }

    public void OnAllShipsSunk(GamePlayer opponent)
    {
      RoundFinished?.Invoke(opponent, GameSessionRoundResult.AllSunk());
    }

    public void OnShipHit(GamePlayer player)
    {
      RoundFinished?.Invoke(player, GameSessionRoundResult.Hit());
    }

    public void OnShipMissed(GamePlayer opponent)
    {
      RoundFinished?.Invoke(opponent, GameSessionRoundResult.Missed());
    }

    public void OnFireRequested(GamePlayer opponent, int xCord, int yCord)
    {
      FireRequested?.Invoke(opponent, xCord, yCord);
    }

    public void OnGameSessionStarted(IGameSession gameSession)
    {
      GameSessionStarted?.Invoke(gameSession);
    }

    public void OnGameSessionFinished(IGameSession session, GamePlayer winner)
    {
      GameSessionFinished?.Invoke(session, winner);
    }
  }
}
