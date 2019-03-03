# MLBSeasonResultsProject
pulls from a static address and displays the season results for the MLB teams.

This application uses the nuget package Json.NET from Newtonsoft for json deserialization. It should be automatically retrieved via nuget when the application is built.

# To Build This UWP Application

To build this application all you need to do is clone the repository and then load the .sln project file located in the MLBSeasonResults directory in Visual Studios. From there you can deploy the application titled MLBSeasonResults by right clicking the .csproj file in the Solution Explorer tab. 

If you wish to run the Unit tests you can go to the Test menu in Visual Studios and click Run -> All Tests. The Unit tests I wrote were mostly for me to make sure my static utility classes would throw an exception if they failed to complete there task so that the Model could return false when it was initialized and the season results page would not be loaded.
