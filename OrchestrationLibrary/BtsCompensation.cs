using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
    ///TODO: double-check BtsCompensation object to see if it can hold Components or limited set of shapes.
    /// <summary>
    /// custom compensation shape in orchestration.
    /// </summary>
    public class BtsCompensation: BtsBaseComponent
    {
        /// <summary>
        /// IsCustom
        /// </summary>
        private bool _isCustom;

        private List<BtsBaseComponent> _components = new List<BtsBaseComponent>();

        public BtsCompensation (XmlReader reader)
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
                        if (valName.Equals ("IsCustom"))
                            _isCustom = Convert.ToBoolean (val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;                 
                        else
                        {
                            Debug.WriteLine ("[BtsCompensationShape.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                    _components.Add(BtsShapeFactory.CreateShape(reader.ReadSubtree()));
            }
            reader.Close ();
        }
        
        public List<BtsBaseComponent> Components
        {
            get { return _components; }
        }
    }

    public class BtsCompensateShape : BtsBaseComponent
    {
        private string _invokee;
        public BtsCompensateShape(XmlReader reader)
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
                        if (valName.Equals("Invokee"))
                            _invokee = val;
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine("[BtsCompensateShape.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsCompensateShape.ctor] unhandled element " + reader.GetAttribute("Value"));
                    Debugger.Break();
                }
            }
            reader.Close();
        }
        
        public string Invokee
        {
            get { return _invokee; }
        }

    }
}
