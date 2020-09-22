using Battleships.Domain.Game.Events;
using Battleships.Domain.Game.Player;
using Battleships.Domain.Game.Session.Round;

namespace Battleships.Domain.Game.Session
{
  public class GameSession : IGameSession
  {
    private readonly IGameEventBus _eventBus;

    public GamePlayer Player1 { get; private set; }

    public GamePlayer Player2 { get; private set; }

    public GameSession(IGameEventBus eventBus, IGameEvents events)
    {
      _eventBus = eventBus;

      events.RoundFinished += Events_RoundFinished;
    }

    private void Events_RoundFinished(GamePlayer opponent, GameSessionRoundResult result)
    {
      if (result.ShipsStillFloating)
        return; // game session is only interested in information when to stop

      var winner = opponent;

      _eventBus.OnGameSessionFinished(this, winner);
    }

    public void Start(GamePlayer player1, GamePlayer player2)
    {
      Player1 = player1;
      Player2 = player2;

      _eventBus.OnGameSessionStarted(this);
    }
  }
}
