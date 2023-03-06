using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prometheus;

namespace FR4.Prometheus
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
        /// "prometheus" callExtension "ourString";
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
        /// "prometheus" callExtension ["MyFunction",["arg1",2,"arg3"]];
        /// </code>
        /// <param name="output">A pointer to the memory location of a chars array that will be read after issuing callExtension command</param>
        /// <param name="outputSize">An integer that determines the maximum length of the array</param>
        /// <param name="function">A pointer to the string passed from arma</param>
        /// <param name="argv">An array of pointers (char**). IE: A pointer to a memory location where pointers are stored(Still, can't be casted to IntPtr)</param>
        /// <param name="argc">Integer that points how many arguments there are in argv</param>

        public static unsafe void RVExtensionArgs(char* output, int outputSize, char* function, char** argv, int argc)
        {
            try
            {
                string stringFunction = Marshal.PtrToStringAnsi((IntPtr)function);
                List<string> parameters = Common.readInput(argv, argc);        
                switch (stringFunction)
                {
                    case "start":
                        var hostname = parameters[0].Replace("\"", "");
                        var port = Convert.ToInt32(parameters[1].Replace("\"", ""));
                        var server = new MetricServer(hostname: hostname, port: port);
                        server.Start();
                        string stringToReturn = "Info: New server started -> Hostname: " + hostname + " | Port: " + port;
                        Common.writeOutput(output, outputSize, stringToReturn);
                        break;
                    case "data":
                        StringBuilder sb = new StringBuilder();
                        IList<PrometheusData> prometheusData = fillPrometheusData(parameters.ToArray());
                        var t = Task.Run(() =>
                        {
                            foreach (var entry in prometheusData)
                            {
                                Metrics.CreateGauge(entry.key, entry.description).Set(entry.value);
                                sb.Append("Key: " + entry.key + " | Description: " + entry.description + " | Value: " + entry.value + "\n");
                            }
                        });
                        t.Wait();
                        string dataStringToReturn = "Info: New info sent to Prometheus -> \n" + sb.ToString();
                        Common.writeOutput(output, outputSize, dataStringToReturn);
                        break;
                    default:
                        string defaultStringToReturn = "ERROR: Default value is not a correct value";
                        Common.writeOutput(output, outputSize, defaultStringToReturn);
                        break;
                }
            }
            catch (Exception e)
            {
                string exceptionStringToReturn = "ERROR: " + e.ToString();
                Common.writeOutput(output, outputSize, exceptionStringToReturn);
            }
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
            string greetingsString = "Prometheus 0.0.1";

            Common.writeOutput(output, outputSize, greetingsString);
        }

        /**
            * This method is used to fill a Map with the Prometheus data that is passed from Arma 3
            * @param args The arguments passed from Arma 3
            * @return A Map with the Prometheus data
            */
        public static unsafe IList<PrometheusData> fillPrometheusData(string[] args)
        {
            //Create a new instance of our class
            IList<PrometheusData> arguments = new List<PrometheusData>();

            //Populate our list
            for (int i = 0; i < args.Length; i++)
            {
                String[] split = args[i].Split(':');
                for (int j = 0; j < split.Length; j++)
                {
                    split[j] = split[j].Replace("\"", "");
                }
                arguments.Add(new PrometheusData(split[0], split[1], Convert.ToDouble(split[2])));
            }

            return arguments;
        }
    }
}
