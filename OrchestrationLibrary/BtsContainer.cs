using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EndpointSystems.OrchestrationLibrary
{
    public class BtsContainer : BtsBaseComponent, IBtsContainer
    {
        internal List<BtsBaseComponent> _comps = new List<BtsBaseComponent> ();
        /// <summary>
        /// InitializedTransactionType
        /// </summary>
        internal bool _initTxType;
        /// <summary>
        /// IsSynchronized
        /// </summary>
        internal bool _sync;
        /// <summary>
        /// UseDefaultConstructor
        /// </summary>
        internal bool _defCtor;
        /// <summary>
        /// TimeoutExpression
        /// </summary>
        //internal string _timeoutExp;

        public BtsContainer(XmlReader reader) : base(reader)
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
                      if (valName.Equals("InitializedTransactionType"))
                          _initTxType = Convert.ToBoolean(val);
                      else if (valName.Equals("IsSynchronized"))
                          _sync = Convert.ToBoolean(val);
                      else if (valName.Equals("UseDefaultConstructor"))
                          _defCtor = Convert.ToBoolean(val);
                      else if (valName.Equals("AnalystComments"))
                          _comments = val;
                      //else if (valName.Equals("TimeoutExpression"))
                        //  _timeoutExp = val;
                      else
                      {
                          Debug.WriteLine("[BtsCatchShape.ctor] unhandled property " + valName);
                          Debugger.Break();
                      }
                  }
              }
              else if (reader.Name.Equals ("om:Element"))
                  _comps.Add (BtsShapeFactory.CreateShape (reader.ReadSubtree ()));        
          }                   
            reader.Close ();
      }

        /*
      public string TimeoutExpression
      {
          get { return _timeoutExp; }
      }
         */

      public bool UseDefaultConstructor
      {
          get { return _defCtor; }
      }

      public List<BtsBaseComponent> Components
      {
          get { return _comps; }
      }

      public bool IsSynchronized
      {
          get { return _sync; }
      }

      public bool InitializedTransactionType
      {
          get { return _initTxType; }
      }

  }
    public class BtsScopeShape : BtsContainer
    {
        public BtsScopeShape (XmlReader reader) : base (reader) { }
        
        public string TimeoutExpression
        {
            get { return ""; } //_timeoutExp; }
        }

    }

}
