using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();

            Task.WaitAll(app.Run());
        }
    }
}
