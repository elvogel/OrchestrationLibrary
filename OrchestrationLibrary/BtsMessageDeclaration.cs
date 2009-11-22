using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// BtsMessageDeclaration is a child of BtsServiceDeclaration and is used to declare message types
    /// </summary>
    public class BtsMessageDeclaration: BtsBaseComponent, IBtsMessageDeclaration
    {
        private MessageDirection _paramDirection;
        private string _type;
        public BtsMessageDeclaration(XmlReader reader)
            : base(reader)
        {
            while (reader.Read())
            {                
                if (!reader.HasAttributes)
                    break;
                if (reader.Name.Equals("om:Property"))
                {
                    if (!base.GetReaderProperties(reader.GetAttribute("Name"), reader.GetAttribute("Value")))
                    {
                        if (reader.GetAttribute("Name").Equals("ParamDirection"))
                            this._paramDirection = base.GetMessageDirection(reader.GetAttribute("Value"));
                        else if (reader.GetAttribute("Name").Equals("Type"))
                            this._type = reader.GetAttribute("Value");
                        else if (reader.GetAttribute("Name").Equals("AnalystComments"))
                            _comments = reader.GetAttribute("Value");
                        else
                        {
                            Debug.WriteLine("[BtsMessageDeclaration.ctor] unhandled property " + reader.GetAttribute("Name"));
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsMessageDeclaration.ctor] unhandled element " + reader.GetAttribute("Type") + " not supported in class! (TODO)");
                }

            }
            reader.Close();
        }

        public string Type { get { return _type; } }
        public MessageDirection ParamDirection
        {
            get { return _paramDirection; }
        }

    }
}
