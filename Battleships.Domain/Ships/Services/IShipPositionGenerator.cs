using Battleships.Domain.Board;

namespace Battleships.Domain.Ships.Services
{
  public interface IShipPositionGenerator
  {
    ShipOnBoardOrientation GetOrientation();

    (int, int) GetStartCoordinates(int maxXCord, int maxYCord);
  }
}
