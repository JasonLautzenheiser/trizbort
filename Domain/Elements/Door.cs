namespace Trizbort.Domain.Elements; 

public class Door {
  public bool Lockable { get; set; }
  public bool Locked { get; set; }
  public bool Open { get; set; }
  public bool Openable { get; set; }
}