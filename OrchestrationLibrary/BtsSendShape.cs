using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    class BtsSendShape: BtsBaseComponent
    {
        private string _portName;
        private string _msgName;
        private string _opName;
        private string _opMsgName;
        private string _svcLinkName;
        private string _svcLinkPortTypeName;
        private string _svcLinkRoleName;

        public BtsSendShape (XmlReader reader)
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
                        if (valName.Equals ("PortName"))
                            _portName = val;
                        else if (valName.Equals ("MessageName"))
                            _msgName = val;
                        else if (valName.Equals ("OperationName"))
                            _opName = val;
                        else if (valName.Equals ("OperationMessageName"))
                            _opMsgName = val;
                        else if (valName.Equals("ServiceLinkName"))
                            _svcLinkName = val;
                        else if (valName.Equals("ServiceLinkPortTypeName"))
                            _svcLinkPortTypeName = val;
                        else if (valName.Equals("ServiceLinkRoleName"))
                            _svcLinkRoleName = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsSendShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[BtsSendShape.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }                
            }
            reader.Close ();
        }

        
        public string ServiceLinkRoleName
        {
            get { return _svcLinkRoleName; }
        }

        public string ServiceLinkPortTypeName
        {
            get { return _svcLinkPortTypeName; }
        }

        public string ServiceLinkName
        {
            get { return _svcLinkName; }
        }

        public string OperationMessageName
        {
            get { return _opMsgName; }
        }

        public string OperationName
        {
            get { return _opName; }
        }

        public string MessageName
        {
            get { return _msgName; }
        }

        public string PortName
        {
            get { return _portName; }
        }

    }
}
