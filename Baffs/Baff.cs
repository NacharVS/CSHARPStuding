using System;
using System.Collections.Generic;
using System.Text;

namespace Baffs
{
    //Возможны доработки
    public abstract class Baff
    {
        public delegate void BaffStatusChanged();
        private string _baffName;
        private double _activeTime;
        private int _baffStrength;

        public int BaffStrength 
        {
            
            get => _baffStrength;          
            set => _baffStrength=value;
        }
        public string BaffName
        { 
            get => _baffName;
            set => _baffName = value;
        }
        public double ActiveTime
        {
            get=>_activeTime;
            set
            {
                _activeTime = value;
                if(_activeTime<=0)
                {
                    Deactivate();
                }        
            }
        }

        public Baff(string name , int strength , double time) 
        {
            _baffName = name;
            _baffStrength = strength;
            _activeTime = time;
            Activate();
        }

        //Активация баффа
        public virtual void Activate() 
        {
            StartTimer(_activeTime);
            OnActivate?.Invoke();
        }

        //Деактивация баффа
        public virtual void Deactivate() 
        {
            _baffStrength = 0;
            _activeTime = 0;
            OnDeactivate?.Invoke();
        }
        
        void StartTimer(double time)
        {
            //Здесь необходима реализация кода таймера
            //Есть вариант использовать карутины

            if (time <= 0) 
            {
                onTimerEnd();
            }
        }

        //по окончанию таймера выполняется деактивация
        void onTimerEnd() 
        {
            Deactivate();
        }

        public bool IsActive => _activeTime > 0;

        //события активации и деактивации бафа
        public event BaffStatusChanged OnActivate;
        public event BaffStatusChanged OnDeactivate;
        
    }
}
