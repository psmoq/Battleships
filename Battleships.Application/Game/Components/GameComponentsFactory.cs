using System.Collections.Generic;
using Autofac;
using Battleships.Domain.Board;
using Battleships.Domain.Game;
using Battleships.Domain.Game.Player;
using Battleships.Domain.Ships;

namespace Battleships.Application.Game.Components
{
  public class GameComponentsFactory
  {
    private readonly ILifetimeScope _container;

    private readonly IGameBoardStrategy _boardStrategy;

    private readonly IGameShipsStrategy _shipsStrategy;

    public GameComponentsFactory(ILifetimeScope container, IGameBoardStrategy boardStrategy, IGameShipsStrategy shipsStrategy)
    {
      _container = container;
      _boardStrategy = boardStrategy;
      _shipsStrategy = shipsStrategy;
    }

    public GamePlayer CreatePlayer<TPlayer>(string name) where TPlayer : GamePlayer
    {
      var player = _container.Resolve<TPlayer>();

      player.Init(name, CreateBoard(), CreateShips());

      return player;
    }

    public GameBoard CreateBoard()
    {
      return _boardStrategy.CreateBoard();
    }

    public IEnumerable<IShip> CreateShips()
    {
      return _shipsStrategy.CreateShips();
    }
  }
}
