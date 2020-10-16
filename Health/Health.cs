using System;

namespace HealthControlling
{
    public sealed class Health
    {
        public delegate void ChangedDelegate(int health, int value);

        private int _max;
        private int _value;

        public Health(int max)
        {
            Max = max;
        }

        public int Max
        {
            get => _max;
            set
            {
                if (value == _max)
                    return;

                var oldMax = _max;
                //Максимальное количество здоровья не может быть меньше нуля, также как и само количество здоровья. 
                //Если оставить возможность уменьшения макс значения ниже нуля, то в строке 39 в наше количество здоровья запишется отрицательное значение здоровья.
                //Конечно это не повлияет на исход get-ов IsAlive и IsDead, однако это может в процессе разработки привести к непредвиденным обстоятельствам. 
                //(Например неверному рассчету дифференциала diff и maxDiff)
                _max = value;
                if (_max < 0)
                    _max = 0;

                var maxDiff = _max - oldMax;

                if (_value > _max)
                {
                    var oldValue = _value;
                    Value = _max;
                    var diff = _value - oldValue;

                    //Так же данное изменение (падение значения _value > 0, как следствие изменения Max) можно исправить если присваивать новое значение _max не напрямую в поле _value, а через свойство Value.
                    //Помимо этого это избавит нас от необходимости дублировать вызов события "изменения значения здоровья" (ValueChangedEvent) в двух различных точках кода.

                    //ValueChangedEvent?.Invoke(_value, diff);
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

                if (IsDead)
                    DeathEvent?.Invoke();
            }
        }

        public bool IsAlive => _value > 0;
        public bool IsDead => _value <= 0;


        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;
        public event Action DeathEvent;

        public static implicit operator int(Health health) => health.Value;

        public static Health operator +(Health health, int value)
        {
            health.ValueAdd(value);
            return health;
        }

        public static Health operator -(Health health, int value)
        {
            health.ValueRemove(value);
            return health;
        }


    }
}
