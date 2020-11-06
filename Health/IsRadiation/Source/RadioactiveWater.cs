using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.IsRadiation.Source
{
    public sealed class RadioactiveWater : IRadiationSource
    {
        public ERadiationLevel Level { get; } = ERadiationLevel.Low;
        public event Action<ERadiationLevel> levelChangedEvent;
    }
}
