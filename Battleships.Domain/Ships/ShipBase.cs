using Battleships.Domain.Board.Field;
using Battleships.Domain.Ships.Field;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Domain.Ships
{
  public abstract class ShipBase : IShip
  {
    private bool _isReadyForBattle = false;

    public abstract int FieldCount { get; }

    public bool IsHit => !_isReadyForBattle ?
      throw new InvalidOperationException("Ship is not ready for battle yet.") : Fields.Any(f => f.IsDestroyed);

    public bool IsSink => !_isReadyForBattle ?
      throw new InvalidOperationException("Ship is not ready for battle yet.") : Fields.All(f => f.IsDestroyed);

    public ShipField[] Fields { get; private set; }

    protected ShipBase()
    {
      Fields = new ShipField[0];
    }

    public bool TryHit(int xCord, int yCord)
    {
      var field = Fields.FirstOrDefault(f => f.XCord == xCord && f.YCord == yCord);

      if (field == null)
        return false;

      field.Destroy();

      return true;
    }

    public void SetBoardPosition(IEnumerable<GameBoardField> boardFields)
    {
      var fields = boardFields.ToArray(); // materialize it once

      if (FieldCount != fields.Length)
        throw new ArgumentException("Provided fields count is incorrect.");

      Fields = fields.Select(field => new ShipField(field.XCord, field.YCord)).ToArray();

      _isReadyForBattle = true;
    }
  }
}
