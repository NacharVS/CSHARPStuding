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
        private TimeSpan _activeTime;
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
        public TimeSpan ActiveTime
        {
            get=>_activeTime;
            set
            {
                _activeTime = value;
                
                if(_activeTime<=new TimeSpan(0))
                {
                    Deactivate();
                }        
            }
        }

        public Baff(string name , int strength , TimeSpan time) 
        {
            _baffName = name;
            _baffStrength = strength;
            _activeTime = time;
            Activate(DateTime.Now);
        }

        public Baff(string name, int strength, TimeSpan time, DateTime current)
        {
            _baffName = name;
            _baffStrength = strength;
            _activeTime = time;
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
            _activeTime = new TimeSpan(0);
            OnDeactivate?.Invoke();
        }

        //по окончанию таймера выполняется деактивация
        void Update(DateTime current)
        {
            if (_activeTime <= current - _lastActivateTime) Deactivate();

            
        }

        

        public bool IsActive => _activeTime > new TimeSpan(0);

        //события активации и деактивации бафа
        public event BaffStatusChanged OnActivate;
        public event BaffStatusChanged OnDeactivate;
        
    }
}
