using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.IsRadiation
{
    public interface IRadiationSource
    {
        ERadiationLevel Level { get; }
        event Action<ERadiationLevel> levelChangedEvent;
    }
}
