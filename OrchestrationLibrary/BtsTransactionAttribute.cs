using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    public  class BtsTransactionAttribute: BtsBaseComponent
    {
        private bool _batch;
        private bool _retry;
        private int _timeout;
        private IsolationType _isolation;
        public BtsTransactionAttribute (XmlReader reader)
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
                        if (valName.Equals ("Batch"))
                            _batch = Convert.ToBoolean (val);
                        else if (valName.Equals ("Retry"))
                            _retry = Convert.ToBoolean (val);
                        else if (valName.Equals ("Timeout"))
                            _timeout = Convert.ToInt32 (val);
                        else if (valName.Equals ("Isolation"))
                            _isolation = base.GetIsolation (val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsTransactionAttribute.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[BtsTransactionAttribute.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }
            }
            reader.Close ();
        }
        
        public IsolationType IsolationType
        {
            get { return _isolation; }
        }

        public int Timeout
        {
            get { return _timeout; }
        }

        public bool Retry
        {
            get { return _retry; }
        }

        public bool Batch
        {
            get { return _batch; }
        }

    }

    public class BtsTargetXmlAttribute : BtsBaseComponent
    {
        private string _target;

        public BtsTargetXmlAttribute(XmlReader reader)
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
                        if (valName.Equals("TargetXMLNamespace"))
                            _target = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine("[BtsTargetXmlAttribute.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsTargetXmlAttribute.ctor] unhandled element " + reader.GetAttribute("Value"));
                    Debugger.Break();
                }
            }
            reader.Close();
        }
        
        public string TargetXMLNamespace
        {
            get { return _target; }
        }
	 
    }
}
