using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// BtsPortDeclaration is a child of BtsServiceDeclaration
    /// </summary>
    public class BtsPortDeclaration : BtsBaseComponent, IBtsPortDeclaration
    {
        private string _modifier;
        private string _orientation;
        private short _portIdx;
        private bool _webPort;
        private bool _orderedDelivery;
        private string _notification;
        private string _type;
        private MessageDirection _direction;
        private BtsBindingAttribute _binding;
        
        public BtsPortDeclaration(XmlReader reader): base(reader)
        {
            while (reader.Read())
            {
                if (!reader.HasAttributes)
                    break;

                int i = 0;
                if (reader.Name.Equals("om:Property"))
                    this.GetReaderProperties(reader.GetAttribute("Name"), reader.GetAttribute("Value"));
                else if (reader.AttributeCount != 0 && reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute ("Type").Equals ("LogicalBindingAttribute"))
                    {
                        if (i < 1)
                            _binding = new BtsLogicalBindingAttribute (reader.ReadSubtree ());
                        else
                            Debugger.Break (); //checking to make sure only 
                    }
                    else if (reader.GetAttribute ("Type").Equals ("DirectBindingAttribute"))
                    {
                        if (i < 1)
                            _binding = new BtsDirectBindingAttribute (reader.ReadSubtree ());
                        else
                            Debugger.Break ();
                    }
                    else if (reader.GetAttribute("Type").Equals("PhysicalBindingAttribute"))
                    {
                        if (i < 1)
                            _binding = new BtsPhysicalBindingAttribute(reader.ReadSubtree());
                        else
                            Debugger.Break();
                    }
                    else
                    {
                        Debug.WriteLine("[BtsPortDeclaration.ctor] unhandled element type: " + reader.GetAttribute("Type"));
                        Debugger.Break();
                    }
                }
            }
            reader.Close();
        }

        internal new void GetReaderProperties(string xmlName, string xmlValue)
        {

            if (!base.GetReaderProperties(xmlName, xmlValue))
            {

                if (xmlName.Equals("PortModifier"))
                    _modifier = xmlValue;
                else if (xmlName.Equals("Orientation"))
                    _orientation = xmlValue;
                else if (xmlName.Equals("PortIndex"))
                    _portIdx = Convert.ToInt16(xmlValue);
                else if (xmlName.Equals("IsWebPort"))
                    _webPort = Convert.ToBoolean(xmlValue);
                else if (xmlName.Equals("OrderedDelivery"))
                    _orderedDelivery = Convert.ToBoolean(xmlValue);
                else if (xmlName.Equals("DeliveryNotification"))
                    _notification = xmlValue;
                else if (xmlName.Equals("ParamDirection"))
                    _direction = base.GetMessageDirection(xmlValue);
                else if (xmlName.Equals("Type"))
                    _type = xmlValue;
                else if (xmlName.Equals("AnalystComments"))
                    _comments = xmlValue;
                else
                    Debug.WriteLine("[BtsPortDeclaration.GetReaderProperties] unhandled om:Property " + xmlName);
                return;
            }
        }
        
        public string Type
        {
            get { return _type; }
        }

        public BtsBindingAttribute BindingAttribute
        {
            get { return _binding; }
        }

        public string PortModifier
        {
            get { return _modifier; }
        }
        
        public MessageDirection ParamDirection
        {
            get { return _direction; }
        }

        public string DeliveryNotification
        {
            get { return _notification; }
        }

        public bool OrderedDelivery
        {
            get { return _orderedDelivery; }
        }

        public bool IsWebPort
        {
            get { return _webPort; }
        }

        public int PortIndex
        {
            get { return _portIdx; }
        }

        public string Orientation
        {
            get { return _orientation; }
        }
	

    }

    public class BtsBindingAttribute : BtsBaseComponent
    {
        internal BindingAttributeType _bindType;

        public BtsBindingAttribute (XmlReader reader) : base (reader) { }
        
        public BindingAttributeType BindingAttributeType
        {
            get { return _bindType; }
        }

        public bool Signal
        {
            get
            {
                return _signal;
            }
        }
    }

    public class BtsLogicalBindingAttribute : BtsBindingAttribute
    {
        public BtsLogicalBindingAttribute(XmlReader reader)
            : base(reader)
        {
            base._bindType = BindingAttributeType.Logical;
            //no, really - nothing to do here
            reader.Close();
        }

    }

    public class BtsDirectBindingAttribute: BtsBindingAttribute
    {
        private string _partnerPort;
        private string _partnerSvc;
        private string _dirBindType;

	  public BtsDirectBindingAttribute(XmlReader reader) : base(reader)
	  {
          _bindType = BindingAttributeType.Direct;

	   while (reader.Read())
		{
		 if (!reader.HasAttributes)
			break;
		 else if (reader.Name.Equals("om:Property"))
			{
		     string valName = reader.GetAttribute("Name");
		     string val = reader.GetAttribute("Value");
			if (!base.GetReaderProperties(valName,val))
			  {
                  if (valName.Equals ("PartnerPort"))
                      _partnerPort = val;
                  else if (valName.Equals ("PartnerService"))
                      _partnerSvc = val;
                  else if (valName.Equals ("DirectBindingType"))
                      _dirBindType = val;
                  else if (valName.Equals ("Signal"))
                      _signal = Convert.ToBoolean (val);
                  else
                  {
                      Debug.WriteLine ("[BtsDirectBindingAttribute.ctor] unhandled property " + valName);
                      Debugger.Break ();
                  }
			   }
			}
		else if (reader.Name.Equals("om:Element"))
			   {
				Debug.WriteLine("[BtsDirectBindingAttribute.ctor] unhandled element " + reader.GetAttribute("Value"));
				Debugger.Break();
				}			
		 }
		  reader.Close();
	   }


       public string DirectBindingType
       {
           get { return _dirBindType; }
       }

       public string PartnerService
       {
           get { return _partnerSvc; }
       }

       public string PartnerPort
       {
           get { return _partnerPort; }
       }      						 
    }

    public class BtsPhysicalBindingAttribute : BtsBindingAttribute
    {
        /// <summary>
        /// InPipeline
        /// </summary>
        private string _inPipe;
        /// <summary>
        /// OutPipeline
        /// </summary>
        private string _outPipe;
        /// <summary>
        /// TransportType
        /// </summary>
        private string _transType;
        /// <summary>
        /// URI
        /// </summary>
        private string _uri;
        /// <summary>
        /// IsDynamic
        /// </summary>
        private bool _dynamic;

        public BtsPhysicalBindingAttribute(XmlReader reader)
            : base(reader)
        {
            base._bindType = BindingAttributeType.Physical;

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
                        if (valName.Equals("InPipeline"))
                            _inPipe = val;
                        else if (valName.Equals("OutPipeline"))
                            _outPipe = val;
                        else if (valName.Equals("TransportType"))
                            _transType = val;
                        else if (valName.Equals("URI"))
                            _uri = val;
                        else if (valName.Equals("IsDynamic"))
                            _dynamic = Convert.ToBoolean(val);
                        else
                        {
                            Debug.WriteLine("[BtsPhysicalBindingAttribute.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
                else if (reader.Name.Equals("om:Element"))
                {
                    Debug.WriteLine("[BtsPhysicalBindingAttribute.ctor] unhandled element " + reader.GetAttribute("Value"));
                    Debugger.Break();
                }
            }
            reader.Close();
        }

        public bool IsDynamic
        {
            get { return _dynamic; }
        }
	
        public string URI
        {
            get { return _uri; }
        }

        public string TransportType
        {
            get { return _transType; }
        }

        public string OutPipeline
        {
            get { return _outPipe; }
        }

        public string InPipeline
        {
            get { return _inPipe; }
        }
		 
    }
}
