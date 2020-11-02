using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.Intoxication
{
    public class Controller
    {
        private static Health _health;
        private static Intoxication _intoxication;
        private static EffectIntoxicated _effect;

        public Controller(Health health, Intoxication intoxication)
        {
            _health = health;
            _intoxication = intoxication;
        }

        public void IntoxicatedStatus()
        {
            if (_intoxication.IsIntoxicated)
            {
                if (_effect == null)
                {
                    _effect = new EffectIntoxicated(_intoxication, _health);

                }
                else if (_effect != null && _effect.End != true)
                {
                    _effect.Update();
                }

                else if (_effect.End)
                {
                    _intoxication.IsIntoxicated = false;
                    _effect = null;
                }

            }

        }
    }
}