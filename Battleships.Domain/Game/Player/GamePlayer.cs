using Battleships.Domain.Board;
using Battleships.Domain.Ships;
using System.Collections.Generic;

namespace Battleships.Domain.Game.Player
{
  public class GamePlayer
  {
    public string Name { get; private set; }

    public IGamePlayerManager Manager { get; }

    public GamePlayer(IGamePlayerManager manager)
    {
      Manager = manager;
    }

    public virtual void Init(string name, GameBoard board, IEnumerable<IShip> ships)
    {
      Name = name;

      Manager.Init(board, this);
    }
  }
}
