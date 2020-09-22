using System;
using Battleships.Domain.Game.Player;
using Battleships.Domain.Game.Session;
using Battleships.Domain.Game.Session.Round;

namespace Battleships.Domain.Game.Events
{
  public interface IGameEvents
  {
    event Action<GamePlayer, GameSessionRoundResult> RoundFinished;

    event Action<GamePlayer, int, int> FireRequested;

    event Action<IGameSession> GameSessionStarted;

    event Action<IGameSession, GamePlayer> GameSessionFinished;
  }
}
