using System.Linq;
using Battleships.Domain.Board;
using Battleships.Domain.Game.Events;

namespace Battleships.Domain.Game.Player
{
  public class GamePlayerManager : IGamePlayerManager
  {
    private readonly IGameEventBus _gameEventBus;

    private GamePlayer _gamePlayer;

    public GameBoard GameBoard { get; private set; }

    public GamePlayerManager(IGameEventBus gameEventBus)
    {
      _gameEventBus = gameEventBus;
    }

    public void Init(GameBoard gameBoard, GamePlayer gamePlayer)
    {
      GameBoard = gameBoard;
      _gamePlayer = gamePlayer;
    }

    public void SkipRound()
    {
      _gameEventBus.OnRoundSkipped(_gamePlayer);
    }

    public bool TryFire(int xCoord, int yCoord, out string error)
    {
      error = null;

      if (!CoordinatesValid(xCoord, yCoord))
      {
        error = "Provided coordinates are out of range";

        return false;
      }

      _gameEventBus.OnFireRequested(_gamePlayer, xCoord, yCoord);

      return true;
    }

    public bool VerifyOpponentFire(GamePlayer opponent, int xCoord, int yCoord)
    {
      foreach (var ship in GameBoard.Ships)
      {
        var wasHit = ship.TryHit(xCoord, yCoord);

        if (wasHit)
        {
          if (ship.IsSink)
          {
            if (AllShipsSunk())
              _gameEventBus.OnAllShipsSunk(opponent);
            else _gameEventBus.OnShipSunk(opponent);

            return true;
          }

          _gameEventBus.OnShipHit(opponent);

          return true;
        }
      }

      _gameEventBus.OnShipMissed(opponent);

      return false;
    }

    private bool AllShipsSunk() => GameBoard.Ships.All(ship => ship.IsSink);

    private bool CoordinatesValid(int x, int y) => GameBoard.LengthX >= x && GameBoard.LengthY >= y;
  }
}
