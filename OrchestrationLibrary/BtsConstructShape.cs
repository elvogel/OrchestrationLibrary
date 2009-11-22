using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using System.Xml;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsConstructShape : BtsBaseComponent, IBtsShape
    {
        private List<BtsMessageRef> _refs = new List<BtsMessageRef>();
        private List<BtsTransform> _transforms = new List<BtsTransform>();
        private List<BtsMsgAssignmentShape> _assignments = new List<BtsMsgAssignmentShape> ();

        public BtsConstructShape(XmlReader reader)
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
                        if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            //we're expecting all defaults, so anything else is a surprise...
                            Debug.WriteLine("[BtsConstructShape.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("Transform"))
                        this._transforms.Add(new BtsTransform(reader.ReadSubtree()));
                    else if (reader.GetAttribute("Type").Equals("MessageRef"))
                        this._refs.Add(new BtsMessageRef(reader.ReadSubtree()));
                    else if (reader.GetAttribute("Type").Equals("MessageAssignment"))
                        this._assignments.Add(new BtsMsgAssignmentShape(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsConstructShape.ctor] unhandled element " + reader.GetAttribute("Type"));
                        Debugger.Break();
                    }
                }
                else
                    continue;
            }
            reader.Close();
        }

        public List<BtsMsgAssignmentShape> MessageAssignments
        {
            get { return _assignments; }
        }
        
        public List<BtsTransform> Transforms
        {
            get { return _transforms; }
        }

	
        public List<BtsMessageRef> MessageReferences
        {
            get { return _refs; }
        }

    }
}
