public interface IElevator
{
  void AddRequest(Request request);
  int CalcDistance(Request request);
  Task Move();
  string ToString();
  int TotalDistance(Request request);
}


