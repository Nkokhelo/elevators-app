public class Request
{
  public Request(int originFloor, int destinationFloor, int noOfPeople)
  {
    OriginFloor = originFloor;
    DestinationFloor = destinationFloor;
    NoOfPeople = noOfPeople;
  }
  public int OriginFloor { get; set; }
  public int DestinationFloor { get; set; }
  public int NoOfPeople { get; set; }

  public EState GetDirection()
  {
    return OriginFloor > DestinationFloor ? EState.Down : EState.Up;
  }
}