namespace Battleships.Domain.Game.Session.Round
{
  public class GameSessionRoundResult
  {
    public bool WasHit { get; private set; }

    public bool WasDestroyed { get; private set; }

    public bool ShipsStillFloating { get; private set; }

    public static GameSessionRoundResult Skipped()
    {
      return new GameSessionRoundResult
      {
        WasHit = false,
        WasDestroyed = false,
        ShipsStillFloating = true
      };
    }

    public static GameSessionRoundResult Hit()
    {
      return new GameSessionRoundResult
      {
        WasHit = true,
        WasDestroyed = false,
        ShipsStillFloating = true
      };
    }

    public static GameSessionRoundResult Sunk()
    {
      return new GameSessionRoundResult
      {
        WasHit = true,
        WasDestroyed = true,
        ShipsStillFloating = true
      };
    }

    public static GameSessionRoundResult AllSunk()
    {
      return new GameSessionRoundResult
      {
        WasHit = true,
        WasDestroyed = true,
        ShipsStillFloating = false
      };
    }

    public static GameSessionRoundResult Missed()
    {
      return new GameSessionRoundResult
      {
        WasHit = false,
        WasDestroyed = false,
        ShipsStillFloating = true
      };
    }
  }
}
