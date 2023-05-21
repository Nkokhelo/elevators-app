public class ElevatorsController
{
  private readonly IDisplay _display;
  private readonly IInput _input;
  private readonly List<Elevator> _elevators = new List<Elevator>();
  private readonly IAppLogger _appLogger;

  public bool IsRunning = false;
  public ElevatorsController(int noOfElevators, IDisplay display, IInput elevatorInput, IAppLogger appLogger)
  {
    _input = elevatorInput;
    _display = display;
    _appLogger = appLogger;
    for (int i = 0; i < noOfElevators; i++)
    {
      var e = new Elevator(i + 1, appLogger);
      _elevators.Add(e);
    }
  }

  public void Start()
  {
    if (IsRunning)
    {
      _display.ShowMessage("Elevators are already running...");
      return;
    }

    IsRunning = true;
    // Seeding data 
    var requests = new List<Request>() {
      new Request(2, 4, 21),
      new Request(5, 1, 10),
      new Request(2, 5, 14),
      new Request(1, 3, 9),
      new Request(4, 2, 25)
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
    var elevator = _elevators.OrderBy(e => e.TotalDistance(r)).First();
    elevator.AddRequest(r);
  }

  public void GetState()
  {
    _elevators.ForEach(e => Console.WriteLine(e.ToString()));
  }

  public void ViewLog()
  {
    _appLogger.View();
  }
}