using Battleships.Domain.Game.Player;

namespace Battleships.Domain.Game.Session
{
  public interface IGameSession
  {
    GamePlayer Player1 { get; }

    GamePlayer Player2 { get; }

    void Start(GamePlayer player1, GamePlayer player2);
  }
}
