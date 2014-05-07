using System;
using System.Diagnostics;
using Microsoft.SqlServer.Management.UI.VSIntegration;
using Microsoft.SqlServer.Management.UI.VSIntegration.ObjectExplorer;

namespace DamnTools.SqlTodoExplorer.Services
{
    public static class TodoExplorer
    {
        public static ITodoExplorerManager CreateFromCurrentConnection()
        {
            var explorerService = (IObjectExplorerService)ServiceCache.ServiceProvider.GetService(typeof(IObjectExplorerService));
            var scriptFactory = ServiceCache.ScriptFactory;
            var connectionService = new ServerConnectionService(scriptFactory, explorerService);
            var connectionInfo = connectionService.GetCurrentConnection();
            if (connectionInfo == null) throw new Exception("Cannot create todo explorer services because cannot get the current connection.");
            Trace.TraceInformation("ConnectionInfo: {0}", connectionInfo);
            var serverGateway = new SmoServerGateway(connectionInfo);
            var dte = ServiceCache.ExtensibilityModel;
            var scriptService = new ScriptService(scriptFactory, dte);
            var todoPatternService = new TodoPatternService();
            var todoExplorerService = new TodoExplorerManager(serverGateway, scriptService, todoPatternService);
            return todoExplorerService;
        }
    }
}