using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// generic container for extracting 
    /// </summary>
    public class BtsOrchVariable : BtsBaseComponent, IBtsOrchVariable
    {
        private MessageDirection _direction;
        private string _type;
        private string _initVal;
        private bool    _ctor;


        public BtsOrchVariable (XmlReader reader)
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
                        if (valName.Equals ("Type"))
                            _type = val;
                        else if (valName.Equals ("InitialValue"))
                            _initVal = val;
                        else if (valName.Equals ("UseDefaultConstructor"))
                            _ctor = Convert.ToBoolean (val);
                        else if (valName.Equals ("ParamDirection"))
                            _direction = base.GetMessageDirection (val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsOrchVariable.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[BtsOrchVariable.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }
            }
            reader.Close ();
        }
									 	
        public string InitialValue
        {
            get { return _initVal; }
        }

        public MessageDirection ParamDirection
        {
            get { return _direction; }
        }

        public string TypeName
        {
            get { return _type; }
        }
	
    }
}
