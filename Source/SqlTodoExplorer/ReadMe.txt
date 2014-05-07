Instruction to run in debug:

1 - Copy the "DamnTools.SqlTodoExplorer.AddIn" file in the following folder:

		C:\ProgramData\Microsoft\SQL Server Management Studio\11.0\AddIns

2 - Edit the copy of the .AddIn file and change the <Assembly> path 
	to point the debug dll "DamnTools.SqlTodoExplorer.dll" is, for example:

		<Assembly>C:\Users\UserAccount\Documents\Git\DamnTools\Source\SqlTodoExplorer\bin\DamnTools.SqlTodoExplorer.dll</Assembly>

3 - Open the project properties of "DamnTools.SqlTodoExplorer" project (ALT+ENTER)
	and edit the Debug->Start external application with the full path to the 
	SQL Management Studio 2012 executable (Ssms.exe):

		C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Binn\ManagementStudio\Ssms.exe

	and the Working directory corrisponding to the Ssms.exe parent folder:

		C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Binn\ManagementStudio\

4 - Go to Debug->Exceptions... and uncheck the Thrown on "PInvokeStackImbalance" and "IvalidVariant" under "Managed Debugging Assistant"

5 - Hit F5