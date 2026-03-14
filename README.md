Fast Food Restaurant Web Application

A full-stack Fast Food Restaurant Web Application built with ASP.NET Core MVC.
The system allows customers to browse meals, view prices, add items to the cart, and place orders easily.

The project also includes an Admin Dashboard that allows administrators to manage meals, categories, employees, and customer orders.

--------------------------------------------------

FEATURES

Customer Features
- Browse restaurant menu with prices
- View meal details
- Add meals to cart
- Update meal quantities
- Remove items from cart
- Register and login before placing orders
- Place orders using Cash on Delivery
- View and manage cart before confirming the order

Admin Dashboard Features
- Manage Employees's Accounts 
- Manage Roles and Permissions
- Add / Edit / Delete Meals
- Manage Meal Categories
- Upload Meal Images
- View all Customer Orders
--------------------------------------------------

ADMIN ACCESS

Email:
Admin@gmail.com

Password:
Admin@123

--------------------------------------------------

TECHNOLOGIES USED

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- JavaScript
- HTML
- CSS

-------------------------------------------------

DEPENDENCIES

The project uses the following main NuGet packages:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.AspNetCore.Identity.EntityFrameworkCore

--------------------------------------------------

INSTALLATION AND SETUP

Follow these steps to run the project locally.

1) Clone the repository

--------------------------------------------------

2) Open the Project

Open the .sln file using Visual Studio.

--------------------------------------------------

3) Configure Database Connection

Open the file appsettings.json

Update the connection string:

"ConnectionStrings": {
 "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=FastFoodRestaurant;Trusted_Connection=True;TrustServerCertificate=True;"
}

--------------------------------------------------

4) Create the Database

Open:
Tools → NuGet Package Manager → Package Manager Console

Run the command:

Update-Database

This will create the database and tables automatically using Entity Framework Core migrations.

--------------------------------------------------

5) Run the Project

Set Portfolio.Web as the Startup Project.

Then press F5 to run the application.
