#define BTS
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.XPath;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
#if BTS
    /// <summary>
    /// CatalogExplorerFactory - create CatalogExplorer shapes.
    /// Dependency: Microsoft.BizTalk.ExplorerOM (C:\Program Files\Microsoft BizTalk Server 2006\Developer Tools\Microsoft.BizTalk.ExplorerOM.dll)
    /// This module must run on a BTS server.
    /// </summary>
    public sealed class CatalogExplorerFactory
    {
        
        public static BtsCatalogExplorer CatalogExplorer()
        {
            BtsCatalogExplorer _catalog = new BtsCatalogExplorer();

            if (_catalog.ConnectionString.Length < 0)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\BizTalk Server\3.0\Administration");
                _catalog.ConnectionString = String.Format("Server={0};Database={1};Integrated Security=SSPI", key.GetValue("MgmtDBServer"), key.GetValue("MgmtDBName"));
            }

            return _catalog;
        }
        public static BtsCatalogExplorer CatalogExplorer(string mgmtDBServer, string mgmtDBName)
        {
            BtsCatalogExplorer _catalog = new BtsCatalogExplorer();
            _catalog.ConnectionString = String.Format("Server={0};Database={1};Integrated Security=SSPI", mgmtDBServer,mgmtDBName);
            return _catalog;

        }
    }

#endif
public sealed class BtsBaseComponentFactory
{
    public static BtsBaseComponent BtsBaseComponent()
    {
        BtsBaseComponent _base = new BtsBaseComponent();


        return _base;
    }
}

#if BTS
    /// <summary>
    /// Return System.Reflection.Assembly versions of BizTalk assemblies.
    /// </summary>
    public sealed class BtsAssemblyFactory
    {
        /// <summary>
        /// Extracts XML assembly information from adpl_sat table in BizTalkMgmtDb table, searches for and loads the System.Reflection.Assembly object from its physical location.
        /// </summary>
        /// <param name="assemblyDisplayName">DisplayName property of ExplorerOM.BtsAssembly object.</param>
        /// <returns>loaded System.Reflection.Assembly object.</returns>
        public static System.Reflection.Assembly GetAssembly(string assemblyDisplayName)
        {
            string fname = String.Empty;
            SqlConnection conn = new SqlConnection(CatalogExplorerS.GetCatalogExplorer().ConnectionString);           
            try
            {
                conn.Open();
                SqlCommand sb = new SqlCommand(String.Format("select properties from adpl_sat where luid='{0}'", assemblyDisplayName), conn);
                XmlReader read = sb.ExecuteXmlReader();

                XmlDocument doc = new XmlDocument();
                doc.Load(read);
                
                XPathNavigator nav = doc.CreateNavigator();
                nav.MoveToRoot();
                XPathNavigator iterator = nav.SelectSingleNode("DictionarySerializer2OfStringObject/dictionary/item[key = \"SourceLocation\"]");
                XPathNodeIterator fullFileName = iterator.SelectChildren("SourceLocation","");

                if (null == fullFileName.Current.Value)
                {
                    ///TODO: research if %BTAD_Installdir% in properties column expands as needed, or what -- what IF SourceLocation doesn't exist? does that happen?s
                    System.Diagnostics.Debugger.Break(); 
                }
                fname = fullFileName.Current.Value.Replace("SourceLocation","");
            }
            catch (Exception e)
            {
#if DEBUG
                System.Diagnostics.Debugger.Break();
#endif
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            Debug.WriteLine("loading file " + fname);

            return System.Reflection.Assembly.LoadFile(fname);
        }
    }
#endif

    /// <summary>
    /// This class is used to build everything that can be found within a ServiceBody defnition. 
    /// </summary>
    public sealed class BtsShapeFactory
    {

        public static BtsBaseComponent CreateShape (XmlReader reader)
        {
            //gotta initialize it
            reader.Read ();

            //we don't read a subtree in any of these because we're receiving one from the invoker.
            string val = reader.GetAttribute("Type");
            switch (val)
            {
                case "AtomicTransaction":
                    return new BtsAtomicTx (reader);
                case "Call":
                    return new BtsCallShape (reader);
                case "CallRules":
                    return new BtsCallRulesShape(reader);
                case "Catch":
                    return new BtsCatchShape (reader);
                case "Compensation":
                    return new BtsCompensation (reader);
                case "Compensate":
                    return new BtsCompensateShape(reader);
                case "Construct":
                    return new BtsConstructShape (reader);
                case "CorrelationDeclaration":
                    return new BtsCorrelationDeclaration (reader);
                case "Decision":
                    return new BtsDecisionShape (reader);
                case "DecisionBranch":
                    return new BtsDecisionShape (reader);
                case "Delay":
                    return new BtsDelayShape (reader);
                case "Listen":
                    return new BtsListenShape (reader);
                case "ListenBranch":
                    return new BtsListenBranchShape (reader);
                case "LongRunningTransaction":
                    return new BtsLongRunningTx (reader);
                case "MessageDeclaration":
                    return new BtsMessageDeclaration (reader);
                case "Parallel":
                    return new BtsParallelShape(reader);
                case "ParallelBranch":
                    return new BtsParallelBranchShape(reader);
                case "Parameter":
                    return new BtsParameter (reader);
                case "Receive":
                    return new BtsReceiveShape (reader);
                case "Scope":
                    return new BtsScopeShape (reader);
                case "Send":
                    return new BtsSendShape (reader);
                case "StatementRef":
                    return new BtsStatementRef (reader);
                case "Suspend":
                    return new BtsSuspendShape(reader);
                case "Task":
                    return new BtsTaskShape (reader);
                case "Terminate":
                    return new BtsTerminateShape (reader);
                case "Throw":
                    return new BtsThrowShape (reader);
                case "TransactionAttribute":
                    return new BtsTransactionAttribute (reader);
                case "VariableAssignment":
                    return new BtsVariableAssignmentShape (reader);
                case "VariableDeclaration":
                    return new BtsVariableDeclaration (reader);
                case "While":
                    return new BtsWhileShape (reader);
                default:
                    {
                        Debug.WriteLine ("[BtsShapeFactory.CreateShape] unhandled shape constructor for : " + reader.GetAttribute ("Type"));
                        Debugger.Break ();
                        break;
                    }
            }
            return null;
        }
    }
}