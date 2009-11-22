using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using System.Xml;
using System.Reflection;
using System.IO;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsReceiveShape : BtsBaseComponent, IBtsShape
    {
        #region vars
        private bool _activate;
        private string _msgName;

        private string _operationName;
        private string _operationMsgName;
        
        private string _svcLinkName;
        private string _svcLinkPortTypeName;
        private string _svcLinkRoleName;
        private string _portName;

        private List<BtsFilter> _predicates = new List<BtsFilter> ();
        #endregion

        public BtsReceiveShape (XmlReader reader)
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
                        if (valName.Equals ("Activate"))
                            _activate = Convert.ToBoolean (val);
                        else if (valName.Equals ("OperationMessageName"))
                            _operationMsgName = val;
                        else if (valName.Equals ("MessageName"))
                            _msgName = val;
                        else if (valName.Equals ("ServiceLinkName"))
                            _svcLinkName = val;
                        else if (valName.Equals ("ServiceLinkPortTypeName"))
                            _svcLinkPortTypeName = val;
                        else if (valName.Equals ("ServiceLinkRoleName"))
                            _svcLinkRoleName = val;
                        else if (valName.Equals ("PortName"))
                            _portName = val;
                        else if (valName.Equals ("OperationName"))
                            _operationName = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsReceiveShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    if (reader.GetAttribute ("Type").Equals ("DNFPredicate"))
                    {
                        _predicates.Add (new BtsFilter (reader.ReadSubtree ()));
                    }
                    else
                    {
                        Debug.WriteLine ("[BtsReceiveShape.ctor] unhandled element " + reader.GetAttribute ("Type"));
                        Debugger.Break ();
                    }
                }
            }
            reader.Close ();
        }

#region props
        
        public List<BtsFilter>  Filter
        {
            get { return _predicates; }
        }

        public string PortName
        {
            get { return _portName; }
        }

        public bool Activate
        {
            get { return _activate; }
        }
        public string MessageName
        {
            get { return _msgName; }
        }
        public string OperationName
        {
            get { return _operationName; }
        }

        public string OperationMessageName
        {
            get { return _operationMsgName; }
        }

        public string ServiceLinkName
        {
            get { return _svcLinkName; }
        }

        public string ServiceLinkPortTypeName
        {
            get { return _svcLinkPortTypeName; }
        }

        public string ServiceLinkRoleName
        {
            get { return _svcLinkRoleName; }
        }
#endregion
	
    }    
}
