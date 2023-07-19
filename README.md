# Online Education System

This is a web application for online education that allows instructors to add, edit, and remove students based on admin approval and also to record their old and new grades finally, at the end of the month you can calculate their average degrees. The application provides two types of users, Admin, Instructor. The application is built using ASP.NET Core MVC.

## Prerequisites 
- Visual Studio 2020
- .NET Core 7
- SQL Server 2019 or later

## Getting Started

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Open the `appsettings.json` file in the `QuizeApp.API` project and replace the database connection string with your own.
4. Open the Package Manager Console in Visual Studio and run the following command to create the database tables:

```
Update-Database
```

5. Build and run the solution in Visual Studio. 

## Features

### Admin

The admin can perform the following actions:

- Login to the application.
- Add, edit, and delete students or instructors.
- accept or decline the instructor's requests.
- Edit and update the grades of each student.

### Instructors

The instructor can perform the following actions:

- log in to the application.
- Initiate requests to add edit or delete students.
- Either edit or delete his request.
- View all his sent requests and their status.
- Check, edit, and record student's grades.
- Calculate their average degrees.



## Authentication

The application uses password hashing in the database to store user passwords securely. When a user logs in, the application generates a token that is used to check if the user is authenticated or not.

## Built With

- Bootstrap - A front-end web application framework.
- ASP.NET Core Web MVC - A  web application framework.
- SQL Server - A relational database management system.

