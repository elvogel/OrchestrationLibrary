using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsVariableAssignmentShape: BtsBaseComponent
    {
        /// <summary>
        /// Expression
        /// </summary>
        private string _exp;

        public BtsVariableAssignmentShape (XmlReader reader)
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
                        if (valName.Equals ("Expression"))
                            _exp = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsVariableAssignmentShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[BtsVariableAssignmentShape.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }
            }
            reader.Close ();
        }
        
        public string Expression
        {
            get { return _exp; }
        }
						 
    }
}
