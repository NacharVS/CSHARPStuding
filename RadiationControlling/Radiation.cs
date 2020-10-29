using System;
using System.Collections.Generic;
using System.Text;
using HealthControlling;

namespace RadiationControlling
{
    public class Radiation
    {
        public delegate void ChangedDelegate(int health, int value);
        public delegate void RadiationLevel(string level);

        private int _max;
        private int _value;
        private int _level; // Хранит числовой уровень радиации
        private Health _health;
        private int _diff;
        public Radiation(Health health, int max)
        {
            _health = health; // Вариант не рабочий и временый
            Max = max;
            Level = 0;
            Diff = 0;
        }

        public int Max
        {
            get => _max;
            set
            {
                if (value == _max)
                    return;

                var oldMax = _max;
                _max = value;
                var maxDiff = _max - oldMax;

                if (_value > _max)
                {
                    var oldValue = _value;
                    _value = _max;
                    var diff = _value - oldValue;
                    ValueChangedEvent?.Invoke(_value, diff);

                    RadiationLevelChek(_value * 100 / _max); // Пересчитывает уровень радиации и проверяет изменения уровня радиации
                }

                MaxChangedEvent?.Invoke(_max, maxDiff);
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                if (value == _value)
                    return;

                var oldValue = _value;
                _value = value > Max ? Max : value;
                if (_value < 0)
                    _value = 0;
                var diff = _value - oldValue;

                ValueChangedEvent?.Invoke(_value, diff);

                RadiationLevelChek(_value * 100 / _max); // Пересчитывает уровень радиации и проверяет изменения уровня радиации
            }
        }
        public int Level
        {
            get => _level;
            set => _level = value;
        }
        public int Diff
        {
            get => _diff;
            set => _diff = value;
        }

        public void RadiationLevelChek(int percent) // Изменяет уровень радиации
        {
            int NewLevel;
            if (percent >= 90)
                NewLevel = 4;
            else if (percent >= 75)
                NewLevel = 3;
            else if (percent >= 50)
                NewLevel = 2;
            else if (percent >= 25)
                NewLevel = 1;
            else
                NewLevel = 0;
            if (Level != NewLevel) // Если он отличаеться от старого, снимает все дебафы старого и добовляет дебафы нового уровня
            {
                RadiationDebuff(_health, Level, false);
                RadiationDebuff(_health, NewLevel, true);
                Level = NewLevel;
            }
        }
        public void RadiationDebuff(Health health, int level, bool ONorOFF) // Тут все максимально плохо написано
        {
            switch (level)
            {
                case 0:
                    break;
                case 1:
                    if (ONorOFF)
                    {
                        (_health.Max, Diff) = HealthExtension.MaxRemove(_health, int.Parse($"{_health.Max * 0.1}"));
                    }
                    else
                    {
                        (_health.Max, Diff) = HealthExtension.MaxAdd(_health, int.Parse($"{Diff}"));
                    }
                    break;
                case 2:
                    if (ONorOFF)
                    {
                        (_health.Max, Diff) = HealthExtension.MaxRemove(_health, int.Parse($"{_health.Max * 0.2}"));
                    }
                    else
                    {
                        (_health.Max, Diff) = HealthExtension.MaxAdd(_health, int.Parse($"{Diff}"));
                    }
                    break;
                case 3:
                    if (ONorOFF)
                    {
                        (_health.Max, Diff) = HealthExtension.MaxRemove(_health, int.Parse($"{_health.Max * 0.5}"));
                    }
                    else
                    {
                        (_health.Max, Diff) = HealthExtension.MaxAdd(_health, int.Parse($"{Diff}"));
                    }
                    break;
                case 4:
                    if (ONorOFF)
                    {
                        (_health.Max, Diff) = HealthExtension.MaxRemove(_health, int.Parse($"{_health.Max * 0.9}"));
                    }
                    else
                    {
                        (_health.Max, Diff) = HealthExtension.MaxAdd(_health, int.Parse($"{Diff}"));
                    }
                    break;
            }
        }

        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;

        public static implicit operator int(Radiation radiation) => radiation.Value;

        public static Radiation operator +(Radiation radiation, int value)
        {
            radiation.ValueAdd(value);
            return radiation;
        }

        public static Radiation operator -(Radiation radiation, int value)
        {
            radiation.ValueRemove(value);
            return radiation;
        }
    }
}
