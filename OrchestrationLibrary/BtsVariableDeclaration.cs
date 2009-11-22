using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsVariableDeclaration: BtsBaseComponent
    {
        /// <summary>
        /// use default constructor
        /// </summary>
        private bool _dtor;
        private string _type;
        private string _initVal;
        private MessageDirection _paramDir;

        public BtsVariableDeclaration (XmlReader reader)
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
                        if (valName.Equals ("UseDefaultConstructor"))
                            _dtor = Convert.ToBoolean (val);
                        else if (valName.Equals ("Type"))
                            _type = val;
                        else if (valName.Equals ("ParamDirection"))
                            _paramDir = base.GetMessageDirection (val);
                        else if (valName.Equals ("InitialValue"))
                            _initVal = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsVariableDeclaration.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                    else if (reader.Name.Equals ("om:Element"))
                    {
                        Debug.WriteLine ("[BtsVariableDeclaration.ctor] unhandled element " + reader.GetAttribute ("Value"));
                        Debugger.Break ();
                    }
                }
            }
            reader.Close ();
        }

        public string InitialValue
        {
            get { return _initVal; }
        }

        public MessageDirection ParameterDirection
        {
            get { return _paramDir; }
        }

        public string VariableType
        {
            get { return _type; }
        }

        public bool UseDefaultConstructor
        {
            get { return _dtor; }
        }
						 
    }
}
