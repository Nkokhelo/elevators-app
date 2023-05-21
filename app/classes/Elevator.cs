
public class Elevator : IElevator
{
  public int Id { get; set; }
  public int NoOfPeople { get; set; } = 0;
  public IList<Request> Requests { get; set; } = new List<Request>();
  private int CurrentFloor { get; set; } = 0;
  public EState State { get; set; } = EState.Stopped;
  private bool _isMoving { get; set; } = false;
  private string jobStatus { get; set; } = "idle";
  private int _totalDistance { get; set; } = 0;
  private readonly IAppLogger _appLogger;

  public Elevator(int id, IAppLogger appLogger)
  {
    Id = id;
    _appLogger = appLogger;
  }

  public int TotalDistance(Request request)
  {

    var summonDistance = Math.Abs(CurrentFloor - request.OriginFloor);//1 trip to origin floor
    if (Requests.Count() == 0)
      return summonDistance;

    var totalDistance = Requests.Sum(r => Math.Abs(r.OriginFloor - r.DestinationFloor)) * 2;

    // last destination for the lisft to be on, add/subtract that to the distance
    var lastRequest = Requests.Last();
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

    Requests.Add(request);
    _totalDistance += CalcDistance(request);
    if (!_isMoving)
      await Move();
  }

  public int CalcDistance(Request request)
  {
    var tripLastFloor = Requests.Count() == 0 ? 0 : Requests.Last().DestinationFloor;
    return Math.Abs(tripLastFloor - request.OriginFloor) + Math.Abs(request.OriginFloor - request.DestinationFloor);

  }
  public async Task Move()
  {
    _isMoving = true;
    var request = Requests.First();

    // I used strings to avoing retiving enums by number
    // (fetching = 0, loading = 1, moving = 2, unloading = 3)
    var liftRequestStates = new string[] { "fetching", "loading", "moving", "unloading" };
    while (Requests.Count() > 0)
    {
      for (int i = 0; i < 4; i++)
      {
        jobStatus = liftRequestStates[i];
        switch (liftRequestStates[i])
        {
          case "fetching":
            State = CurrentFloor > request.OriginFloor ? EState.Down : EState.Up;
            _appLogger.Add($"Elevator {Id} is fetching {request.NoOfPeople} people from {CurrentFloor} to {request.OriginFloor}");
            break;
          case "loading":
            State = EState.Stopped;
            NoOfPeople += request.NoOfPeople;
            CurrentFloor = request.OriginFloor;
            _appLogger.Add($"Elevator {Id} is loading {request.NoOfPeople} people at {CurrentFloor}");
            break;
          case "moving":
            State = request.GetDirection();
            _appLogger.Add($"Elevator {Id} is moving {request.NoOfPeople} people from {CurrentFloor} to {request.DestinationFloor}");
            break;
          case "unloading":
            State = EState.Stopped;
            NoOfPeople -= request.NoOfPeople;
            CurrentFloor = request.DestinationFloor;
            _appLogger.Add($"Elevator {Id} is unloading {request.NoOfPeople} people at {CurrentFloor} from {request.OriginFloor} ");
            break;
        }
        await Task.Delay(3000);
      }
      Requests.RemoveAt(0);
      _appLogger.Add($"---------------------Elevator {Id} job requests is now {Requests.Count()}-----------------------");
    }
    _isMoving = false;
  }

  public override string ToString()
  {
    return $"id:{Id}, NoOfPeople:{NoOfPeople}, Direction:{State}, Current Floor:{CurrentFloor}, State:{jobStatus}";
  }
}




