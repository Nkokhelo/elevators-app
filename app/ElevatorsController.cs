public class ElevatorsController
{
  private readonly IDisplay _display;
  private readonly IInput _input;
  public ElevatorsController(IDisplay display, IInput elevatorInput)
  {
    _input = elevatorInput;
    _display = display;
  }

  public void Run()
  {
  }

  public void CallElevator()
  {
    _input.GetRequestInfor();
  }
}