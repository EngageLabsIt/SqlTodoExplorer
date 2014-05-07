![DamnTools logo](https://github.com/wiki/DamnTools/SqlTodoExplorer/images/logo.png)
SQL Todo Explorer
=========

SQL Todo Explorer is a SQL Server Management Studio add-in. It helps to navigate the list of "to-do" items within your databases. 

![SQL Todo Explorer icon](https://github.com/wiki/DamnTools/SqlTodoExplorer/images/sqltodoexplorer_ico.png)

"to-do" items can be marked as:
  - TODO
  - HACK
  - BUG
  - ASK

##Download

    git clone https://github.com/DamnTools/SqlTodoExplorer.git
    
##Build

In order to build the source, you need to install SQL Server Management Studio 2012 or above (also the Express version).

##Install

- Build the solution SqlTodoExplorer.sln

- Copy the `DamnTools.SqlTodoExplorer.dll` created in the bin folder to the following folder:

        C:\Program Files\DamnTools\SqlTodoExplorer

- Copy the `DamnTools.SqlTodoExplorer.AddIn` file in the following folder:

        C:\ProgramData\Microsoft\SQL Server Management Studio\<SQL Server Version>\AddIns

        Microsoft SQL Server Management Studio 2012:
        C:\ProgramData\Microsoft\SQL Server Management Studio\11.0\AddIns
        
- Edit the copy of the .AddIn file and change the <Assembly> path:

        <Assembly>C:\Program Files\DamnTools\SqlTodoExplorer\DamnTools.SqlTodoExplorer.dll</Assembly>

After the setup, you will find a new menu command under the "Tools" menu of Sql Server Management Studio.

![SQL Todo Explorer menu command](https://github.com/wiki/DamnTools/SqlTodoExplorer/images/new_menu_command.png)

The add-in view is a floating (by default) panel in your Sql Server Management Studio. You can dock the panel, pin and move it as any other panel within the IDE.

![SQL Todo Explorer panel](https://github.com/wiki/DamnTools/SqlTodoExplorer/images/panel.png)

##Debug

In Visual Studio:

- Open the project properties of "SqlTodoExplorer" project (ALT+ENTER) and fill in the "Debug->Start external application" option with the SQL Management Studio executable (Ssms.exe) full path:

        C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Binn\ManagementStudio\Ssms.exe

- Set the "Working directory" option to the Ssms.exe parent folder:

        C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Binn\ManagementStudio\
		
- Close the project properties

- Go to menu "Debug->Exceptions"... and uncheck the "Thrown" on "PInvokeStackImbalance" and "IvalidVariant" under "Managed Debugging Assistant"

- Hit F5

##Features

- Get the list of "to-do" items (TODO, HACK, BUG, ASK) of the selected connection in a specific user database
- Filter by database
- Filter by "to-do" item type
- Choose from three layout results (Flat list, Grouped by "to-do" and Grouped by object type)
- Drill/collapse the result when the layout is a treeview
- When the layout is "Flat list", filter by as-you-type search directly in the comments
- Double click an item in order to get the ALTER statement of the selected object in a new query window
- Refresh the connection context
- Export the results as CSV or XML file

##Contributing

1. Clone
2. Branch
3. Make changes
4. Push
5. Make a pull request

##Authors

- Michael Denny ([@dennymic])
- Alessandro Alpi ([@suxstellino])

__Contributors__
- See the [contributor] section

##License

DamnTools Sql Todo Explorer is released under the [MIT License].

##Icon

Icon and Logo created by [Daniela Malvisi]


[Daniela Malvisi]: https://it.linkedin.com/pub/daniela-malvisi/61/859/275
[MIT License]: https://github.com/DamnTools/License.txt
[contributor]: https://github.com/DamnTools/SqlTodoExplorer/graphs/contributors
[@suxstellino]: https://twitter.com/suxstellino
[@dennymic]: https://twitter.com/dennymic
