using System;
using System.Diagnostics;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo.RegSvrEnum;
using Microsoft.SqlServer.Management.UI.VSIntegration.Editors;
using Microsoft.SqlServer.Management.UI.VSIntegration.ObjectExplorer;

namespace DamnTools.SqlTodoExplorer.Services
{
    public class ServerConnectionService : IServerConnectionService
    {
        private readonly IScriptFactory _scriptFactory;
        private readonly IObjectExplorerService _objectExplorerService;
        
        public ServerConnectionService(IScriptFactory scriptFactory, IObjectExplorerService objectExplorerService)
        {
            if (scriptFactory == null) throw new ArgumentNullException("scriptFactory");
            if (objectExplorerService == null) throw new ArgumentNullException("objectExplorerService");

            _scriptFactory = scriptFactory;
            _objectExplorerService = objectExplorerService;
        }

        public ServerConnectionInfo GetCurrentConnection()
        {
            ServerConnectionInfo currentConnection = null;
            try
            {
                UIConnectionInfo connectionInfo = null;

                if (_scriptFactory.CurrentlyActiveWndConnectionInfo != null)
                {
                    connectionInfo = _scriptFactory.CurrentlyActiveWndConnectionInfo.UIConnectionInfo;
                }

                if (connectionInfo != null)
                {
                    currentConnection = new ServerConnectionInfo
                    {
                        ServerName = connectionInfo.ServerName,
                        UseIntegratedSecurity = string.IsNullOrEmpty(connectionInfo.Password),
                        UserName = connectionInfo.UserName,
                        Password = connectionInfo.Password
                    };
                }
                else
                {
                    int nodeCount;
                    INodeInformation[] nodes;

                    _objectExplorerService.GetSelectedNodes(out nodeCount, out nodes);

                    if (nodes.Length > 0)
                    {
                        var info = nodes[0].Connection as SqlConnectionInfo;
                        if (info != null)
                        {
                            currentConnection = new ServerConnectionInfo
                            {
                                ServerName = info.ServerName,
                                UseIntegratedSecurity = info.UseIntegratedSecurity,
                                UserName = info.UserName,
                                Password = info.Password
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error getting current connection: {0}", ex);
            }
            return currentConnection;
        }
    }
}