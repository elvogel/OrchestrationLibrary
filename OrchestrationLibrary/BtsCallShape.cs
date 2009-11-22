using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
    class BtsCallShape: BtsBaseComponent
    {
        /// <summary>
        /// Identifier
        /// </summary>
        private string _identifier;
        /// <summary>
        /// Invokee
        /// </summary>
        private string _invokee;
        /// <summary>
        /// Parameters
        /// </summary>
        private List<BtsParameter> _params = new List<BtsParameter> ();
        
        public BtsCallShape (XmlReader reader)
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
                        if (valName.Equals ("Identifier"))
                            _identifier = val;
                        else if (valName.Equals ("Invokee"))
                            _invokee = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;

                        else
                        {
                            Debug.WriteLine ("[BtsCallShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    if (reader.GetAttribute ("Type").Equals ("Parameter"))
                        _params.Add (new BtsParameter (reader.ReadSubtree ()));
                    else
                    {
                        Debug.WriteLine ("[BtsCallShape.ctor] unhandled element " + reader.GetAttribute ("Value"));
                        Debugger.Break ();
                    }
                }
            }
            reader.Close ();
        }
        
        public List<BtsParameter> Parameters
        {
            get { return _params; }
        }

        public string Invokee
        {
            get { return _invokee; }
        }

        public string Identifier
        {
            get { return _identifier; }
        }
	 
    }

    public class BtsParameter : BtsBaseComponent
    {

        private MessageDirection _direction;
        private string _type;
        public BtsParameter (XmlReader reader)
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
                        if (valName.Equals ("Direction"))
                            _direction = base.GetMessageDirection (val);
                        else if (valName.Equals ("Type"))
                            _type = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsParameter.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[BtsParameter.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }
            }
            reader.Close ();
        }
        
        public MessageDirection Direction
        {
            get { return _direction; }
        }
        public string ParameterType
        {
            get { return _type; }
        }
    }
}
