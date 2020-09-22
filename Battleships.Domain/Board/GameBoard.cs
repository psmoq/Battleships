using Battleships.Domain.Board.Field;
using Battleships.Domain.Ships;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Domain.Board
{
  public class GameBoard
  {
    public int LengthX { get; }

    public int LengthY { get; }

    public GameBoard(int lengthX, int lengthY)
    {
      LengthX = lengthX;
      LengthY = lengthY;
    }

    public IList<IShip> Ships { get; } = new List<IShip>();

    public bool TryPlaceShip(IShip ship, int startXCord, int startYCord, ShipOnBoardOrientation orientation)
    {
      var boardFields = new List<GameBoardField>();

      switch (orientation)
      {
        case ShipOnBoardOrientation.Horizontal:

          for (var i = 0; i < ship.FieldCount; i++)
          {
            boardFields.Add(new GameBoardField
            {
              XCord = startXCord + i,
              YCord = startYCord
            });
          }

          break;

        case ShipOnBoardOrientation.Vertical:

          for (var i = 0; i < ship.FieldCount; i++)
          {
            boardFields.Add(new GameBoardField
            {
              XCord = startXCord,
              YCord = startYCord + i
            });
          }

          break;
      }

      if (Ships.Any(s => s.Fields.Any(f =>
          boardFields.Exists(c => c.XCord == f.XCord && c.YCord == f.YCord))))
      {
        return false;
      }

      ship.SetBoardPosition(boardFields);

      Ships.Add(ship);

      return true;
    }
  }
}
