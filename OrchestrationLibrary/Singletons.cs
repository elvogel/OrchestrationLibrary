#define BTS   //on a BizTalk server
using System;
using System.Collections;
using System.Collections.Generic;
#if BTS
using Microsoft.BizTalk.ExplorerOM;
#endif
using Microsoft.Win32;
using System.Diagnostics;
namespace EndpointSystems.OrchestrationLibrary
{
#if BTS
    /// <summary>
    /// CatalogExplorerS - CatalogExplorer Singleton class
    /// Dependency: Microsoft.BizTalk.ExplorerOM (C:\Program Files\Microsoft BizTalk Server 2006\Developer Tools\Microsoft.BizTalk.ExplorerOM.dll)
    /// This module must run on a BTS server.
    /// </summary>
    public sealed class CatalogExplorerS
    {
        private static BtsCatalogExplorer _catalog = new BtsCatalogExplorer();
        
        public static BtsCatalogExplorer GetCatalogExplorer()
        {
            if (null == _catalog.ConnectionString)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\BizTalk Server\3.0\Administration");
                _catalog.ConnectionString = String.Format("Server={0};Database={1};Integrated Security=SSPI", key.GetValue("MgmtDBServer"), key.GetValue("MgmtDBName"));
            }

            return _catalog;
        }

        /// <summary>
        /// the "remote" version?
        /// </summary>
        /// <param name="mgmtDBServer">Database server containing BTS MgmtDBServer</param>
        /// <param name="mgmtDBName">BTS Mgmt DB Name</param>
        /// <returns>BtsCatalogExplorer object (ExplorerOM)</returns>
        public static BtsCatalogExplorer CatalogExplorer(string mgmtDBServer, string mgmtDBName)
        {
            BtsCatalogExplorer _catalog = new BtsCatalogExplorer();
            _catalog.ConnectionString = String.Format("Server={0};Database={1};Integrated Security=SSPI", mgmtDBServer, mgmtDBName);
            return _catalog;
        }
    } //CatalogExplorerS

    public sealed class ApplicationS
    {
        private static Application _app = null;

        public static Microsoft.BizTalk.ExplorerOM.Application GetApplication(string appName)
        {
            if (null != CatalogExplorerS.GetCatalogExplorer().Applications[appName])
                _app = CatalogExplorerS.GetCatalogExplorer().Applications[appName];

            else
            {
#if DEBUG
                ///TODO: Exception Handling
                Debugger.Break();       
#endif
                _app = null;
            }
            return _app;
        }             
    } //ApplicationS
#endif //BTS
} //namespace