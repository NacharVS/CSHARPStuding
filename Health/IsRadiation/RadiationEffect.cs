using System;
namespace HealthControlling.IsRadiation
{
    public enum ERadiationLevel
    {
        Normal,
        Low,
        Hight
    }
    public sealed class RadiationEffect
    {
        public delegate void DebuffStatusChange();

        private readonly Health _health;
        private readonly Radiation _radiation;
        private double _RateOfChange = 1;
        private int _count = 0;
        public bool End;

        public RadiationEffect(Health health, Radiation radiation)
        {
            _health = health;
            _radiation = radiation;
        }

        public void RadiationUpdate(ERadiationLevel level)
        {
            UpdateRadiation(level);
            UpdateHealth();

            Console.WriteLine($"health Value:{_health.Value} Max:{_health.Max} "); // Времено для теста
            Console.WriteLine($"radiation Value:{_radiation.Value} Max:{_radiation.Max} "); // Времено для теста

            if (_health <= 0)
            {
                Console.WriteLine("Death"); // Времено для теста // У хп есть свой event для смерти, сдесь это выводиться только на времи проверки кода
                End = true;
                return;
            }

            if (_radiation <= 0)
            {
                Console.WriteLine("RadZERO"); // Времено для теста // Анологично строчке 36
                End = true;
                return;
            }
        }

        private void UpdateRadiation(ERadiationLevel level)
        {
            var percents = -0.05;
            if (level == ERadiationLevel.Hight)
            {
                percents = 0.05;
            }
            else if (level == ERadiationLevel.Low)
            {
                percents = 0.02;
            }
            var value = Convert.ToInt32(_radiation.Max * percents);
            _radiation.ValueAdd(value);
        }

        private void UpdateHealth()
        {
            _health.MaxSet(Convert.ToInt32(_health.Max / _RateOfChange)); // Возращает max и value здоровья к изночальным значением, чтобы не стакать дебафы
            _health.ValueSet(Convert.ToInt32(_health.Value / _RateOfChange));
            _RateOfChange = 1;
            if (_radiation.Value*100 / _radiation.Max < 25)
            {
                _count = 0;
                DebuffDeacivateEvent?.Invoke();
            }
            else if (_radiation.Value*100 / _radiation.Max < 50)
            {
                _RateOfChange = 0.8;
                _health.ValueSet(Convert.ToInt32(_health.Value * _RateOfChange));
                _health.MaxSet(Convert.ToInt32(_health.Max * _RateOfChange));
            }
            else if (_radiation.Value*100 / _radiation.Max < 75)
            {
                _RateOfChange = 0.65;
                _health.ValueSet(Convert.ToInt32(_health.Value * _RateOfChange));
                _health.MaxSet(Convert.ToInt32(_health.Max * _RateOfChange));
            }
            else if (_radiation.Value*100 / _radiation.Max < 95)
            {
                _RateOfChange = 0.5;
                _health.ValueSet(Convert.ToInt32(_health.Value * _RateOfChange));
                _health.MaxSet(Convert.ToInt32(_health.Max * _RateOfChange));
            }
            else
            {
                _count += 1; 
                _RateOfChange = 0.1;
                _health.ValueSet(Convert.ToInt32(_health.Value * _RateOfChange));
                _health.MaxSet(Convert.ToInt32(_health.Max * _RateOfChange));
                if (_count > 3)
                {
                        _health.ValueAdd(Convert.ToInt32(_health.Max * -0.1));
                }
            }
        }

        public event DebuffStatusChange DebuffActivateEvent;
        public event DebuffStatusChange DebuffDeacivateEvent;
    }
}
