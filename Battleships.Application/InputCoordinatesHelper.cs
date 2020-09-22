using System;
using System.Linq;

namespace Battleships.Application
{
  public static class InputCoordinatesHelper
  {
    public static bool IsValid(string input)
    {
      if (string.IsNullOrEmpty(input))
        return false;

      if (input.Length != 2 && input.Length != 3)
        return false;

      if (!char.IsLetter(input[0]))
        return false;

      var firstDigit = input.ElementAt(1);

      if (!char.IsDigit(firstDigit))
        return false;

      var secondDigit = input.ElementAtOrDefault(2);

      if (secondDigit != default)
        if (!char.IsDigit(secondDigit))
          return false;

      return true;
    }

    public static (int X, int Y) Convert(string input)
    {
      if (!IsValid(input))
        throw new ArgumentException("Given input is invalid", nameof(input));

      var x = char.ToUpper(input[0]) - 64;
      var y = int.Parse(input.ElementAt(1).ToString() + input.ElementAtOrDefault(2));

      return (x, y);
    }

    public static string ConvertBack(int x, int y)
    {
      return $"{(char)(x + 64)}{y}";
    }
  }
}
