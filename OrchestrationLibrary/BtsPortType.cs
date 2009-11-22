using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// PortType is a top-level child of an orchestration. It contains an OperationDeclaration, which in turn contains a MessageRef
    /// </summary>
    class BtsPortType: BtsBaseComponent, IBtsPortType
    {
        /// <summary>
        /// Synchronous
        /// </summary>
        private bool _sync = false;
        /// <summary>
        /// TypeModifier (NOTE: this property is used by orchestration as well - if multiple others, we need to move
        /// </summary>
        private string _mod = String.Empty;
        /// <summary>
        /// OperationDeclaration found within PortType
        /// </summary>
        private List<BtsOperationDeclaration> _opDecs = new List<BtsOperationDeclaration>();

        public BtsPortType(XmlReader reader)
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
                        if (valName.Equals("Synchronous"))
                            this._sync = Convert.ToBoolean(val);
                        else if (valName.Equals("TypeModifier"))
                            this._mod = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine("[BtsPortType.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("OperationDeclaration"))
                        this._opDecs.Add(new BtsOperationDeclaration(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsPortType.ctor] unhandled element " + reader.GetAttribute("Value"));
                        Debugger.Break();
                    }
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
        public string Modifier
        {
            get { return _mod; }
        }
        public bool Synchronous
        {
            get { return _sync; }
        }
        
        public List<BtsOperationDeclaration> OperationDeclarations
        {
            get { return _opDecs; }
        }
	

    } //BtsPortType

    /// <summary>
    /// BtsOperationDeclaration is a single-child of BtsPortType.
    /// </summary>
    public class BtsOperationDeclaration : BtsBaseComponent, IBtsOperationDeclaration
    {
        private OperationType _opType;
        private List<BtsMessageRef> _msgRefs = new List<BtsMessageRef>();

        public BtsOperationDeclaration(XmlReader reader)
            : base(reader)
        {
            while (reader.Read())
            {
                if (!reader.HasAttributes)
                    break;
                else if (reader.Name.Equals("om:Property"))
                {
                    if (!base.GetReaderProperties(reader.GetAttribute("Name"),reader.GetAttribute("Value")))
                    {
                        if (reader.GetAttribute("Name").Equals("OperationType"))
                            this._opType = this.DetermineOpType(reader.GetAttribute("Value"));
                        else
                        {
                            Debug.WriteLine("[BtsOperationDeclaration.ctor] unhandled property " + reader.GetAttribute("Name"));
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("MessageRef"))
                        _msgRefs.Add(new BtsMessageRef(reader.ReadSubtree()));
                }
                else
                    continue;
            }
            reader.Close();
        }

        private OperationType DetermineOpType(string opType)
        {
            Debug.WriteLine("[BtsPortType.DetermineOpType] Operation Type: " + opType);

            if (opType.Equals("OneWay"))
                return OperationType.OneWay;
            else if (opType.Equals("RequestResponse"))
                return OperationType.RequestResponse;
            else
            {
                System.Diagnostics.Debug.WriteLine("ERROR! OperationType " + opType + " not supported by OperationType enum, and needs to be added!!");
#if DEBUG
                System.Diagnostics.Debug.Fail("ERROR! OperationType " + opType + " not supported by OperationType enum, and needs to be added!!");
#endif
                return OperationType.None;
            }
        }

        public OperationType OperationType
        {
            get { return _opType; }
        }
        
        public bool Signal
        {
            get { return _signal; }
        }
        
        public List<BtsMessageRef> MessageRef
        {
            get { return _msgRefs; }
        }
	

    }//BtsOperationDeclaration

    /// <summary>
    /// BtsMessageRef is a single child of BtsOperationDeclaration.
    /// </summary>
    public class BtsMessageRef: BtsBaseComponent, IBtsMessageRef
	{
        private string _ref;


        public BtsMessageRef(XmlReader reader)
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
                        else
                        {
                            Debug.WriteLine("[BtsMessageRef.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsMessageRef.ctor] unhandled element " + reader.GetAttribute("Type"));
                    Debugger.Break();
                }
                else
                    continue;
            }
            reader.Close();
        }
        
        public string Ref
        {
            get { return _ref; }
        }
        
        public bool Signal
        {
            get { return _signal; }
        }
	}

    public class BtsMessagePartRef : BtsBaseComponent, IBtsMessagePartRef
    {
        private string _messageRef;
        private string _partRef;

        public BtsMessagePartRef(XmlReader reader)
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
                        if (valName.Equals("MessageRef"))
                            _messageRef = val;
                        else if (valName.Equals("PartRef"))
                            _partRef = val;
                        else
                        {
                            Debug.WriteLine("[BtsMessagePartRef.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsMessagePartRef.ctor] unhandled element " + reader.GetAttribute("Type"));
                    Debugger.Break();
                }
                else
                    continue;
            }
            reader.Close();
        }
    }
}