using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// ServiceLink - ref for ServiceLinkDeclaration (ports?) in orchestration
    /// ServiceDeclaration_ServiceLinkDeclaration (parentlink)
    /// </summary>
    class BtsServiceLinkDeclaration: BtsBaseComponent,IBtsBaseComponent
    {

#region vars
        /// <summary>
        /// Orientation
        /// </summary>
        private string  _orientation;
        /// <summary>
        /// PortIndex
        /// </summary>
        private short     _portIdx;
        /// <summary>
        /// PortModifier
        /// </summary>
        private string  _portModifier;
        /// <summary>
        /// RoleName
        /// </summary>
        private string  _roleName;
        /// <summary>
        /// OrderedDelivery
        /// </summary>
        private bool    _ordered;
        /// <summary>
        /// DeliveryNotification
        /// </summary>
        private string _notification;

        private string _type;

        private MessageDirection _direction;
#endregion

        public BtsServiceLinkDeclaration (XmlReader reader)
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
                        if (valName.Equals ("Orientation"))
                            _orientation = val;
                        else if (valName.Equals ("RoleName"))
                            _roleName = val;
                        else if (valName.Equals ("PortIndex"))
                            _portIdx = Convert.ToInt16 (val);
                        else if (valName.Equals ("PortModifier"))
                            _portModifier = val;
                        else if (valName.Equals ("OrderedDelivery"))
                            _ordered = Convert.ToBoolean (val);
                        else if (valName.Equals("DeliveryNotification"))
                            _notification = val;
                        else if (valName.Equals("Type"))
                            _type = val;
                        else if (valName.Equals("ParamDirection"))
                            _direction = base.GetMessageDirection(val);
                        else if (valName.Equals("AnalystComments"))
                            _comments = val;
                        else
                        {
                            Debug.WriteLine ("[ServiceLink.ctor] unhandled property " + valName);
                            Debugger.Break ();
                        }
                    }
                }
                else if (reader.Name.Equals ("om:Element"))
                {
                    Debug.WriteLine ("[ServiceLink.ctor] unhandled element " + reader.GetAttribute ("Value"));
                    Debugger.Break ();
                }
            }
            reader.Close ();
        }


        public MessageDirection Direction
        {
            get { return _direction; }
        }

        public string Type
        {
            get { return _type; }
        }

        public string DeliveryNotification
        {
            get { return _notification; }
        }

        public bool IsOrdered
        {
            get { return _ordered; }
        }

        public string RoleName
        {
            get { return _roleName; }
        }

        public string PortModifier
        {
            get { return _portModifier; }
        }

        public short PortIndex
        {
            get { return _portIdx; }
        }

        public string Orientation
        {
            get { return _orientation; }
        }

    }
}
