using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using System.Xml;

namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsTreeBranch: BtsBaseComponent
    {

        private bool _ghostBranch;
        private string _expression = String.Empty;
        private List<BtsBaseComponent> _shapes = new List<BtsBaseComponent> ();
        
        public BtsTreeBranch (XmlReader reader)
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
                        if (valName.Equals ("IsGhostBranch"))
                            _ghostBranch = Convert.ToBoolean (val);
                        else if (valName.Equals ("Expression"))
                            _expression = val;
                        else if (valName.Equals ("Name"))
                            _name = val;
                        else
                        {
                            Debug.WriteLine ("[BtsDecisionBranchShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                    _shapes.Add (BtsShapeFactory.CreateShape (reader.ReadSubtree ()));
            }
            reader.Close ();
        }

        
        public List<BtsBaseComponent> Shapes
        {
            get { return _shapes; }
        }

        public bool GhostBranch
        {
            get { return _ghostBranch; }
        }

        public string Expression
        {
            get { return _expression; }
        }
	
    }

    public class BtsDecisionBranchShape : BtsTreeBranch
    {
        public BtsDecisionBranchShape (XmlReader reader) : base (reader) { }
    }

    public class BtsListenBranchShape : BtsTreeBranch
    {
        public BtsListenBranchShape (XmlReader reader) : base (reader) { }
    }

    public class BtsParallelBranchShape : BtsTreeBranch
    {
        public BtsParallelBranchShape(XmlReader reader) : base(reader) { }
    }

}
