public class Emulator
{

  private readonly ElevatorsController _elevatorController;
  private readonly IDisplay _display;
  private int notElevators = 2;
  private int maxFloors = 6;
  private int maxPeople = 30;
  public Emulator(IDisplay display)
  {
    _display = display;
    var validation = new Validation(maxFloors, maxPeople);
    var elevatorInput = new Input(display, validation);
    _elevatorController = new ElevatorsController(notElevators, display, elevatorInput);
    _display.ShowMessage("**********************Welcome to the DVT*******************");
  }

  public void Run()
  {
    //r - start the application
    //e - emulate lifts movements
    //c - call elevator
    //q - quit
    //s - elevators status
    var k = Console.ReadKey();
    Console.Clear();
    switch (k.KeyChar)
    {
      case 'r':
        _display.ShowMessage("Starting...");
        _elevatorController.Start();
        break;
      case 'h':
        _display.ShowMessage("App Log...");
        _elevatorController.ViewLog();
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
  public void GetAppSummary()
  {
    _display.ShowMessage("**********************Welcome to the DVT*******************");
    _display.ShowMessage("This is a simple elevator emulator");
    _display.ShowMessage("You can run the application emulation by pressing 'r'");
    _display.ShowMessage("You can call an elevator by pressing 'c'");
    _display.ShowMessage("You can view the log by pressing 'l'");
    _display.ShowMessage("You can view the elevators status by pressing 's'");
    _display.ShowMessage("You can quit the application by pressing 'q'");
    _display.ShowMessage("***********************************************************");
  }
}

