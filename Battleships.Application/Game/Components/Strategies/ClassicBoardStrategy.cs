using Battleships.Domain.Board;
using Battleships.Domain.Game;

namespace Battleships.Application.Game.Components.Strategies
{
  public class ClassicBoardStrategy : IGameBoardStrategy
  {
    public GameBoard CreateBoard()
    {
      return new GameBoard(10, 10); // classic board size
    }
  }
}
