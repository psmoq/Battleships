using System.Collections.Generic;
using System.Linq;
using Battleships.Domain.Board;
using Battleships.Domain.Game.Events;
using Battleships.Domain.Game.Player.AI;
using Battleships.Domain.Ships;
using Battleships.Domain.Ships.Services;

namespace Battleships.Domain.Game.Player
{
  public class ComputerGamePlayer : GamePlayer
  {
    private readonly IShipPositionGenerator _shipPositionGenerator;

    private bool _nextMoveAvailable = true;

    public ComputerGamePlayer(IGamePlayerManager manager, IShipPositionGenerator shipPositionGenerator, IGameEvents gameEvents, IPlayerAI playerAI) 
      : base(manager)
    {
      _shipPositionGenerator = shipPositionGenerator;

      gameEvents.FireRequested += (player, xCord, yCord) =>
      {
        if (player.Equals(this))
          return;

        Manager.VerifyOpponentFire(player, xCord, yCord);
      };

      gameEvents.GameSessionStarted += (session) =>
      {
        _nextMoveAvailable = true;
      };

      gameEvents.GameSessionFinished += (session, winner) =>
      {
        _nextMoveAvailable = false;
      };

      gameEvents.RoundFinished += (player, result) =>
      {
        if (_nextMoveAvailable)
          playerAI.PerformNextAction(Manager);
      };
    }

    public override void Init(string name, GameBoard board, IEnumerable<IShip> ships)
    {
      ships = ships.ToArray(); // materialize once just in case

      base.Init(name, board, ships);

      foreach (var ship in ships)
      {
        bool shipPlacedSuccessfully;

        do
        {
          var shipOrientation = _shipPositionGenerator.GetOrientation();
          var shipCoordinates = _shipPositionGenerator.GetStartCoordinates(board.LengthX, board.LengthY);

          shipPlacedSuccessfully = board.TryPlaceShip(ship, shipCoordinates.Item1, shipCoordinates.Item2, shipOrientation);

        } while (!shipPlacedSuccessfully);
      }

      Manager.Init(board, this);
    }
  }
}
