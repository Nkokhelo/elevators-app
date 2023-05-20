public class ElevatorsController
{
  private readonly IDisplay _display;
  private readonly IInput _input;
  private readonly List<Elevator> _elevators = new List<Elevator>();

  private List<string> _log = new List<string>();

  public ElevatorsController(int noOfElevators, IDisplay display, IInput elevatorInput)
  {
    _input = elevatorInput;
    _display = display;

    for (int i = 0; i < noOfElevators; i++)
    {
      _elevators.Add(new Elevator { Id = i + 1 });
    }

    // _elevators.ForEach(e => Console.WriteLine(e.ToString()));

  }

  public void Start()
  {
    // Seed testing data 
    var requests = new List<Request>() {
      new Request(1, 3, 5),
      new Request(1, 3, 5),
      new Request(1, 3, 5),
      new Request(1, 3, 5),
      new Request(1, 3, 5)
    };

    requests.ForEach(r => AddToElevator(r));
  }

  public void CallElevator()
  {
    var request = _input.GetRequestInfor();
    AddToElevator(request);
  }

  public void AddToElevator(Request r)
  {
    var elevator = _elevators.OrderBy(e => e.TravelTime(r)).First();
    elevator.AddRequest(r);
    _log.Add($"Elevator {elevator.Id} is called from floor {r.OriginFloor} to floor {r.DestinationFloor} to get {r.NoOfPeople} people");
  }

  public void GetState()
  {
    _elevators.ForEach(e => Console.WriteLine(e.ToString()));
  }


  public void ViewLog()
  {
    _log.ForEach(Console.WriteLine);
  }
}