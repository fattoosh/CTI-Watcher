using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using JulMar.Atapi;

namespace ConsoleApp1
{
    class Program
    {
        private static TapiManager _mgr = new TapiManager("ConsoleApp1");

        public static void start()
        {
            //            Console.ReadLine();
            while (true)
                //                        {
                if (_mgr.Initialize())
            {
                
//                _mgr.NewCall += NewCall;
//                _mgr.LineChanged += LineChanged;
//                _mgr.CallInfoChanged += CallInfoChanged;
//                _mgr.CallStateChanged += CallStateChanged;
                TapiLine line = _mgr.GetLineByName("EXTENSION 200 Keyset", false);

                if (line != null)
                {

                    try
                    {
//                       
                            //line.Monitor();
                            Console.WriteLine("Line: {0} opened!", line.Name);

                            line.NewCall += new EventHandler<NewCallEventArgs>(NewCall);
                            ;
                            line.Changed += new EventHandler<LineInfoChangeEventArgs>(Changed);
                            line.CallInfoChanged += new EventHandler<CallInfoChangeEventArgs>(CallInfoChanged);
                            line.CallStateChanged += new EventHandler<CallStateEventArgs>(CallStateChanged);
                            line.AddressChanged += new EventHandler<AddressInfoChangeEventArgs>(AddressChanged);

                            line.Monitor();
//                            line.Open(line.Capabilities.MediaModes);
                            Console.WriteLine("Monitoring Started!");

                        
                    }
                    catch (TapiException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    _mgr.Shutdown();
                }
            }
        }

        private static void PhoneStateChanged(object sender, PhoneStateEventArgs e)

        {
           
            Console.WriteLine("Phone State Changed!");
        }

        private static void LineChanged(object sender, LineInfoChangeEventArgs e)

        {
            Console.WriteLine("Line Changed!");
        }

        private static void AddressChanged(object sender, AddressInfoChangeEventArgs e)
        {
            Console.WriteLine("Address Changed!");
        }

        private static void NewCall(object sender, NewCallEventArgs e)
        {
            Console.WriteLine("New Call!");
            Console.WriteLine(e.Call.CallerId);
//            Console.WriteLine(e.Call.TrunkId);
//            Console.WriteLine(e.Call.Address);
//            Console.WriteLine(e.Call.CalledId);
//            Console.WriteLine(e.Call.CallState);



        }

        private static void Changed(object sender, LineInfoChangeEventArgs e)
        {
            Console.WriteLine("Changed!");
            Console.WriteLine("Calls Count: {0}", e.Line.GetCalls().Count());

            
        }
        private static void CallStateChanged(object sender, CallStateEventArgs e)
        {
            Console.WriteLine("State Changed!");

            Console.WriteLine(e.Call.CallerId);
//            Console.WriteLine(e.Call.TrunkId);
//            Console.WriteLine(e.Call.Address);
//            Console.WriteLine(e.Call.CalledId);
//            Console.WriteLine(e.Call.CallState);
            
        }

        private static void CallInfoChanged(object sender, CallInfoChangeEventArgs e)
        {
            Console.WriteLine("Info Changed!");
        }

        static void Main(string[] args)
        {
//            _mgr.Shutdown();
            start();
        }
    }
}
