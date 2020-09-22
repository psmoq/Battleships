namespace Battleships.Domain.Ships.Field
{
  public class ShipField
  {
    public int XCord { get; }

    public int YCord { get; }

    public bool IsDestroyed { get; private set; }

    public ShipField(int xCord, int yCord)
    {
      XCord = xCord;
      YCord = yCord;
    }

    public void Destroy()
    {
      IsDestroyed = true;
    }
  }
}
