using System;
using System.Collections.Generic;
using System.Text;
using HealthControlling.IsRadiation;

namespace HealthControlling
{
    public sealed class Status
    {
        // Создал оттдельный класс для проверки (в  теории) статуса всех эфектов которые могут быть наложенны на персонажа
        // Не знаю наскольку это будет лучше чем тупо апдейтить все эфекты каждый frame/tick или как мы это там реализуем
        // Код сырой, время час ночи, запушил чтобы потом почитать без доступа к компьютеру

        private readonly Health _health;
        /*Radiation:*/ public ERadiationLevel RadiationLevel = ERadiationLevel.Normal;  private bool NewRadiation = true;  private RadiationEffect _RadiationEffect;

        public Status(Health health)
        {
            _health = health;
        }
        public void StatusUpdate()
        {
            if (RadiationLevel != ERadiationLevel.Normal || !NewRadiation) RadiationStatus();
        }

        private void RadiationStatus()
        {
            if (NewRadiation)
            {
                NewRadiation = false;
                Radiation radiation = new Radiation(100);
                _RadiationEffect = new RadiationEffect(_health, radiation);
            }
            else if (!NewRadiation)
            {
                _RadiationEffect.RadiationUpdate(RadiationLevel);
            }
            if (_RadiationEffect.End)
            {
                NewRadiation = true; // К этому моменту RadiationLevel итак будет normal, а изменение данной переменной оборвет вызов даного метода (строчка 19)
                return;
            }
        }
    }
}
