using System;
using Battleships.Domain.Board;

namespace Battleships.Domain.Ships.Services
{
  public class ShipPositionGenerator : IShipPositionGenerator
  {
    public ShipOnBoardOrientation GetOrientation()
    {
      var values = Enum.GetValues(typeof(ShipOnBoardOrientation));
      var random = new Random();

      return (ShipOnBoardOrientation)values.GetValue(random.Next(values.Length));
    }

    public (int, int) GetStartCoordinates(int maxXCord, int maxYCord)
    {
      var random = new Random();

      return (random.Next(1, maxXCord), random.Next(1, maxYCord));
    }
  }
}
