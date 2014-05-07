using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.SqlServer.Management.UI.VSIntegration.Editors;

namespace DamnTools.SqlTodoExplorer.Services
{
    public class ScriptService : IScriptService
    {
        private readonly IScriptFactory _scriptFactory;
        private readonly _DTE _dte;
        
        public ScriptService(IScriptFactory scriptFactory, _DTE dte)
        {
            if (scriptFactory == null) throw new ArgumentNullException("scriptFactory");
            if (dte == null) throw new ArgumentNullException("dte");

            _scriptFactory = scriptFactory;
            _dte = dte;
        }

        public void CreateNewScript(string content)
        {
            Document activeDocument = null;
            
            _scriptFactory.CreateNewBlankScript(ScriptType.Sql);

            var dte = _dte.DTE as DTE2;
            if (dte != null)
            {
                activeDocument = dte.ActiveDocument;
            }

            if (activeDocument != null)
            {
                var ts = activeDocument.Selection as TextSelection;
                if (ts != null)
                {
                    ts.Insert(content, (int)vsInsertFlags.vsInsertFlagsCollapseToStart);
                }
            }
        }
    }
}