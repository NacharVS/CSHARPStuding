namespace HealthControllingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new SimpleTest();
            test.Setup();
            test.Execute();
        }
    }
}