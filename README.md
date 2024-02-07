# TEMPLATE

built with C# and Razor Pages, using the MVC structure.

By Henry Oberholtzer & Noah Kise

## Technologies Used

- C#
- MySQL
- MSTest
- Razor Pages

## Description

## Refactor Goals

- Add data validation
- Date updated fields for collections
- Selectable search category on main page
- Sort items by date added, or by value
- More layout / css
- When a user adds a collection, input fields create a new table with those properties in MySQL, create expandable categories for more specific things

## User Stories

## Technical requirements


## Project Structure

```
.gitignore
README.md
```

## Setup/Installation Requirements

- .NET6 or greater is required for set up.
- To establish locally, download the [repository](https://github.com/henry-oberholtzer/collections-manager.git) to your computer.
- Open the folder with your terminal and run `dotnet restore` to gather necessary resources.
- To run the application, within the solution folder, run the command `dotnet run` after the restore.
- A local instance of MySQL is required to be set up and running to use the project.
- You will need to estalish a copy of the database on your own machine, by importing the `collections_manager.sql` schema into your server.
- In the production direction, `/ProjectName` run `$ touch appsettings.json`
- In `appsettings.json`, enter the following, replacing `USERNAME` and `PASSWORD` to match the settings of your local MySQL server.
    ```
    {
        "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Port=3306;database=collections_manager$> sudo apt-get install mysql-workbench-community$> sudo apt-get install mysql-workbench-community;uid=USERNAME;pwd=PASSWORD;"
        }
    }
    ```
- To start the project, in the production directory, run the command `dotnet run`.

## Known Bugs

- Nothing sets the date

## License

(c) 2024 [Henry Oberholtzer](https://www.henryoberholtzer.com/) & Noah Kise

Original code licensed under GNU GPLv3, other code bases and libraries as stated.
