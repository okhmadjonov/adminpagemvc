#  Oybek Akhmadjonov.
#  adminpagemvc


This is ASP.Net core 7 MVC project (C# and its framework). And it made on Visual Studio 2022 code editor and Windows 11 OS.
To Run this aplication you need to install .NET 7 .SDK , PostgresSQL on your OS.
PostgreySQL RDBM (Relational Database Management System) used for saving data.
1) First you need to clone project's github link  into any  file directory.
2) Then open the project with code editor which you prefer, (I recommend Visual Studio 2022)
3) Open the terminal in your code editor and type        add-migration InitialCreate
4) If migration succeed then type and run                update-database
5) If you finish this steps successfully than check your database to be sure and there would be created all custom tables and embedded identity tables.
6) After that come to terminal again, and run command           dotnet run seeddata
7) This command seeds default Admin and User  examples to database. After that you can run project and there you will see login page.
8) You can enter the Home page after registration or login.
9) You can find default login credentials inside Data directory Seed.cs file.
