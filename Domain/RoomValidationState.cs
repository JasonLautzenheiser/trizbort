using Trizbort.Domain.Enums;

namespace Trizbort.Domain; 

public enum RoomValidationStatus
{
  Valid,
  Invalid
}

public class RoomValidationState
{
  public RoomValidationStatus Status { get; set; }
  public ValidationType Type { get; set; }
  public string Message { get; set; }
}