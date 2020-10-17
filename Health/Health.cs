using System;

namespace HealthControlling
{
    public sealed class Health
    {
        //объявление делегата
        public delegate void ChangedDelegate(int health, int value);

        private int _max; //максимальное хп
        private int _value; //значение

        public Health(int max)//обычный конструктор в котором задается изначальное максимальное хп
        {
            Max = max;
        }

        public int Max //свойство переменной Max
        {
            get => _max; //передача значения переменной max
            set
            {
                //если передаваемое значение равно max то ничего не делать
                if (value == _max) 
                    return;

                //задание нового max и нахождение разницы
                var oldMax = _max;
                _max = value;
                var maxDiff = _max - oldMax;

                if (_value > _max)
                {
                    var oldValue = _value;
                    _value = _max;
                    var diff = _value - oldValue;
                    //проверка на пустоту делегата и вызов его
                    ValueChangedEvent?.Invoke(_value, diff);
                }

                //проверка на пустоту делегата и вызов его
                MaxChangedEvent?.Invoke(_max, maxDiff);
            }
        }

        public int Value
        {
            get => _value;//передача значения переменной value
            set
            {
                //если передаваемое значение равно value то ничего не делать
                if (value == _value)
                    return;

                //задание нового max и нахождение разницы
                var oldValue = _value;
                _value = value > Max ? Max : value;
                if (_value < 0)
                    _value = 0;
                var diff = _value - oldValue;

                //проверка на пустоту делегата и вызов его
                ValueChangedEvent?.Invoke(_value, diff);

                //проверка его живизну и на пустоту делегата и вызов его
                if (IsDead)
                    DeathEvent?.Invoke();
            }
        }

        //создание свойства по умолчанию (я не знаю как это правильно называется)
        public bool IsAlive => _value > 0;
        public bool IsDead => _value <= 0;

        //объявление эвентов
        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;
        public event Action DeathEvent;

        //метод неявного преобразование 
        public static implicit operator int(Health health) => health.Value;

        public static Health operator +(Health health, int value)//переопределение +
        {
            //вызов функции из HelthExtensions
            health.ValueAdd(value);
            return health;
        }

        public static Health operator -(Health health, int value) //переопределение -
        {
            //вызов функции из HelthExtensions
            health.ValueRemove(value);
            return health;
        }
    }
}