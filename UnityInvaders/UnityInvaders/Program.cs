
using System;
using System.Reflection;

namespace UnityInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly miExtensionAssembly = Assembly.LoadFile(@"C:\Users\Diego\Source\Repos\UnityInvaders\UnityInvaders\UnityInvaders.Types\bin\Release\UnityInvaders.Types.dll");
            Type class1 = miExtensionAssembly.GetType("UnityInvaders.Types.Interfaces.Class1");
            object strategyLocationDefenses = Activator.CreateInstance(class1);

            Game game = new Game();
        }
    }
}
