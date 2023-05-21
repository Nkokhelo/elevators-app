public interface IElevator
{

  int Id { get; set; }
  int NoOfPeople { get; set; }
  IList<Request> Requests { get; set; }
  EState State { get; set; }

  void AddRequest(Request request);
  Task Move();
  int TotalDistance(Request request);

  string ToString();
}


