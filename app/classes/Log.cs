

public class Log : ILog
{

  List<string> _logHistory = new List<string>();

  public void Add(string log)
  {
    _logHistory.Add(log);
  }

  public void View()
  {
    _logHistory.ForEach(Console.WriteLine);
  }
}