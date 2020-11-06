using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.IsRadiation.Source
{
    public sealed class RadioactiveArea : IRadiationSource
    {
        public ERadiationLevel Level { get; } = ERadiationLevel.Hight;
        public event Action<ERadiationLevel> levelChangedEvent;
    }
}
