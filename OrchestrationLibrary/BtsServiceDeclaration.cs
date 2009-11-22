using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// BtsServiceDeclaration is one of the top child nodes of Module (our BtsServiceBody class) which contains BtsMessageDeclarations and BtsServiceBody, 
    /// the 'heart' of the orchestration. 
    /// NOTE: This object can be null in the event of trying to parse Reference orchestrations!
    /// </summary>
    public class BtsServiceDeclaration : BtsBaseComponent
    {
        /// <summary>
        /// InitializedTransactionType
        /// </summary>
        private bool _initTxType;
        /// <summary>
        /// IsInvokable
        /// </summary>
        private bool _invokable;
        /// <summary>
        /// TypeModifier
        /// </summary>
        private string _modifier;
        /// <summary>
        /// MessageDeclaration
        /// </summary>
        private List<BtsMessageDeclaration> _msgDecs = new List<BtsMessageDeclaration>();
        /// <summary>
        /// CorrelationDeclaration
        /// </summary>
        private List<BtsCorrelationDeclaration> _corrDecs = new List<BtsCorrelationDeclaration>();
        /// <summary>
        /// PortDeclaration
        /// </summary>
        private List<BtsPortDeclaration> _portDecs = new List<BtsPortDeclaration>();
        /// <summary>
        /// VariableDeclaration
        /// </summary>
        private List<BtsVariableDeclaration> _varDecs = new List<BtsVariableDeclaration> ();
        /// <summary>
        /// ServiceBody
        /// </summary>
        private BtsServiceBody _svcBody;
        /// <summary>
        /// LongRunningTransaction or AtomicTransaction  
        /// </summary>
        private BtsTx _tx;
        /// <summary>
        /// TransactionAttribute
        /// </summary>
        private BtsTransactionAttribute _txAtt;
        /// <summary>
        /// Compensation
        /// </summary>
        private BtsCompensation _comp;
        /// <summary>
        /// ServiceLinkDeclaration
        /// </summary>
        private BtsServiceLinkDeclaration _sld;
        /// <summary>
        /// TargetXMLNamespaceAttribute
        /// </summary>
        private BtsTargetXmlAttribute _target;

        public BtsServiceDeclaration(XmlReader reader)
            : base(reader)
        {            
            while (reader.Read())
            {
                if (!reader.HasAttributes)
                    continue;

                if (reader.Name.Equals("om:Property"))
                    this.GetReaderProperties(reader.GetAttribute("Name"), reader.GetAttribute("Value"));
                else if (reader.Name.Equals("om:Element"))
                {
                    string attr = reader.GetAttribute("Type");
                    if (attr.Equals("ServiceBody"))
                        _svcBody = new BtsServiceBody(reader.ReadSubtree());
                    else if (attr.Equals("PortDeclaration"))
                        _portDecs.Add(new BtsPortDeclaration(reader.ReadSubtree()));
                    else if (attr.Equals("CorrelationDeclaration"))
                        _corrDecs.Add(new BtsCorrelationDeclaration(reader.ReadSubtree()));
                    else if (attr.Equals("MessageDeclaration"))
                        _msgDecs.Add(new BtsMessageDeclaration(reader.ReadSubtree()));
                    else if (attr.Equals("VariableDeclaration"))
                        _varDecs.Add(new BtsVariableDeclaration(reader.ReadSubtree()));
                    else if (attr.Equals("LongRunningTransaction"))
                        _tx = new BtsLongRunningTx(reader.ReadSubtree());
                    else if (attr.Equals("AtomicTransaction"))
                        _tx = new BtsAtomicTx(reader.ReadSubtree());
                    else if (attr.Equals("TransactionAttribute"))
                        _txAtt = new BtsTransactionAttribute(reader.ReadSubtree());
                    else if (attr.Equals("Compensation"))
                        _comp = new BtsCompensation(reader.ReadSubtree());
                    else if (attr.Equals("ServiceLinkDeclaration"))
                        _sld = new BtsServiceLinkDeclaration(reader.ReadSubtree());
                    else if (attr.Equals("TargetXMLNamespaceAttribute"))
                        _target = new BtsTargetXmlAttribute(reader.ReadSubtree());
                    else
                    {
                        Debug.WriteLine("[BtsServiceDeclaration.ctor] unhandled element " + reader.GetAttribute("Type") + "!!!!");
                        Debugger.Break();
                    }
                }
            }
            reader.Close();            
        }

        internal new void GetReaderProperties(string xmlName, string xmlValue)
        {
            if (!base.GetReaderProperties(xmlName, xmlValue))
            {
                if (xmlName.Equals("InitializedTransactionType"))
                    _initTxType = Convert.ToBoolean(xmlValue);
                else if (xmlName.Equals("IsInvokable"))
                    _invokable = Convert.ToBoolean(xmlValue);
                else if (xmlName.Equals("TypeModifier"))
                    _modifier = xmlValue;
                else if (xmlName.Equals("AnalystComments"))
                    _comments = xmlValue;
                else
                    Debug.WriteLine("[BtsServiceDeclaration.GetReaderProperties] TODO: implement handler for " + xmlName);
            }
        }
        
        public BtsServiceBody ServiceBody
        {
            get { return _svcBody; }
        }
	

        public BtsCompensation Compensation
        {
            get { return _comp;}
        }
        public BtsTransactionAttribute TransactionAttribute
        {
            get { return _txAtt; }
        }

        public BtsTx Transaction
        {
            get { return _tx; }
        }

        public List<BtsVariableDeclaration> VariableDeclarations
        {
            get { return _varDecs; }
        }
 
        public List<BtsPortDeclaration> PortDeclarations
        {
            get { return _portDecs; }
        }
 
        public List<BtsCorrelationDeclaration> CorrelationDeclarations
        {
            get { return _corrDecs; }
        }

        public List<BtsMessageDeclaration> MessageDeclarations
        {
            get { return _msgDecs; }
        }

        public string TypeModifier
        {
            get { return _modifier; }
        }

    }
}
