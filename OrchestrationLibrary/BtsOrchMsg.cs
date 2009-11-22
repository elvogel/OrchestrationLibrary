using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsOrchMsg: BtsBaseComponent, IBtsOrchMsg
    {
        private MessageDirection _direction;

        public BtsOrchMsg(XmlReader reader)
            : base(reader)
        {
            while (reader.Read())
            {
                if (!reader.HasAttributes)
                    continue;
                else if (reader.Name.Equals ("om:Property"))
                {
                    string valName = reader.GetAttribute ("Name");
                    string val = reader.GetAttribute ("Value");
                    if (!base.GetReaderProperties (valName, val))
                    {
                        if (valName.Equals ("MessageDirection"))
                            _direction = base.GetMessageDirection (val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsOrchMsg.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else
                {
                    Debug.WriteLine ("[BtsOrchMsg.ctor] unhandled: " + reader.GetAttribute(0));
                    Debugger.Break ();
                }
            }
            reader.Close();
        }
        
        public MessageDirection MessageDirection
        {
            get { return _direction; }
        }

    }
}
