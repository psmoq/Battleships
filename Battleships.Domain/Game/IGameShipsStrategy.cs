using Battleships.Domain.Ships;
using System.Collections.Generic;

namespace Battleships.Domain.Game
{
  public interface IGameShipsStrategy
  {
    IEnumerable<IShip> CreateShips();
  }
}
