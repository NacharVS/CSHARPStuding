namespace HealthControllingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new SimpleTests();
            test.Setup();
            test.Execute();
        }
    }
}
