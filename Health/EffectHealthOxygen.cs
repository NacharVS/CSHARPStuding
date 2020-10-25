using HealthControlling;
using OxygenControlling;
using System;
using System.Data;
using System.Threading;

namespace EffectHealthOxygenControlling
{
    public sealed class EffectHealthOxygen
    {
        //Вообще я не считаю это за код, фигня какая-та

        /*
         * Идея в том что есть функция которая вызывается при каждом изменении и в зависимости от нахождении героя 
         * или других действий происходит изменение процентов эффектов
         * 
         * Надо будет потом еще доделать, что бы эффект утихал
        */

        public delegate void BaffStatus(Oxygen oxygen, Health health);

        private double percentEffectHealth = 0;
        private double percentEffectOxygen = 0;

        private DateTime start;
        private int effectTime = 2000;

        public void EffectOxygenHunger(Oxygen oxygen, Health health, bool flag)
        {
            ChangeProcentDamageHealth(oxygen, health);
            ChangeProcentDamageOxygen(flag);
            oxygen += (int)(oxygen.Max * percentEffectOxygen);
            health += (int)(health * percentEffectHealth);
        }

        public void ChangeProcentDamageHealth(Oxygen oxygen, Health health)
        {
            if (oxygen < oxygen.Max * 0.2 && oxygen != 0)
            {
                percentEffectHealth = 0;
            }
            else if (oxygen == 0)
            {
                percentEffectHealth = -0.05;
            }
        }

        public void ChangeProcentDamageOxygen(bool flag)
        {
            if (flag)
            {
                percentEffectOxygen = -0.1;
                start = DateTime.Now;
            }
            else if((DateTime.Now - start).TotalMilliseconds < effectTime)
            {
                percentEffectOxygen = -0.05;
            }
            else
            {
                percentEffectOxygen = 0.075;
            }
        }

        public event BaffStatus OxygenHunger;
    }
}
