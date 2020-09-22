using System;
using System.Linq;
using Autofac;
using Battleships.Application;
using Battleships.Application.Game.Components;
using Battleships.Application.Game.Components.Strategies;
using Battleships.Domain.Game;
using Battleships.Domain.Game.Events;
using Battleships.Domain.Game.Player;
using Battleships.Domain.Game.Player.AI;
using Battleships.Domain.Game.Session;
using Battleships.Domain.Game.Session.Round;
using Battleships.Domain.Ships.Services;

namespace Battleships.Host.ConsoleApp
{
  class Program
  {
    private static IContainer _container;

    private static GameComponentsFactory _componentsFactory;

    private static IGameSession _session;

    private static IGameEvents _events;

    private static GamePlayer _humanPlayer;
    private static GamePlayer _computerPlayer;

    static void Main(string[] args)
    {
      BuildContainer();
      SubscribeToEvents();

      _componentsFactory = _container.Resolve<GameComponentsFactory>();
      _session = _container.Resolve<IGameSession>();

      Console.WriteLine("Welcome in the Battleship game!");
      Console.WriteLine("");

      StartNewSession();
    }

    static void BuildContainer()
    {
      var builder = new ContainerBuilder();

      builder.RegisterType<GameComponentsFactory>();

      builder.RegisterType<GameSession>().As<IGameSession>();

      // players
      builder.RegisterType<GamePlayer>();
      builder.RegisterType<ComputerGamePlayer>();
      builder.RegisterType<GamePlayerManager>().As<IGamePlayerManager>();
      builder.RegisterType<DummyPlayerAI>().As<IPlayerAI>();

      // strategies
      builder.RegisterType<ClassicBoardStrategy>().As<IGameBoardStrategy>();
      builder.RegisterType<BattleshipWithDestroyersShipsStrategy>().As<IGameShipsStrategy>();

      // services
      builder.RegisterType<ShipPositionGenerator>().As<IShipPositionGenerator>();

      // events
      builder.RegisterType<GameEventBus>()
        .As<IGameEventBus>()
        .As<IGameEvents>()
        .SingleInstance();

      _container = builder.Build();
    }

    static void SubscribeToEvents()
    {
      _events = _container.Resolve<IGameEvents>();
      _events.GameSessionStarted += Events_GameSessionStarted;
      _events.GameSessionFinished += Events_GameSessionFinished;
      _events.FireRequested += Events_FireRequested;
      _events.RoundFinished += Events_RoundFinished;
    }

    static void StartNewSession()
    {
      Console.WriteLine("Please enter your name first:");

      var playerName = Console.ReadLine();
      _humanPlayer = _componentsFactory.CreatePlayer<GamePlayer>(playerName);
      _computerPlayer = _componentsFactory.CreatePlayer<ComputerGamePlayer>("COMP");

      Console.WriteLine("Press ENTER to start new game session.");
      Console.ReadLine();

      _session.Start(_humanPlayer, _computerPlayer);
    }

    private static void GoHumanPlayer()
    {
      Console.WriteLine("Please enter coordinates to fire");

      var input = Console.ReadLine();

      if (!InputCoordinatesHelper.IsValid(input))
      {
        Console.WriteLine("Entered input is invalid. Please try once again.");
        
        GoHumanPlayer();

        return;
      }

      var coordinates = InputCoordinatesHelper.Convert(input);

      var result = _session.Player1.Manager.TryFire(coordinates.X, coordinates.Y, out string error);

      if (!result)
      {
        Console.WriteLine($"Couldn't request the coordinates hit. Reason: {error}");
        Console.WriteLine("Please try once again");

        GoHumanPlayer();
      }
    }

    private static void PrintPlayerState(GamePlayer player)
    {
      Console.WriteLine();
      Console.WriteLine($"Player {player.Name} stats:");
      Console.WriteLine($"Healthy ships: {player.Manager.GameBoard.Ships.Count(c => !c.IsHit && !c.IsSink)}");
      Console.WriteLine($"Hit ships: {player.Manager.GameBoard.Ships.Count(c => c.IsHit && !c.IsSink)}");
      Console.WriteLine($"Destroyed ships: {player.Manager.GameBoard.Ships.Count(c => c.IsSink)}");
      Console.WriteLine();
    }

    #region Event handlers

    private static void Events_RoundFinished(GamePlayer player, GameSessionRoundResult result)
    {
      if (result.WasHit && result.WasDestroyed)
        Console.WriteLine($"{player.Name} has destroyed the ship - ship has sunk");

      if (result.WasHit && !result.WasDestroyed)
        Console.WriteLine($"{player.Name} has hit the ship but the ship is still floating");

      if (!result.WasHit && !result.WasDestroyed)
        Console.WriteLine($"{player.Name} has missed the ships");

      if (!result.ShipsStillFloating)
        Console.WriteLine($"{player.Name} has destroyed all ships of his opponent - all ships are sunk");
      else
      {
        if (_humanPlayer != player) // other player finished the round, now it's human turn but first print the state
        {
          PrintPlayerState(player);
          GoHumanPlayer();
        }
        else // human player finished the round, now it's other player turn
        {
          Console.WriteLine();
          Console.WriteLine($"Waiting for {_session.Player2.Name} shot");
          Console.WriteLine();
        }
      }
    }

    private static void Events_FireRequested(GamePlayer player, int xCord, int yCord)
    {
      Console.WriteLine($"{player.Name} has requested the fire to coordinates: {InputCoordinatesHelper.ConvertBack(xCord, yCord)}");
    }

    private static void Events_GameSessionStarted(IGameSession session)
    {
      Console.WriteLine("New game session has been started.");
      Console.WriteLine($"Player 1: {session.Player1.Name}");
      Console.WriteLine($"Player 2: {session.Player2.Name}");

      GoHumanPlayer();
    }

    private static void Events_GameSessionFinished(IGameSession session, GamePlayer winner)
    {
      Console.WriteLine("Game session has been finished.");
      Console.WriteLine($"The winner is: {winner.Name}");
    }

    #endregion

  }
}
