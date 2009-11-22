using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsFilter: BtsBaseComponent, IBtsFilter
    {
        private string _lhs;
        private string _rhs;
        private string _grouping;
        private string _operator;
        
public BtsFilter(XmlReader reader) : base(reader)
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
                if (valName.Equals("LHS"))
                {
                    _lhs = val;
                }
                else if (valName.Equals("RHS"))
                    _rhs = val;
                else if (valName.Equals("Grouping"))
                    _grouping = val;
                else if (valName.Equals("Operator"))
                    _operator = val;
                else if (valName.Equals("Signal"))
                    _signal = Convert.ToBoolean(val);
                else if (valName.Equals("AnalystComments"))
                    _comments = val;
                else
                {
                    Debug.WriteLine("[BtsFilter.ctor] unhandled property " + valName);
                    Debugger.Break();
                }
            }
        }
        else if (reader.Name.Equals("om:Element"))
        {
            Debug.WriteLine("[BtsFilter.ctor] unhandled element " + reader.GetAttribute("Type"));
            Debugger.Break();
        }
        else
            continue;
    }
    reader.Close();
}

        public bool Signal
        {
            get { return _signal; }
        }

        public string Operator
        {
            get { return _operator; }
        }

        public string Grouping
        {
            get { return _grouping; }
        }

        public string RHS
        {
            get { return _rhs; }
        }

        public string LHS
        {
            get { return _lhs; }
        }


    }
}
