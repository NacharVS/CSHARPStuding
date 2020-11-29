using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.IsRadiation
{
    public struct RadEffectConstant
    {
        public double RateOfChangeLow { get; private set; }
        public double RateOfChangeMedium { get; private set; }
        public double RateOfChangeHight { get; private set; }
        public double RateOfChangeDeadly { get; private set; }

        public int PassiveRadiationExtraction { get; private set; } //Запись: 5% -- 5

        public int LowLevel { get; private set; }
        public int MediumLevel { get; private set; }
        public int HightLevel { get; private set; }
        public int DeadlyLevel { get; private set; }

        public int DeadlyCountIncrease { get; private set; }

        public RadEffectConstant(double RateOfChangeLow,
                                 double RateOfChangeMedium,
                                 double RateOfChangeHight,
                                 double RateOfChangeDeadly,
                                 int PassiveRadiationExtraction,
                                 int LowLevel,
                                 int MediumLevel,
                                 int HightLevel,
                                 int DeadlyLevel,
                                 int DeadlyCountIncrease)
        {
            this.RateOfChangeLow = RateOfChangeLow;
            this.RateOfChangeMedium = RateOfChangeMedium;
            this.RateOfChangeHight = RateOfChangeHight;
            this.RateOfChangeDeadly = RateOfChangeDeadly;
            this.PassiveRadiationExtraction = PassiveRadiationExtraction;
            this.LowLevel = LowLevel;
            this.MediumLevel = MediumLevel;
            this.HightLevel = HightLevel;
            this.DeadlyLevel = DeadlyLevel;
            this.DeadlyCountIncrease = DeadlyCountIncrease;
        }

    }
}
