using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
   public class BtsMultiPartMessageType: BtsBaseComponent, IBtsMultiPartMessageType
    {
        private string  _modifier;
       private List<BtsPartDeclaration> _parts = new List<BtsPartDeclaration>();

       public BtsMultiPartMessageType(XmlReader reader)
           : base(reader)
       {
           while (reader.Read())
           {
               if (!reader.HasAttributes)
                   continue;
               else if (reader.Name.Equals("om:Property"))
               {
                   string valName = reader.GetAttribute("Name");
                   string val = reader.GetAttribute("Value");
                   if (!base.GetReaderProperties(valName, val))
                   {
                       if (valName.Equals("TypeModifier"))
                           _modifier = val;
                       else if (valName.Equals("AnalystComments"))
                           _comments = val;
                       else
                       {
                           Debug.WriteLine("[BtsMultiPartMessageType.ctor] unhandled property " + valName);
                           Debugger.Break();
                       }
                   }
               }
               else if (reader.Name.Equals("om:Element"))
               {
                   if (reader.GetAttribute("Type").Equals("PartDeclaration"))
                       _parts.Add(new BtsPartDeclaration(reader.ReadSubtree()));
               }
               else
                   continue;
           }
           reader.Close();
       }
        
        public string Modifier
        {
            get { return _modifier; }
        }
        
        public bool Signal
        {
            get { return _signal; }
        }

       
        public List<BtsPartDeclaration> PartDeclarations
        {
            get { return _parts; }
        }


    } //BtsMultiPartMessageType

    public class BtsPartDeclaration : BtsBaseComponent, IBtsPartDeclaration
    {
        private string _className;
        private bool _bodyPart;

        public BtsPartDeclaration(XmlReader reader)
            : base(reader)
        {
            while (reader.Read())
            {
                if (!reader.HasAttributes)
                    continue;
                else if (reader.Name.Equals("om:Property"))
                {
                    string valName = reader.GetAttribute("Name");
                    string val = reader.GetAttribute("Value");
                    if (!base.GetReaderProperties(valName, val))
                    {
                        if (valName.Equals("ClassName"))
                        {
                            _className = val;
                        }
                        else if (valName.Equals("IsBodyPart"))
                            _bodyPart = Convert.ToBoolean(val);
                        else
                        {
                            Debug.WriteLine("[BtsPartDeclaration.ctor] unhandled property " + valName);
                            Debugger.Break();
                        }
                    }
                }
            }
            reader.Close();
        }
        
        public bool Signal
        {
            get { return _signal; }
        }

        public bool IsBodyPart
        {
            get { return _bodyPart; }
        }

        public string ClassName
        {
            get { return _className; }
        }

    }
}//namespace
