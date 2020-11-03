using System;
using HealthControlling.IsRadiation;

namespace HealthControlling
{
    public sealed class Controller
    {
        private readonly Health _health;
        /*Radiation:*/
        public ERadiationLevel RadiationLevel = ERadiationLevel.Normal;
        private RadiationEffect _radiationEffect;

        public Controller(Health health)
        {
            _health = health;
        }
        public void Update(DateTime dateTime)
        {
            Console.WriteLine($"Seconds: {dateTime.Second}");
            if (RadiationLevel != ERadiationLevel.Normal || _radiationEffect != null) 
                UpdateRadiation(RadiationLevel);
        }

        public void UpdateRadiation(ERadiationLevel level)
        {
            if (_radiationEffect == null)
            {
                _radiationEffect = new RadiationEffect(_health);
            }

            if (_radiationEffect.End)
            {
                RadiationLevel = ERadiationLevel.Normal;
                _radiationEffect = null;
                return;
            }
                _radiationEffect.RadiationUpdate(level);
        }
    }
}
