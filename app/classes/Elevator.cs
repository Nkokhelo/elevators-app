


public class Elevator : IElevator
{
  protected int _id { get; set; }
  protected int _noOfPeople { get; set; } = 0;
  protected IList<Request> _requests { get; set; } = new List<Request>();
  protected int _currentFloor { get; set; } = 0;
  protected EState _state { get; set; } = EState.Stopped;
  protected bool _isMoving { get; set; } = false;
  protected string jobStatus { get; set; } = "idle";
  protected int _totalDistance { get; set; } = 0;
  protected readonly IAppLogger _appLogger;

  public Elevator(int id, IAppLogger appLogger)
  {
    _id = id;
    _appLogger = appLogger;
  }

  public int TotalDistance(Request request)
  {

    var summonDistance = Math.Abs(_currentFloor - request.OriginFloor);//1 trip to origin floor
    if (_requests.Count() == 0)
      return summonDistance;

    var totalDistance = _requests.Sum(r => Math.Abs(r.OriginFloor - r.DestinationFloor)) * 2;

    // last destination for the lisft to be on, add/subtract that to the distance
    var lastRequest = _requests.Last();
    if (request.OriginFloor > lastRequest.DestinationFloor && lastRequest.GetDirection() == EState.Up)
    {
      totalDistance -= Math.Abs(lastRequest.DestinationFloor - request.OriginFloor);
    }
    else
    {
      totalDistance += Math.Abs(lastRequest.DestinationFloor - request.OriginFloor);
    }

    return totalDistance;
  }

  public async void AddRequest(Request request)
  {

    _requests.Add(request);
    _totalDistance += CalcDistance(request);
    if (!_isMoving)
      await Move();
  }

  public int CalcDistance(Request request)
  {
    var tripLastFloor = _requests.Count() == 0 ? 0 : _requests.Last().DestinationFloor;
    return Math.Abs(tripLastFloor - request.OriginFloor) + Math.Abs(request.OriginFloor - request.DestinationFloor);

  }
  public async Task Move()
  {
    _isMoving = true;
    var request = _requests.First();

    // I used strings to avoing retiving enums by number
    // (fetching = 0, loading = 1, moving = 2, unloading = 3)
    var liftRequestStates = new string[] { "fetching", "loading", "moving", "unloading" };
    while (_requests.Count() > 0)
    {
      for (int i = 0; i < 4; i++)
      {
        jobStatus = liftRequestStates[i];
        switch (liftRequestStates[i])
        {
          case "fetching":
            _state = _currentFloor > request.OriginFloor ? EState.Down : EState.Up;
            _appLogger.Add($"Elevator {_id} is fetching {request.NoOfPeople} people from {_currentFloor} to {request.OriginFloor}");
            break;
          case "loading":
            _state = EState.Stopped;
            _noOfPeople += request.NoOfPeople;
            _currentFloor = request.OriginFloor;
            _appLogger.Add($"Elevator {_id} is loading {request.NoOfPeople} people at {_currentFloor}");
            break;
          case "moving":
            _state = request.GetDirection();
            _currentFloor = -1;
            _appLogger.Add($"Elevator {_id} is moving {request.NoOfPeople} people from {request.OriginFloor} to {request.DestinationFloor}");
            break;
          case "unloading":
            _state = EState.Stopped;
            _noOfPeople -= request.NoOfPeople;
            _currentFloor = request.DestinationFloor;
            _appLogger.Add($"Elevator {_id} is unloading {request.NoOfPeople} people at {_currentFloor} from {request.OriginFloor} ");
            break;
        }
        await Task.Delay(3000);
      }
      _requests.RemoveAt(0);
      _appLogger.Add($"---------------------Elevator {_id} job requests is now {_requests.Count()}-----------------------");
      jobStatus = _requests.Count() == 0 ? "idle" : jobStatus;
    }
    _isMoving = false;
  }

  public override string ToString()
  {
    return $"Elevator:{_id}, Floor:{(_currentFloor == -1 ? "None" : _currentFloor)}, Direction:{_state}, State:{jobStatus}, People:{_noOfPeople}";
  }
}




