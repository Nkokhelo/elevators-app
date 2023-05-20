
public class Input : IInput
{

  private readonly IDisplay _display;
  private readonly Validation _validation;
  public Input(IDisplay display, Validation validation)
  {
    _display = display;
    _validation = validation;
  }

  public Request GetRequestInfor()
  {
    return _getElevatorRequest("Please enter the request information in this format: origin, destination, noOfPeople e.g. 1, 3, 5");


  }
  private Request _getElevatorRequest(string message)
  {
    do
    {
      _display.ShowMessage(message);
      var input = Console.ReadLine();

      try
      {
        var requestInput = _validation.ValidateInput(input);
        return new Request
        {
          OriginFloor = requestInput[0],
          DestinationFloor = requestInput[1],
          NoOfPeople = requestInput[2]
        };

      }
      catch (Exception e)
      {
        _display.ShowMessage($"'{input}' is invalid, please try again:");
      }
    }
    while (true);
  }
}