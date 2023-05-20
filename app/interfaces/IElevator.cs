public interface IElevator
{

  int Id { get; set; }
  int NoOfPeople { get; set; }
  IList<Request> Requests { get; set; }

  int CurrentFloor { get; set; }

  void SetCurrentFloor(int value);

  EState State { get; set; }

  void AddRequest(Request request);
  Task Move();
  int TravelTime(Request request);
  string ToString();
}


