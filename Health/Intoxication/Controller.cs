
namespace HealthControlling.Intoxication
{
    public class Controller
    {
        private readonly  Health _health;
        private readonly Intoxication _intoxication;
        private  EffectIntoxicated _effect;

        public Controller(Health health, Intoxication intoxication)
        {
            _health = health;
            _intoxication = intoxication;
        }

        public void IntoxicatedUpdate()
        {
            if (_intoxication.IsIntoxicated)
                Update();
        }

        public void Update()
        {

            if (_effect == null)
            { 
                _effect = new EffectIntoxicated(_intoxication, _health);

            }
            else if (_effect.End)
            {
                _effect = null;
            }
            else if (_effect != null && !_effect.End)
            {
                _effect.Update();
            }

        }
    }
}