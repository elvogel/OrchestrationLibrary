using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsServiceBody : BtsContainer, IBtsOrch
    {        
        public BtsServiceBody (XmlReader reader)
            : base (reader)
	  {
          while (reader.Read())
          {
              if (reader.AttributeCount < 1)
                  break;
              else if (reader.Name.Equals("om:Property"))
              {
                  string valName = reader.GetAttribute("Name");
                  string val = reader.GetAttribute("Value");

                  if (!base.GetReaderProperties(valName,val))
                  {
                      if (valName.Equals("AnalystComments"))
                          _comments = val;
                      else
                      {
                          Debug.WriteLine("[BtsServiceBody.ctor] unhandled property " + valName);
                          Debugger.Break();
                      }
                      
                  }
              }
              else if (reader.Name.Equals("om:Element"))
              {
                  Debug.WriteLine("[BtsServiceBody.ctor] unhandled element " + reader.GetAttribute("Type"));
                  Debugger.Break();
              }
          }
      }									 
    }
}
