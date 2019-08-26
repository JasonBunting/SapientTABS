using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapientTABS
{
    public class Game
    {
        private int Counter = 0;

        // this is an example of a property
        public string DataToPass => string.Format("Passed data: {0}", ++Counter);

        // this is an example of a method call
        public string GetSomethingFromExternalDLL(string initialString)
        {
            return string.Format("Here's the string you passed in, now concatenated to this: {0}", initialString);
        }
    }
}


/* 
	private dynamic foo = null;
    private dynamic Foo
    {
        get
        {
            if (foo == null)
            {
                foo = System.Activator.CreateInstance(System.Reflection.Assembly.LoadFile(@"D:\Source\Repos\SapientTABS\SapientTABS\bin\Debug\SapientTABS.dll").GetType("SapientTABS.Game"));
            }
            return foo;
        }
    }
*/
