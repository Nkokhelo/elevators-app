using System.Text.RegularExpressions;


public class Validation
{
  private int _maxFloors { get; set; }
  private int _maxPeople { get; set; }

  public Validation(int maxFloors, int maxPeople)
  {
    _maxFloors = maxFloors;
    _maxPeople = maxPeople;
  }

  public virtual Request ValidateAndPassInput(string? input)
  {
    if (string.IsNullOrEmpty(input))
      throw new Exception($"Nothing was provided");

    var requestInput = input.Replace(" ", "").Split(",");

    if (requestInput.Length != 3)
      throw new Exception($"{input} is not a valid reqest");

    var areAllInt = requestInput.All(x => Regex.IsMatch(x, "^\\d+$"));

    if (!areAllInt)
      throw new Exception($"{input} is not a valid request");

    var r = requestInput.Select(x => int.Parse(x)).ToArray();
    var request = new Request(r[0], r[1], r[2]);

    if (request.OriginFloor > _maxFloors || request.DestinationFloor > _maxFloors)
      throw new Exception($"Maximum floor is {_maxFloors}");

    if (request.NoOfPeople > _maxPeople)
      throw new Exception($"Maximum people alowed is per elevator {_maxPeople}");

    if (request.OriginFloor == request.DestinationFloor)
      throw new Exception($"Origin floor and destination floor cannot be the same");

    return request;

  }
}
