⚙️ Installation and Operation You can follow these steps to run the project on your local machine:

Clone the project

Open with Visual Studio:

Open the .sln file in the project folder using Visual Studio.

Configure the database connection:

Open the appsettings.json file inside the Portfolio.Web project.

Update the connection information in the ConnectionStrings section according to your SQL Server credentials.

"ConnectionStrings": { "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=Ecommerce;Trusted_Connection=True;TrustServerCertificate=True;" }

Create the Database (Migrations):

Visual Studio'da Tools > NuGet Package Manager > Package Manager Console'u açın.

In the console that opens, select Portfolio.DataAccess as the Default project.

Create the database and add the tables by running the following command:

Update-Database

Start the Project:

Set Portfolio.Web as the Startup Project.

To run the project, press F5 or click the Start button.

📄 License This project is licensed under the MIT License. See the LICENSE file for details.
