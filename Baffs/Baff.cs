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
        private DateTime _lastActivateTime;
        private TimeSpan _duration;
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
        public TimeSpan DurationTime
        {
            get=>_duration;
            set
            {
                _duration = value;
                
                if(_duration<=new TimeSpan(0))
                {
                    Deactivate();
                }        
            }
        }

        public Baff(string name , int strength , TimeSpan duration) 
        {
            _baffName = name;
            _baffStrength = strength;
            this._duration = duration;
            Activate(DateTime.Now);
        }

        public Baff(string name, int strength, TimeSpan duration, DateTime current)
        {
            _baffName = name;
            _baffStrength = strength;
            this._duration = duration;
            Activate(current);
        }

        //Активация баффа
        public virtual void Activate(DateTime current) 
        {
            _lastActivateTime = current;
            OnActivate?.Invoke();
        }

        //Деактивация баффа
        public virtual void Deactivate() 
        {
            _baffStrength = 0;
            _duration = TimeSpan.FromSeconds(0);
            OnDeactivate?.Invoke();
        }

        //по окончанию таймера выполняется деактивация
        public virtual void Update(DateTime current)
        {
            if (_duration <= current - _lastActivateTime) Deactivate();  
        }

        

        public bool IsActive => _duration > new TimeSpan(0);

        //события активации и деактивации бафа
        public event BaffStatusChanged OnActivate;
        public event BaffStatusChanged OnDeactivate;
        
    }
}
