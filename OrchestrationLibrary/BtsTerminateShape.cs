using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsTerminateShape: BtsBaseComponent
    {
        private string _err;
        public BtsTerminateShape (XmlReader reader)
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
                        if (valName.Equals ("ErrorMessage"))
                            _err = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsTerminateShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[BtsTerminateShape.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }
            }
            reader.Close ();
        }
        
        public string ErrorMessage
        {
            get { return _err; }
        }

									 
    }
}
