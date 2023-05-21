
// UI Interaction class
public class Emulator
{
  private readonly ElevatorsController _elevatorController;
  private readonly IDisplay _display;
  private int _notElevators;
  private int _maxFloors;
  private int _maxPeople;
  public Emulator(IDisplay display, IAppLogger appLogger, Validation validation, IInput input, ElevatorsController elevatorsController, int noOfElevators, int maxFloors, int maxPeople)
  {
    _display = display;
    _notElevators = noOfElevators;
    this._maxFloors = maxFloors;
    this._maxPeople = maxPeople;
    _elevatorController = elevatorsController;
    AppSummary();
  }
  public void Run()
  {
    var k = Console.ReadKey();
    var howTo = "[press : 'r' - run,  'h' - help, 'q' - quit,  'l' - latest log, 'c' - call an elevator, 's' - latest elevators status]";
    Console.Clear();
    switch (k.KeyChar)
    {
      case 'r':
        _display.ShowMessage($"Running: {howTo}");
        if (_elevatorController.IsRunning)
        {
          _display.ShowMessage("Elevators are already running...");
          return;
        }
        _elevatorController.Start();
        break;
      case 'h':
        _display.ShowMessage("Get Help:");
        AppSummary();
        break;
      case 'l':
        _display.ShowMessage($"App Log: {howTo}");
        _elevatorController.ViewLog();
        break;
      case 'c':
        _display.ShowMessage($"Calling elevator: {howTo}");
        _elevatorController.CallElevator();
        break;
      case 'q':
        _display.ShowMessage($"Quitting: {howTo}");
        break;
      case 's':
        _display.ShowMessage($"Elevators status: {howTo}");
        _display.ShowMessage($"The status is in intervals of 3 seconds: you can constantly press 's' to view the latest status");
        _elevatorController.GetState();
        break;
      default:
        _display.ShowMessage($"Invalid choice: {howTo}");
        break;
    }
  }
  public void AppSummary()
  {
    _display.ShowMessage("**********************Welcome to the DVT*******************");
    _display.ShowMessage("This is a simple elevator emulator");
    _display.ShowMessage("Press 'r' to run the application");
    _display.ShowMessage("Press 'c' to call an elevator");
    _display.ShowMessage("Press 'l' to view the log");
    _display.ShowMessage("Press 'h' to get help");
    _display.ShowMessage("Press 's' to view the elevators status");
    _display.ShowMessage("Press 'q' to quit the application");
    _display.ShowMessage("***********************************************************");
  }
}

