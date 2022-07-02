using System;
using System.Collections.Generic;
using Trizbort.Domain.Enums;

namespace Trizbort.Domain; 

public static class Directions
{
  public static IEnumerable<MappableDirection> AllDirections {
    get {
      foreach (MappableDirection direction in Enum.GetValues(typeof(MappableDirection))) yield return direction;
    }
  }
}