using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
namespace FR4.Prometheus
{
    public class Common
    {
        public static unsafe void writeOutput(char* output, int outputSize, string outputString)
        {
            //Now that we have our reversed string terminated by the null character,
            //we have to convert it to a byte array in order to allow the arma extension loader (Which is c/c++)
            //to "decode" our string. We'll basically copy our string into the location pointed by the 'output' pointer.
            outputString = outputString + '\0';

            byte[] byteFinalString = Encoding.ASCII.GetBytes(outputString);

            //We're done, now that we have our properly encoded byte array, we have to 'assign' its value to the
            //memory location pointed by output pointer.
            Marshal.Copy(byteFinalString, 0, (IntPtr)output, byteFinalString.Length);
        }

        public static unsafe List<string> readInput(char** argv, int argc)
        {
            //Let's create a list with all the parameters inside
            List<String> parameters = new List<string>();

            //Populate our List
            for (int i = 0; i < argc; i++)
            {
                string curStr = Marshal.PtrToStringAnsi((IntPtr)argv[i]);
                parameters.Add(curStr);
            }

            return parameters;
        }
    }
}
