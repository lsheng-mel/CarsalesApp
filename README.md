# CarsalesApp

Follow the steps below to migrate the database schema to production database server:
1. Update the field "ConnectionStrings::DefaultConnection" in appsettings.json in CarsalesApp project.
2. Run the command 'dotnet ef database update' in CLI/PM.
