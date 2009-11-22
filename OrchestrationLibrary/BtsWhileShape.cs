using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsWhileShape: BtsBaseComponent, IBtsShape
    {
        /// <summary>
        /// Expression
        /// </summary>
        private string _exp;
        private List<BtsBaseComponent> _comps = new List<BtsBaseComponent> ();

        public BtsWhileShape (XmlReader reader)
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
                            Debug.WriteLine ("[BtsWhileShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                    _comps.Add (BtsShapeFactory.CreateShape (reader.ReadSubtree ()));
            }
            reader.Close ();
        }

        
        public List<BtsBaseComponent> Components
        {
            get { return _comps; }
        }

        public string Expression
        {
            get { return _exp; }
        }

									 
    }
}
