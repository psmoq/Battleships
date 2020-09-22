using Battleships.Domain.Board;

namespace Battleships.Domain.Game.Player
{
  public interface IGamePlayerManager
  {
    GameBoard GameBoard { get; }

    void Init(GameBoard gameBoard, GamePlayer gamePlayer);

    void SkipRound();

    bool TryFire(int xCord, int yCord, out string error);

    bool VerifyOpponentFire(GamePlayer opponent, int xCord, int yCord);
  }
}
