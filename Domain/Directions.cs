using System;
using System.Collections.Generic;
using System.Linq;
using Trizbort.Domain.Enums;

namespace Trizbort.Domain; 

public static class Directions
{
  public static IEnumerable<MappableDirection> AllDirections => Enum.GetValues(typeof(MappableDirection)).Cast<MappableDirection>();
}