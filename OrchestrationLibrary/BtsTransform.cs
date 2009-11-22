using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsTransform : BtsBaseComponent, IBtsShape
    {
        private string _className;
        private List<BtsMessagePartRef> _refs = new List<BtsMessagePartRef>(); //temp - delete
        public BtsTransform(XmlReader reader)
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
                        if (valName.Equals("ClassName"))
                            _className = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine("[BtsTransform.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("MessagePartRef"))
                        _refs.Add(new BtsMessagePartRef(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsTransform.ctor] unhandled element " + reader.GetAttribute("Type"));
                        Debugger.Break();
                    }
                }
                else
                    continue;
            }
            reader.Close();
        }

        
        public string  ClassName
        {
            get { return _className; }
        }

    }
}
