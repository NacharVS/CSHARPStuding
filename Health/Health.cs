using System;

namespace HealthControlling
{
    public sealed class Health //Класс не имеет наследников так как имеет модификатор sealed
    {
        public delegate void ChangedDelegate(int health, int value); //Объявление делегата

        private int _max; //максимальное значение зп
        private int _value;//значение

        public Health(int max)//конструктор класса Health
        {
            if (max < 0)
                throw new ArgumentException(nameof(max));

            _max = max;
            _value = max;
        }

        public int Max
        {
            get => _max; // возвращает значение _max
            set
            {
                if (value == _max) //если значение уже равно максимально то выходим из метода 
                    return;

                var oldMax = _max;
                _max = value;
                var maxDiff = _max - oldMax;
                // Находим разницу между старым максимальным значением и новым максимальным значением
                if (_value > _max)
                {
                    var oldValue = _value;
                    _value = _max;
                    var diff = _value - oldValue;
                    ValueChangedEvent?.Invoke(_value, diff);
                }
                //Находим разницу между старым и новым _value и вызваем делегат с проверкой на null если _value больше максимального значения 

                MaxChangedEvent?.Invoke(_max, maxDiff); //вызываем делегат с проверкой на null
            }
        }

        public int Value
        {
            get => _value; //возращение значение _value
            set
            {
                if (value == _value) //если значение уже равно _value то выходим из метода
                    return;

                var oldValue = _value;
                _value = value > Max ? Max : value; //_value будет равнятся Max если значение больше Max в противном случае равняется value
                if (_value < 0) //_value не может быть отрицательным в противном случае _value равняется 0
                    _value = 0;
                var diff = _value - oldValue;
                ValueChangedEvent?.Invoke(_value, diff);
                //Найдем разницу между новым и старым _value и вызываем делегат с проверкой на null
                if (IsDead)
                    DeathEvent?.Invoke();
                //Если IsDead true то вызвать делегат с проверкой на null
            }
        }

        //Создаем свойства по умолчанию
        public bool IsAlive => _value > 0;
        public bool IsDead => _value <= 0;

        //Создаем события
        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;
        public event Action DeathEvent;

        //Метод неявное преобразование 
        public static implicit operator int(Health health) => health.Value;

        public static Health operator +(Health health, int value) // перегрузка оператора +
        {
            health.ValueAdd(value);//Вызываем метод из HealtExtensions и возвращаем  health
            return health;
        }

        public static Health operator -(Health health, int value) // перегрузка оператора +
        {
            health.ValueRemove(value);//Вызываем метод из HealtExtensions и возвращаем  health
            return health;
        }
    }
}
