using System.Collections.Generic;
using Battleships.Domain.Game;
using Battleships.Domain.Ships;

namespace Battleships.Application.Game.Components.Strategies
{
  public class BattleshipWithDestroyersShipsStrategy : IGameShipsStrategy
  {
    public IEnumerable<IShip> CreateShips()
    {
      return new IShip[]
      {
        new Battleship(),
        new Destroyer(),
        new Destroyer()
      };
    }
  }
}
