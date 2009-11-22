using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
    class BtsThrowShape: BtsBaseComponent
    {
        private string _throwRef;

        public BtsThrowShape (XmlReader reader)
            : base (reader)
        {
            while (reader.Read ())
            {
                if (!reader.HasAttributes)
                    break;
                else if (reader.Name.Equals ("om:Property"))
                {
                    string valName = reader.GetAttribute ("Name");
                    string val = reader.GetAttribute ("Value");
                    if (!base.GetReaderProperties (valName, val))
                    {
                        if (valName.Equals ("ThrownReference"))
                            _throwRef = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsThrowShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[BtsThrowShape.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }
            }
            reader.Close ();
        }        
        public string ThrownReference
        {
            get { return _throwRef; }
        }

									 
    }
}
