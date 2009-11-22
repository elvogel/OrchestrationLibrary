using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsTx: BtsBaseComponent
    {
        internal string _timeoutExpr;
        
        public BtsTx(XmlReader reader)
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
                        if (valName.Equals("TimeoutExpression"))
                            _timeoutExpr = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine("[BtsTx.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsTx.ctor] unhandled element " + reader.GetAttribute("Value"));
                    Debugger.Break();
                }
            }
            reader.Close();
        }
        
        public string TimeoutExpression
        {
            get { return _timeoutExpr; }
        }

    }

    public class BtsAtomicTx : BtsTx
    {
        //nothing to do here
        public BtsAtomicTx (XmlReader reader) : base (reader) { }
    }

    public class BtsLongRunningTx : BtsTx
    {
        //nothing to do here either - let the base take care of it
        public BtsLongRunningTx (XmlReader reader) : base (reader) { }
    }
}
