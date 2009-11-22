using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
    class BtsStatementRefShape: BtsBaseComponent
    {
        /// <summary>
        /// Ref
        /// </summary>
        private Guid _ref;
        /// <summary>
        /// Initializes
        /// </summary>
        private bool _initializes;

        public BtsStatementRefShape (XmlReader reader)
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
                        if (valName.Equals ("Ref"))
                            _ref = new Guid(val);
                        else if (valName.Equals ("Initializes"))
                            _initializes = Convert.ToBoolean(val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsStatementRefShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[BtsStatementRefShape.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }
            }
            reader.Close ();
        }
        
        public bool Initializes
        {
            get { return _initializes; }
        }

        public Guid Ref
        {
            get { return _ref; }
        }

    }
}
