using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using System.Xml;
using System.Reflection;
using System.IO;

namespace EndpointSystems.OrchestrationLibrary
{
    public class NullArtifactDataXmlException : ApplicationException
    {
        public NullArtifactDataXmlException(): base("Artifact Xml document must contain a reference")
        {}
    }
    public class NullOrchNodeException : NullReferenceException
    {           
        public NullOrchNodeException(string nodeName, string orchName, string source): base("Node " + nodeName + " does not exist within orchestration XML source obtained from orchestration " + orchName)
        {
            base.Source = source;
            //base.m = "Node " + nodeName + " does not exist within orchestration XML source obtained from orchestration " + orchName;
            base.Data.Add("Node", nodeName);
            base.Data.Add("Orchestration", orchName);
            
        }
    }

    /// <summary>
    /// This exception is called whenever a property is found within a component that is not accounted for by the component. It is a library failure that must
    /// be addressed by the author.
    /// </summary>
    internal class UnhandledPropertyException : ApplicationException
    {
        private string _propName;
        public string PropertyName
        {
            get { return _propName; }
        }

        private string _parentName;
        public string ParentName
        {
            get { return _parentName; }
        }

        /// <summary>
        /// Create the exception
        /// </summary>
        /// <param name="parentName">Name of shape or object containing the unhandled property.</param>
        /// <param name="propName">Name of the unhandled property.</param>
        public UnhandledPropertyException (string parentName, string propName): base("Unhandled property " + propName + " found in " + parentName + " shape.")
        {
            _parentName = parentName;
            _propName = propName;
        }
    }
}