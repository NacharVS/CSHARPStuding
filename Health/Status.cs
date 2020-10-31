using System;
using HealthControlling.IsRadiation;

namespace HealthControlling
{
    public sealed class Status
    {
        // Создал оттдельный класс для проверки (в  теории) статуса всех эфектов которые могут быть наложенны на персонажа
        // Не знаю наскольку это будет лучше чем тупо апдейтить все эфекты каждый frame/tick или как мы это там реализуем

        private int _count = 0; // Времено для теста // Параметр для проверки коректности работы StatusUpdate

        private readonly Health _health;
        /*Radiation:*/
        public ERadiationLevel RadiationLevel = ERadiationLevel.Normal;
        private bool NewRadiation = true;
        private RadiationEffect _RadiationEffect;

        public Status(Health health)
        {
            _health = health;
        }
        public void StatusUpdate()
        {
            Console.WriteLine(_count++); // Времено для теста
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

            if (_RadiationEffect.End)
            {
                RadiationLevel = ERadiationLevel.Normal; //Это присвоение нужно если хп падает до нуля, но для этого у хп есть свой event который (наверное) будет сам все обрывать 
                NewRadiation = true;
                _RadiationEffect.End = false;
                return;
            }
            else if (!NewRadiation)
            {
                _RadiationEffect.RadiationUpdate(RadiationLevel);
            }
        }
    }
}
