
public abstract class Elevator : IElevator
{
  public static int _weghtLimit = 1000;
  public int Id { get; set; }
  public int NoOfPeople { get; set; }
  public string? Direction { get; set; }
  public IList<Request> Requests { get; set; } = new List<Request>();
  public string State { get; set; } = "idle";

  public int StopsBeforeDestination(int destination)
  {
    throw new NotImplementedException();
  }

  public void AddRequest(Request request)
  {
    Requests.Add(request);
  }
}


