


public class Elevator : IElevator
{
  public int Id { get; set; }
  public int NoOfPeople { get; set; } = 0;
  public IList<Request> Requests { get; set; } = new List<Request>();

  private int CurrentFloor { get; set; } = 0;
  public EState State { get; set; } = EState.Stopped;
  private bool _isMoving { get; set; } = false;

  public int TravelTime(Request request)
  {

    var fewestFloors = 0;
    if (Requests.Count() == 0)
      return fewestFloors;

    return Requests.Sum(r => Math.Abs(r.OriginFloor - r.DestinationFloor)) * 2;
  }

  public async void AddRequest(Request request)
  {
    Requests.Add(request);
    if (!_isMoving)
      await Move();
  }

  public async Task Move()
  {
    _isMoving = true;
    var request = Requests.First();

    // I used strings to avoing retiving enums by number
    // (loading = 0, moving = 1, unloading= 2)
    var liftRequestStates = new string[] { "loading", "moving", "unloading" };
    while (Requests.Count() > 0)
    {
      for (int i = 0; i < 3; i++)
      {
        switch (liftRequestStates[i])
        {
          case "loading":
            State = EState.Stopped;
            NoOfPeople += request.NoOfPeople;
            CurrentFloor = request.OriginFloor;
            break;
          case "moving":
            State = request.GetDirection();
            break;
          case "unloading":
            State = EState.Stopped;
            NoOfPeople -= request.NoOfPeople;
            CurrentFloor = request.DestinationFloor;
            break;
        }
        await Task.Delay(3000);
      }

      Requests.Remove(request);
    }
    _isMoving = false;
  }

  public override string ToString()
  {
    return $"id:{Id}, NoOfPeople:{NoOfPeople}, Direction:{State}, on Floor:{CurrentFloor} ";
  }
}




