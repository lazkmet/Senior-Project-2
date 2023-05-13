# Senior-Project-2
This repository contains the code for my senior project. It is a Blazor web application that uses Entity Framework to connect to a database. In order to install and run this project, there is some setup that must be completed, as described in the Database Configuration Guide pdf that has been included. Please read the guide and follow the instructions within.

Once the database is set up, to actually run this project, you will need to open the solution in Visual Studio 2022. Run the application in debug mode by selecting the play button labelled "VideoShareApp".

Because a certificate is required for hosting web applications in .NET, the first time you run the app it will attempt to install a development certificate that can be used for this debugging.

When attempting to connect to the application, your browser may warn you that it is an insecure connection because the certificate is self-signed. This warning can be safely ignored, because the connection is being hosted locally on your machine.