using System;
using System.Collections.Generic;
using System.Text;

namespace EndpointSystems.OrchestrationLibrary
{
    /// <summary>
    /// grandfather interface
    /// </summary>
    public interface IBtsBaseComponent
    {
    }

    /// <summary>
    /// BizTalk orchestration interface
    /// </summary>
    public interface IBtsOrch : IBtsBaseComponent{}

    /// <summary>
    /// interface for the Application logical container
    /// </summary>
    public interface IBtsApp : IBtsBaseComponent{}

    /// <summary>
    /// orchestration variables for orchestration instances
    /// </summary>
    public interface IBtsOrchVariable : IBtsBaseComponent { }

    /// <summary>
    /// instance message variables for orchestration instances
    /// </summary>
    public interface IBtsOrchMsg : IBtsBaseComponent { }

    /// <summary>
    /// Orchestration shapes that can contain other orchestration shapes
    /// </summary>
    public interface IBtsContainer : IBtsBaseComponent, IBtsShape
{
        List<BtsBaseComponent> Components { get;}
}
    /// <summary>
    /// Your average, everyday, run-of-the-mill orchestration shape
    /// </summary>
    public interface IBtsShape : IBtsBaseComponent { }

    public interface IBtsDecisionBranch : IBtsShape { }

    /// <summary>
    /// PortType interface
    /// </summary>
    public interface IBtsPortType : IBtsBaseComponent { }

    public interface IBtsOperationDeclaration : IBtsBaseComponent { }

    public interface IBtsMessageRef : IBtsBaseComponent { }

    public interface IBtsMultiPartMessageType : IBtsBaseComponent { }

    public interface IBtsPartDeclaration : IBtsBaseComponent { }

    public interface IBtsCorrelationDeclaration : IBtsBaseComponent { }

    public interface IBtsCorrelationType : IBtsBaseComponent { }

    public interface IBtsStatementRef : IBtsBaseComponent { }

    public interface IBtsPropertyRef : IBtsBaseComponent { }

    public interface IBtsMessageDeclaration : IBtsBaseComponent { }
    
    public interface IBtsPortDeclaration : IBtsBaseComponent { }

    public interface IBtsMessagePartRef : IBtsBaseComponent { }

    public interface IBtsFilter { }
}
