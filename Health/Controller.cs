using System;
using System.Collections;
using HealthControlling.IsRadiation;

namespace HealthControlling
{
    public sealed class Controller
    {
        private readonly Health _health;
        /*Radiation:*/
        public ArrayList RadSourceList = new ArrayList();
        private RadiationEffect _radiationEffect;
        private RadEffectConstant _radEffConstant;

        public Controller(Health health, RadEffectConstant radEffectConstant)
        {
            _health = health;
            _radEffConstant = radEffectConstant;
        }
        public void Update(DateTime dateTime)
        {
            Console.WriteLine($"Seconds: {dateTime.Second}");
            if (RadSourceList.Count != 0 || _radiationEffect != null) 
                UpdateRadiation();
        }

        public void UpdateRadiation()
        {
            if (_radiationEffect == null)
            {
                _radiationEffect = new RadiationEffect(_health, _radEffConstant);
            }

            if (_radiationEffect.End)
            {
                RadSourceList = new ArrayList(); // Эта строчка нужна если перс умрет от радиации, наврятли он сможет обнулить радиацию если находиться возле источника радиации... надеюсь
                _radiationEffect = null;
                return;
            }
                _radiationEffect.EffectUpdate(RadSourceList);
        }
    }
}
