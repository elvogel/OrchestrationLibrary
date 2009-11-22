namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// Enumerates the type of link the shape has to its parent, if provided within the orchestration shape.
    /// </summary>
    public enum ParentLink
    {
        /// <summary>
        /// Module_*
        /// </summary>
        Module, 
        /// <summary>
        /// ServiceDeclaration_* and ServiceBody_*
        /// </summary>
        Service,
        /// <summary>
        /// Scope_*
        /// </summary>
        Scope,
        /// <summary>
        /// Port* and Port_*
        /// </summary>
        Port,
        /// <summary>
        /// CorrelationDeclaration_StatementRef
        /// </summary>
        Correlation,
        /// <summary>
        /// CorrelationType_PropertyRef
        /// </summary>
        CorrelationProperty,
        /// <summary>
        /// Construct_MessageRef
        /// </summary>
        ConstructMessageRef,
        /// <summary>
        /// MultipartMessageType_*
        /// </summary>
        MultiPartMessage,
        /// <summary>
        /// Operation_*
        /// </summary>
        Operation,
        /// <summary>
        /// ComplexStatement_Statement, others?
        /// </summary>
        Statement,
        /// <summary>
        /// DNFPredicate
        /// </summary>
        FilterPredicate,
        /// <summary>
        /// Transform_InputMessagePartRef
        /// </summary>
        InputMessagePart,
        /// <summary>
        /// Transform_OutputMessagePartRef
        /// </summary>
        OutputMessagePart,
        /// <summary>
        /// CallRules_RulesParameterRef
        /// </summary>
        RulesParameterRef,
        /// <summary>
        /// For those we do not know of...
        /// </summary>
        Unknown,
        /// <summary>
        /// For those with no parent link attributes
        /// </summary>
        None
    }

    //////////////////////////////////////////////////
    ///Module_ServiceDeclaration
    ///ServiceDeclaration_ServiceBody
    /// CorrelationDeclaration_StatementRef
    ///ServiceBody_Statement
    ///Scope_VariableDeclaration
    ///ServiceDeclaration_MessageDeclaration
    ///ServiceDeclaration_VariableDeclaration
    ///ServiceDeclaration_PortDeclaration
    ///PortDeclaration_CLRAttribute
    ///ServiceDeclaration_PortDeclaration
    ///MultipartMessageType_PartDeclaration
    ///OperationDeclaration_RequestMessageRef
    ///CorrelationType_PropertyRef
    ///Module_CorrelationType
    ///ServiceDeclaration_ServiceLinkDeclaration - 

    
    /// <summary>
    /// Enumerate the different Message object directions found in orchestrations.
    /// </summary>
    public enum MessageDirection
    {
        In,
        Out,
        InOut,
        Indeterminant //error condition
    }

    /// <summary>
    /// Used by BtsOperationDeclaration object to determine operation type.
    /// See Microsoft.BizTalk.ExplorerOM.OperationType for 'original'
    /// </summary>
    public enum OperationType
    {
        None,
        Notification,
        OneWay,
        RequestResponse,
        SolicitResponse
    }

    /// <summary>
    /// used by the child objects of BindingAttribute to determine binding type
    /// </summary>
    public enum BindingAttributeType
    {
        Logical,
        Direct,
        Physical
    }

    public enum IsolationType
    {
        Serializable,
        ReadRepeatable,
        ReadCommitted,
    }
}