public interface IElevator
{

  int Id { get; set; }
  int NoOfPeople { get; set; }
  string? Direction { get; set; }
  string State { get; set; }
  IList<Request> Requests { get; set; }
  int StopsBeforeDestination(int destination);

  void AddRequest(Request request);
}


