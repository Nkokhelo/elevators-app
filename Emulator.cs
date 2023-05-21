public class Emulator
{

  private readonly ElevatorsController _elevatorController;
  private readonly IDisplay _display;
  private int notElevators = 2;
  private int maxFloors = 6;
  private int maxPeople = 30;
  public Emulator(IDisplay display, IAppLogger appLogger, Validation validation, IInput input, ElevatorsController elevatorsController, int noOfElevators, int maxFloors, int maxPeople)
  {
    _display = display;
    notElevators = noOfElevators;
    this.maxFloors = maxFloors;
    this.maxPeople = maxPeople;
    _elevatorController = elevatorsController;
    AppSummary();
  }
  public void Run()
  {
    var k = Console.ReadKey();
    Console.Clear();
    switch (k.KeyChar)
    {
      case 'r':
        _display.ShowMessage("Starting...");
        _elevatorController.Start();
        break;
      case 'h':
        _display.ShowMessage("Get Help...");
        AppSummary();
        break;
      case 'l':
        _display.ShowMessage("App Log...");
        _elevatorController.ViewLog();
        break;
      case 'c':
        _display.ShowMessage("Calling elevator...");
        _elevatorController.CallElevator();
        break;
      case 'q':
        _display.ShowMessage("Quitting...");
        break;
      case 's':
        _display.ShowMessage("Elevators status...");
        _elevatorController.GetState();
        break;
      default:
        _display.ShowMessage("Invalid choice...");
        break;
    }
  }
  public void AppSummary()
  {
    _display.ShowMessage("**********************Welcome to the DVT*******************");
    _display.ShowMessage("This is a simple elevator emulator");
    _display.ShowMessage("Press 'r' to run the application emulation with default data");
    _display.ShowMessage("Press 'c' to call an elevator");
    _display.ShowMessage("Press 'l' to view the log");
    _display.ShowMessage("Press 'h' to get help");
    _display.ShowMessage("Press 's' to view the elevators status");
    _display.ShowMessage("Press 'q' to quit the application");
    _display.ShowMessage("***********************************************************");
  }
}

