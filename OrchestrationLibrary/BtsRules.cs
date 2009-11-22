using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsCallRulesShape: BtsBaseComponent    
    {
        private string _policyName;       
        private short _policyVersion;     
        private List<BtsRulesParameterRef> _params = new List<BtsRulesParameterRef>();
        public BtsCallRulesShape(XmlReader reader)
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
                        if (valName.Equals("PolicyName"))
                            _policyName = val;
                        else if (valName.Equals("PolicyVersion"))
                            _policyVersion = Convert.ToInt16(val);
                        else if (valName.Equals("Name"))
                            _name = val;
                        else if (valName.Equals("Signal"))
                            _signal = Convert.ToBoolean(val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine("[BtsCallRulesShape.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("RulesParameterRef"))
                        _params.Add(new BtsRulesParameterRef(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsCallRulesShape.ctor] unhandled element " + reader.GetAttribute("Value"));
                        Debugger.Break();
                    }
                }
            }
            reader.Close();
        }
        
        public short PolicyVersion
        {
            get { return _policyVersion; }
        }

        public string PolicyName
        {
            get { return _policyName; }
        }
    }

    public class BtsRulesParameterRef : BtsBaseComponent
    {
        private string _ref;
        private string _alias;

        public BtsRulesParameterRef(XmlReader reader)
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
                        if (valName.Equals("Reference"))
                            _ref = val;
                        else if (valName.Equals("Alias"))
                            _alias = val;
                        else
                        {
                            Debug.WriteLine("[BtsRulesParameterRef.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsRulesParameterRef.ctor] unhandled element " + reader.GetAttribute("Value"));
                    Debugger.Break();
                }
            }
            reader.Close();
        }
	        
        public string Alias
        {
            get { return _alias; }
        }

        public string Reference
        {
            get { return _ref; }
        }

									 
    }
}
