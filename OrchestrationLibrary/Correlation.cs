using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// BtsCorrelationDeclaration is a child object of BtsServiceDeclaration
    /// </summary>
    public class BtsCorrelationDeclaration : BtsBaseComponent, IBtsCorrelationDeclaration
    {
        private List<BtsStatementRef> _statementRefs = new List<BtsStatementRef>();
        private MessageDirection _paramType;
        private string _type;
        public BtsCorrelationDeclaration(XmlReader reader)
            : base(reader)
        {
            while (reader.Read())
            {
                if (!reader.HasAttributes)
                    break;

                if (reader.Name.Equals("om:Property"))
                    this.GetReaderProperties(reader.GetAttribute("Name"), reader.GetAttribute("Value"));
                else if (reader.Name.Equals("om:Element"))
                {
                    if (reader.GetAttribute("Type").Equals("StatementRef"))
                        _statementRefs.Add(new BtsStatementRef(reader.ReadSubtree()));
                    else
                    {
                        Debug.WriteLine("[BtsCorrelationDeclaration.ctor] unhandled om:Property " + reader.GetAttribute("Name"));
                        Debugger.Break();
                    }
                }
            }
        }

        internal new void GetReaderProperties(string xmlName, string xmlValue)
        {
            if (!base.GetReaderProperties (xmlName, xmlValue))
            {
                if (xmlName.Equals ("ParamDirection"))
                    this._paramType = base.GetMessageDirection (xmlValue);
                else if (xmlName.Equals("AnalystComments"))
                    _comments = xmlValue;
                else if (xmlName.Equals ("Type"))
                    this._type = xmlValue;
                else
                {
                    Debug.WriteLine ("[BtsCorrelationDeclaration.GetReaderProperties] unhandled om:Property " + xmlName);
                    Debugger.Break ();
                }
            }
        }
        
        public string CorrelationType
        {
            get { return _type; }
        }

        public MessageDirection ParameterType
        {
            get { return _paramType; }
        }
        
        public List<BtsStatementRef> StatementReferences
        {
            get { return _statementRefs; }
        }
    }

    public class BtsCorrelationType : BtsBaseComponent, IBtsCorrelationType
    {
        private string _modifier;
        private BtsPropertyRef _propRef;

        public BtsCorrelationType(XmlReader reader)
          : base(reader)
      {
          while (reader.Read())
          {
              if (!reader.HasAttributes)
                  break;
              else if (reader.Name.Equals("om:Property"))
              {
                  if (!base.GetReaderProperties(reader.GetAttribute("Name"), reader.GetAttribute("Value")))
                  {
                      if (reader.GetAttribute("Name").Equals("TypeModifier"))
                          _modifier = reader.GetAttribute("Value");
                      else
                          Debug.WriteLine("[BtsPropertyRef.ctor] unhandled om:Property " + reader.GetAttribute("Name"));
                  }
              }
              else if (reader.Name.Equals("om:Element"))
              {
                  if (reader.GetAttribute("Type").Equals("PropertyRef"))
                      _propRef = new BtsPropertyRef(reader.ReadSubtree());
                  else
                  {
                      Debug.WriteLine("[BtsPropertyRef.ctor] unhandled element " + reader.Name);
                      Debugger.Break();
                  }
              }
              else
              {
                  Debug.WriteLine("[BtsPropertyRef.ctor] unhandled element " + reader.Name);
                  Debugger.Break();
              }
          }          
      }
        
        public string Modifier
        {
            get { return _modifier; }
        }

        public BtsPropertyRef PropertyRef
        {
            get { return _propRef; }
        }

    }

    /// <summary>
    /// BtsPropertyRef is a child of BtsCorrelationType
    /// </summary>
   public class BtsPropertyRef : BtsBaseComponent, IBtsPropertyRef
    {
        private string _ref;
       public BtsPropertyRef(XmlReader reader)
          : base(reader)
      {
          while (reader.Read())
          {
              if (!reader.HasAttributes)
                  break;

              if (reader.Name.Equals ("om:Property"))
              {
                  if (!base.GetReaderProperties (reader.GetAttribute ("Name"), reader.GetAttribute ("Value")))
                  {
                      if (reader.GetAttribute ("Name").Equals ("Ref"))
                          _ref = reader.GetAttribute ("Value");
                      else
                      {
                          Debug.WriteLine ("[BtsPropertyRef.ctor] unhandled om:Property " + reader.GetAttribute ("Name"));
                          Debugger.Break ();
                      }
                  }
              }
              else
              {
                  Debug.WriteLine ("[BtsPropertyRef.ctor] unhandled element " + reader.Name);
                  Debugger.Break ();
              }
          }
          

      }
        
        public string Ref
        {
            get { return _ref; }
        }


    }

    /// <summary>
    /// BtsStatementRef is a child of BtsCorrelationDeclaration
    /// </summary>
  public  class BtsStatementRef : BtsBaseComponent, IBtsStatementRef
    {
        private bool _initializes;
        private string _ref;

      public BtsStatementRef(XmlReader reader)
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
                      if (reader.GetAttribute("Name").Equals("Initializes"))
                          _initializes = Convert.ToBoolean(reader.GetAttribute("Value"));
                      else if (reader.GetAttribute("Name").Equals("Ref"))
                          _ref = reader.GetAttribute("Value");
                      else
                          Debug.WriteLine("[BtsStatementRef.ctor] unhandled om:Property " + reader.GetAttribute("Name"));
                  }
              }
              else if (reader.Name.Equals ("om:Element"))
              {

              }
              else
                  Debug.WriteLine ("[BtsStatementRef.ctor] unhandled element " + reader.Name);
          }          

      }

      public string Ref
      {
          get { return _ref; }
      }
	
      public bool Initializes
      {
          get { return _initializes; }
      }

    }
}
