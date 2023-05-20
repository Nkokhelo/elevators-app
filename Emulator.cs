public class Emulator
{

  private readonly ElevatorsController _elevatorController;
  public Emulator()
  {
    var display = new Display();
    var validation = new Validation(6, 30);
    var elevatorInput = new Input(display, validation);
    _elevatorController = new ElevatorsController(display, elevatorInput);
    display.ShowMessage("**********************Welcome to the DVT*******************");
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
        Console.WriteLine("Starting...");
        break;
      case 'e':
        Console.WriteLine("Emulating...");
        break;
      case 'c':
        Console.WriteLine("Calling elevator...");
        _elevatorController.CallElevator();
        break;
      case 'q':
        Console.WriteLine("Quitting...");
        break;
      case 's':
        Console.WriteLine("Elevators status...");
        break;
      default:
        Console.WriteLine("Invalid choice...");
        break;
    }



    // Console.Clear();
    // Console.WriteLine($"Your choice is: {k.KeyChar}");


    // elevatorController.Run();
  }
}

