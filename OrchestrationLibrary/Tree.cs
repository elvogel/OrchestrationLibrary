using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
//
using System.Xml;

namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsTree : BtsBaseComponent, IBtsShape
    {
        private List<BtsTreeBranch> _branches = new List<BtsTreeBranch> ();

        public BtsTree (XmlReader reader)
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
                        if (valName.Equals("Name"))
                            _name = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine("[BtsDecisionShape.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    if (reader.GetAttribute ("Type").Contains ("Branch"))
                        _branches.Add (new BtsTreeBranch(reader.ReadSubtree ()));
                    else
                    {
                        Debug.WriteLine ("[BtsDecisionShape.ctor] unhandled element " + reader.GetAttribute ("Value"));
                        Debugger.Break ();
                    }
                }
            }
            reader.Close ();
        }
        
        public List<BtsTreeBranch> Branches
        {
            get { return _branches; }
        }
    }

    public class BtsDecisionShape : BtsTree
    {
        public BtsDecisionShape (XmlReader reader) : base (reader) { }
    }

    public class BtsListenShape : BtsTree
    {
        public BtsListenShape (XmlReader reader) : base (reader) { }
    }

    public class BtsParallelShape : BtsTree
    {
        public BtsParallelShape(XmlReader reader) : base(reader) { }
    }
}