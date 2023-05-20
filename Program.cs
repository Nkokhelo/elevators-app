// See https://aka.ms/new-console-template for more information
var display = new Display();
var emulator = new Emulator(display);
while (true)
{
  emulator.Run();
}