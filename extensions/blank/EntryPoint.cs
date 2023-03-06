using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace FR4.Blank
{
    public class ArmaExtension
    {
        //You can call back to Arma by using Callback(string name, string function, string data);
        public static unsafe delegate* unmanaged<string, string, string, int> Callback;
        
        [UnmanagedCallersOnly(EntryPoint = "RVExtensionRegisterCallback")]
        public static unsafe void RvExtensionRegisterCallback(delegate* unmanaged<string, string, string, int> callback)
        {
            Callback = callback;
        }
        
        //This tells the compiler to create an entrypoint named 'RVExtension'. This line should be added
        // to each method you want to export. Only public static are accepted.
        [UnmanagedCallersOnly(EntryPoint = "RVExtension")]
        /// <summary>
        /// This is the code that will get executed upon issuing a call to the extension from arma.
        /// </summary>
        /// <code>
        /// "blank" callExtension "ourString";
        /// </code>
        /// <param name="output">A pointer to the memory location of a chars array that will be read after issuing callExtension command</param>
        /// <param name="outputSize">An integer that determines the maximum length of the array</param>
        /// <param name="function">A pointer to the string passed from arma</param>
        public static unsafe void RVExtension(char* output, int outputSize, char* function)
        {
            try
            {
                string stringToReturn = "WARNING: This method is unavailable. Please use CallExtensionArgs. More info: https://community.bistudio.com/wiki/callExtension";
                Common.writeOutput(output, outputSize, stringToReturn);
            }
            catch (Exception e)
            {
                string stringToReturn = "WARNING: This method is unavailable. Please use CallExtensionArgs. More info: https://community.bistudio.com/wiki/callExtension | Exception: " + e.ToString();
                Common.writeOutput(output, outputSize, stringToReturn);
            }
        }

        [UnmanagedCallersOnly(EntryPoint = "RVExtensionArgs")]
        /// <summary>
        /// This is the code that will get executed upon issuing a call to the extension from arma. Pass back to arma a string formatted like an array.
        /// </summary>
        /// <code>
        /// "blank" callExtension ["MyFunction",["arg1",2,"arg3"]];
        /// </code>
        /// <param name="output">A pointer to the memory location of a chars array that will be read after issuing callExtension command</param>
        /// <param name="outputSize">An integer that determines the maximum length of the array</param>
        /// <param name="function">A pointer to the string passed from arma</param>
        /// <param name="argv">An array of pointers (char**). IE: A pointer to a memory location where pointers are stored(Still, can't be casted to IntPtr)</param>
        /// <param name="argc">Integer that points how many arguments there are in argv</param>

        public static unsafe void RVExtensionArgs(char* output, int outputSize, char* function, char** argv, int argc)
        {
            string stringFunction = Marshal.PtrToStringAnsi((IntPtr)function);
            List<string> parameters = Common.readInput(argv, argc);

            // Do something with the parameters

            string finalString = "Blank" + '\0';

            Common.writeOutput(output, outputSize, finalString);
        }

        [UnmanagedCallersOnly(EntryPoint = "RVExtensionVersion")]
        /// <summary>
        /// This is the code that will get executed once the extension gets loaded from arma.
        /// The output will get printed in RPT logs
        /// </summary>
        /// <param name="output">A pointer to the memory location of a chars array that will be read after the load of the extension.</param>
        /// <param name="outputSize">An integer that determines the maximum length of the array</param>
 
        public static unsafe void RVExtensionVersion(char* output, int outputSize)
        {

            string version = "1.0.0";

            string greetingsString = "|Blank| - " + version + " - |Blank|";
            
            string finalString = greetingsString + '\0';

            byte[] byteFinalString = Encoding.ASCII.GetBytes(finalString);
            
            Marshal.Copy(byteFinalString,0,(IntPtr)output,byteFinalString.Length);
        }
    }
}
