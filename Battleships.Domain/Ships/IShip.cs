using Battleships.Domain.Board.Field;
using Battleships.Domain.Ships.Field;
using System.Collections.Generic;

namespace Battleships.Domain.Ships
{
  public interface IShip : IShipPosition
  {
    int FieldCount { get; }

    bool IsHit { get; }

    bool IsSink { get; }

    bool TryHit(int xCord, int yCord);
  }

  public interface IShipPosition
  {
    ShipField[] Fields { get; }

    void SetBoardPosition(IEnumerable<GameBoardField> boardFields);
  }
}
