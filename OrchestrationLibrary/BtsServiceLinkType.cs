using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsServiceLinkType: BtsBaseComponent
    {
        /// <summary>
        /// TypeModifier
        /// </summary>
        private string _modifier;
        private List<BtsRoleDeclaration> _roleDecs = new List<BtsRoleDeclaration>();
        public BtsServiceLinkType(XmlReader reader)
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
                        if (valName.Equals("Name"))
                            _name = val;
                        else if (valName.Equals("Signal"))
                            _signal = Convert.ToBoolean(val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else if (valName.Equals("TypeModifier"))
                            _modifier = val;
                        else
                        {
                            Debug.WriteLine("[BtsServiceLinkType.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("RoleDeclaration"))
                        _roleDecs.Add(new BtsRoleDeclaration(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsServiceLinkType.ctor] unhandled element " + reader.GetAttribute("Value"));
                        Debugger.Break();
                    }
                }
            }
            reader.Close();
        }      									 
    }

    public class BtsRoleDeclaration : BtsBaseComponent
    {
        private List<BtsPortTypeRef> _portRefs = new List<BtsPortTypeRef>();

        public BtsRoleDeclaration(XmlReader reader)
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
                        if (valName.Equals("Name"))
                            _name = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else if (valName.Equals("Signal"))
                            _signal = Convert.ToBoolean(val);
                        else
                        {
                            Debug.WriteLine("[BtsRoleDeclaration.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("PortTypeRef"))
                        _portRefs.Add(new BtsPortTypeRef(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsRoleDeclaration.ctor] unhandled element " + reader.GetAttribute("Value"));
                        Debugger.Break();
                    }
                }
            }
            reader.Close();
        }

        public List<BtsPortTypeRef> PortTypeRefs
        {
            get { return _portRefs; }
        }

									 
    }

    public class BtsPortTypeRef : BtsBaseComponent
    {
        private string _ref;
        public BtsPortTypeRef(XmlReader reader)
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
                        if (valName.Equals("Ref"))
                            _ref = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else if (valName.Equals("Name"))
                            _name = val;
                        else if (valName.Equals("Signal"))
                            _signal = Convert.ToBoolean(val);
                        else
                        {
                            Debug.WriteLine("[BtsPortTypeRef.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsPortTypeRef.ctor] unhandled element " + reader.GetAttribute("Value"));
                    Debugger.Break();
                }
            }
            reader.Close();
        }

        public string Ref
        {
            get { return _ref; }
        }
	
									 
    }
}
