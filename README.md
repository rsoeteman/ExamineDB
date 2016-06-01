# ExamineDB
ExamineDB allows you to index SQL server data using examine based on a defined query in the configuration. Examine is much faster to query than SQL server so it improves the speed of your website. 

##Install##

Simply install Examine DB by NuGet command "**Install-Package ExamineDB**"

## Configuration ##
To configure Examine DB add the following configuration snippets to the Examine config files.

### ExamineIndex.config ###

Add the following configuration snippet to your ExamineIndex.config.

    <IndexSet SetName="DBIndexerIndexSet" IndexPath="~/App_Data/TEMP/ExamineIndexes/{machinename}/DBIndexer/" />

### ExamineSettings.config ###

Configure the indexer by the following snippet

    <add name="DBIndexer" type="ExamineDB.Indexers.DBIndexer, ExamineDB"
            indexSet="DBIndexerIndexSet"
	        nodeType="examineDBProducts"
	        connectionStringName="examineDBConnection"
	        sql="SELECT * FROM Production.Product"
	        singleRecordSQL = "SELECT * FROM Production.Product where productId = @0"
	        primaryKeyField = "productId"/>

####Configuration attributes explained####

- Indexset, correspondents with the IndexSet we configure in ExamineIndex.config
- nodeType, sets the nodeType
- connectionStringName, correspondents with the configured connectionstring defined in web.config. This connectionstring will be used to retrieve the data.
- sql. The SQL statement that we use to get all the data.
- singleRecordSQL. The SQL used to retrieve a single record form the database. Make sure the primary key parameter is passed in as @0 and make sure to retrieve the same columns as the normal sql statement.
- primaryKeyField. The column in the results that contain the primary key value.

Configure the searcher by the following snippet
    
    <add name="DBSearcher" type="UmbracoExamine.UmbracoExamineSearcher, UmbracoExamine"   indexSet="DBIndexerIndexSet" />

##Usage##

Once you configured the indexer correctly and the index contains items you can query the index using the Examine API. See the [Examine documentation](https://our.umbraco.org/documentation/reference/searching/examine/) for full API syntax.

    @{
	var searcher = ExamineManager.Instance.SearchProviderCollection["DBSearcher"];
	var searchCriteria = searcher.CreateSearchCriteria();
	var query = searchCriteria.Field("Name", "paint").Compile();
	var searchResults = searcher.Search(query);

	<ul>
    	@foreach (var item in searchResults)
	    {
    	    <li>@item.Fields["Name"]</li>
	    }
	</ul>

	}

##API##
ExamineDB  comes with a small management API to rebuild a complete index, or part of the index.

**Rebuild a complete index**
    ExamineDB.Helpers.IndexHelper.RebuildIndex("IndexName here");

**Remove a single item from the index**
    ExamineDB.Helpers.IndexHelper.DeleteFromIndex("Id here", "IndexName here");

**Re-index a single node**
    ExamineDB.Helpers.IndexHelper.DeleteFromIndex("Id here", "IndexName here");

## Test in Umbraco ##

The Examine DB Umbraco tester comes with a dashboard control that allows you to use the above API. 
Simply install Examine DB UmbracoTester by NuGet command "**Install-Package ExamineDB.UmbracoTester**"

After install you need to add the following snippet to dashboard.config file

    <section alias="ExamineDBDashboardSection">
    	<areas>
      		<area>developer</area>
    	</areas>
    	<tab caption="ExamineDB">
      		<control>
        		~/app_plugins/examinedb/backoffice/dashboard.html
      		</control>
    	</tab>
  	</section>
