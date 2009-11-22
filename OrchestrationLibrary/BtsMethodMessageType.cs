using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsMethodMessageType: BtsBaseComponent
    {
        private string _url;
        private string _modifier;
        private List<BtsMethodMessageOperation> _msgOps = new List<BtsMethodMessageOperation>();
        public BtsMethodMessageType(XmlReader reader)
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
                        if (valName.Equals("Url"))
                            _url = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else if (valName.Equals("Name"))
                            _name = val;
                        else if (valName.Equals("Signal"))
                            _signal = Convert.ToBoolean(val);
                        else if (valName.Equals("TypeModifier"))
                            _modifier = val;
                        else
                        {
                            Debug.WriteLine("[BtsMethodMessageType.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("MethodMessageOperation"))
                        _msgOps.Add(new BtsMethodMessageOperation(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsMethodMessageType.ctor] unhandled element " + reader.GetAttribute("Value"));
                        Debugger.Break();
                    }
                }
            }
            reader.Close();
        }

        public string TypeModifier
        {
            get { return _modifier; }
        }
    }

    public class BtsMethodMessageOperation : BtsBaseComponent
    {
        private string _opName;
        private string _opDirection;
        private List<BtsWebOperationPart> _parts = new List<BtsWebOperationPart>();
        public BtsMethodMessageOperation(XmlReader reader)
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
                        if (valName.Equals("OperationName"))
                            _opName = val;
                        else if (valName.Equals("OperationDirection"))
                            _opDirection = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else if (valName.Equals("Name"))
                            _name = val;
                        else if (valName.Equals("Signal"))
                            _signal = Convert.ToBoolean(val);
                        else
                        {
                            Debug.WriteLine("[BtsMethodMessageOperation.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("WebOperationPart"))
                        _parts.Add(new BtsWebOperationPart(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsMethodMessageOperation.ctor] unhandled element " + reader.GetAttribute("Value"));
                        Debugger.Break();
                    }
                }
            }
            reader.Close();
        }									 
    }

    public class BtsWebOperationPart : BtsBaseComponent
    {
        private string _clsName;
        public BtsWebOperationPart(XmlReader reader)
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
                            _clsName = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else if (valName.Equals("Name"))
                            _name = val;
                        else if (valName.Equals("Signal"))
                            _signal = Convert.ToBoolean(val);
                        else
                        {
                            Debug.WriteLine("[BtsWebOperationPart.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsWebOperationPart.ctor] unhandled element " + reader.GetAttribute("Value"));
                    Debugger.Break();
                }
            }
            reader.Close();
        }									 
    }
}
