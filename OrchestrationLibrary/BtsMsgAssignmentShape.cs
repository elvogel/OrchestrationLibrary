using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using System.Xml;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsMsgAssignmentShape: BtsBaseComponent, IBtsShape
    {
        private string _expression = String.Empty;
        
        public string Expression
        {
            get { return _expression; }
        }

        public BtsMsgAssignmentShape(XmlReader reader)
            : base(reader)
        {
            while (reader.Read())
            {
                if (!reader.HasAttributes)
                    break;
                else if (reader.Name.Equals("om:Property"))
                {
                    string valName = reader.GetAttribute("Name");
                    string val = reader.GetAttribute("Value");
                    if (!base.GetReaderProperties(valName, val))
                    {
                        if (valName.Equals("Expression"))
                            _expression = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine("[BtsMsgAssignmentShape.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsMsgAssignmentShape.ctor] unhandled element " + reader.GetAttribute("Type"));
                    Debugger.Break();
                }
                else
                    continue;
            }
            reader.Close();
        }
    }
}
