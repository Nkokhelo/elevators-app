using System.Text.RegularExpressions;


public class Validation
{
  public int MaxFloors { get; set; }
  public int MaxPeople { get; set; }

  public Validation(int maxFloors, int maxPeople)
  {
    MaxFloors = maxFloors;
    MaxPeople = maxPeople;
  }

  public virtual int[] ValidateInput(string? input)
  {
    if (string.IsNullOrEmpty(input))
      throw new Exception();

    var requestInput = input.Replace(" ", "").Split(",");

    if (requestInput.Length != 3)
      throw new Exception();

    var areAllInt = requestInput.All(x => Regex.IsMatch(x, "^\\d+$"));

    if (!areAllInt)
      throw new Exception();

    return requestInput.Select(x => int.Parse(x)).ToArray();
  }
}

public class EmegencyValidation : Validation
{
  public EmegencyValidation(int maxFloors, int maxPeople) : base(maxFloors, maxPeople)
  {
  }

  public override int[] ValidateInput(string? input)
  {
    if (string.IsNullOrEmpty(input))
      throw new Exception();

    var requestInput = input.Replace(" ", "").Split(",");

    if (requestInput.Length != 2)
      throw new Exception();

    var areAllInt = requestInput.All(x => Regex.IsMatch(x, "^\\d+$"));

    if (!areAllInt)
      throw new Exception();

    return requestInput.Select(x => int.Parse(x)).ToArray();
  }
}
