// See https://aka.ms/new-console-template for more information
var display = new Display();
var noOfElevators = 3;
var maxFloors = 5;
var maxPeople = 5;
var validation = new Validation(maxFloors, maxPeople);
var input = new Input(display, validation);
var logger = new AppLogger();
var elevatorsController = new ElevatorsController(noOfElevators, display, input, logger);

var emulator = new Emulator(display, logger, validation, input, elevatorsController, noOfElevators, maxFloors, maxPeople);
while (true)
{
  emulator.Run();
}