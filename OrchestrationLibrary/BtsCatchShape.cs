using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsCatchShape : BtsBaseComponent
    {
        /// <summary>
        /// ExceptionName
        /// </summary>
        private string _excName;
        /// <summary>
        /// ExceptionType
        /// </summary>
        private string _excType;
        /// <summary>
        /// IsFaultMessage
        /// </summary>
        private bool _fault;

        private List<BtsBaseComponent> _comps = new List<BtsBaseComponent> ();

        public BtsCatchShape (XmlReader reader)
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
                        if (valName.Equals ("ExceptionName"))
                            _excName = val;
                        else if (valName.Equals ("ExceptionType"))
                            _excType = val;
                        else if (valName.Equals ("IsFaultMessage"))
                            _fault = Convert.ToBoolean (val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[BtsCatchShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                    _comps.Add (BtsShapeFactory.CreateShape (reader.ReadSubtree ()));                
            }
            reader.Close ();
        }

        public List<BtsBaseComponent> Components
        {
            get { return _comps; }        
        }

        public bool IsFaultMessage
        {
            get { return _fault; }
        }

        public string ExceptionType
        {
            get { return _excType; }
        }

        public string ExceptionName
        {
            get { return _excName; }
        }

    }
}
