using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Baffs
{
    public class BaffTimer
    {
        public delegate void TimerEvent();

        private DateTime _ActivateTime;
        private TimeSpan _duration;

        public event TimerEvent OnTimerStart;
        public event TimerEvent OnTimerStop;

        public bool IsActive => _duration != TimeSpan.FromSeconds(0);

        public BaffTimer (TimeSpan duration)
            :this(duration,DateTime.Now)
        { }

        public BaffTimer (TimeSpan duration,DateTime current)
        {
            _duration = duration;
            _ActivateTime = current;
        }

        public void Activate(DateTime current)
        {
            _ActivateTime = current;
            OnTimerStart?.Invoke();
        }

        public void Update(DateTime current)
        {
            if (_duration <= current - _ActivateTime)
                Deactivate();
        }

        public void Deactivate()
        {
            _duration = TimeSpan.FromSeconds(0);
            OnTimerStop?.Invoke();
        }
    }
}
