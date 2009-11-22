using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using EndpointSystems.OrchestrationLibrary;
using System.Diagnostics;

namespace OrchLibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory + "..\\..\\..\\orchs\\");

            foreach (FileInfo f in di.GetFiles())
            {
                XmlDocument x = new XmlDocument();
                x.Load(f.FullName);
                Console.WriteLine("[" + p.RunTime() + "] Loading orch " + f.Name);
                BtsOrch b = new BtsOrch(x);
                Console.WriteLine("[" + p.RunTime() + "] " + f.Name + " loaded.");
                
                Microsoft.VS.Modeling.IMS.Store store = new Microsoft.VS.Modeling.IMS.Store();
                Microsoft.VS.Modeling.IMS.LoadedElementDirectory load = new Microsoft.VS.Modeling.IMS.LoadedElementDirectory(9999);
                Microsoft.VS.Modeling.IMS.WorkingStoreProxy proxy = new Microsoft.VS.Modeling.IMS.WorkingStoreProxy();
                Microsoft.VS.Modeling.IMS.ElementClassFactory factory = new Microsoft.VS.Modeling.IMS.ElementClassFactory(proxy, load);
                Microsoft.VS.Modeling.IMS.ElementDirectory directory = new Microsoft.VS.Modeling.IMS.ElementDirectory(load, factory);
                //Microsoft.VS.Modeling.IMS.Element element = new Microsoft.VS.Modeling.IMS.Element(
                //directory has nothing
                //factory relies on Store
                //LoadedElementDirectory relies on Element - Element relies on Store & propbag too
               System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFile(@"C:\Program Files\Microsoft BizTalk Server 2006\SDK\Samples\Orchestrations\CallOrchestration\bin\Development\CallOrchestration.dll");

                //Microsoft.VS.Modeling.IMS.Model model = new Microsoft.VS.Modeling.IMS.Model(
                //Microsoft.BizTalk.ObjectModel.Module _mod = new Microsoft.BizTalk.ObjectModel.Module(

                //if (null != b.ServiceDeclaration && null != b.ServiceDeclaration.ServiceBody && null != b.ServiceDeclaration.ServiceBody.TimeoutExpression)
                  //  Debugger.Break();

                //dispose
                b = null;
                
            }
            
            Console.WriteLine("working directory: " + di.FullName);           

            Console.WriteLine("press <Enter> to exit...");
            Console.ReadLine();
        }

        private string RunTime()
        {
            //+ "." + DateTime.Now.Ticks.ToString()
            return DateTime.Now.Second.ToString() + "." + DateTime.Now.Millisecond.ToString() ;
        }
    }
}
