using Battleships.Domain.Board;

namespace Battleships.Domain.Game
{
  public interface IGameBoardStrategy
  {
    GameBoard CreateBoard();
  }
}
