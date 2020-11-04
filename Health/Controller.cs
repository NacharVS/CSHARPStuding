using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling
{
    public sealed class Controller
    {
        private readonly Oxygen _oxygen;
        private readonly Health _health;
        public EOxygenCondition OxygenCondition = EOxygenCondition.Normal;
        private EffectOxygenHunger _effectOxygenHunger;

        public Controller(Oxygen oxygen, Health health)
        {
            _oxygen = oxygen;
            _health = health;
        }

        public void Update(DateTime current)
        {
            if (OxygenCondition != EOxygenCondition.Normal || _effectOxygenHunger != null)
                UpdateOxygen(current, OxygenCondition);
        }

        public void UpdateOxygen(DateTime current, EOxygenCondition condition)
        {
            if(_effectOxygenHunger == null)
            {
                _effectOxygenHunger = new EffectOxygenHunger(_oxygen, _health);
            }

            if (OxygenCondition == EOxygenCondition.Low)
            {
                OxygenCondition = EOxygenCondition.Normal;
                _effectOxygenHunger = null;
                return;
            }
            else if (_effectOxygenHunger != null)
            {
                _effectOxygenHunger.Update(current, condition);
            }
        }
    }
}
