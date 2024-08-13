This application can be used to access your Github repositories, edit their descriptions and save the changes to a file.

The frontend part of the application has been built using Angular (version 18).
To run the frontend, first you would need to install node modules ('npm install') and then run the application with 'ng serve -o'.

The backend part of the application has been built using ASP.NET Core (Version 8).
It can be started with 'dotnet run' in the project directory (or by opening the solution and running it through an IDE).

The Github OAuth app was created through the Github developer tools.

The backend has endpoints for signing in to Github, fetching the repositories and storing them in a file after being potentially edited on the frontend.
Signing into Github has been done via OAuth authorization, after which the acces token is stored in the session and used for subsequent calls.
Should the user attempt to access data without being authorized, they will be redirected to an error page.
