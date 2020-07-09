# Building Websites Using ASP.NET

---

## Razor Page Basics

Razor page files (.cshtml) are used to display HTML data with dynamic C# information in the form of models, pages, and functions. Razor syntax is indicated by the `@` sign.

```cs
@foreach(OrderDetail detail in order.OrderDetails)
{
<tr>
    <td><a href="https://www.google.com/search?q=@detail.Product.ProductName">@detail.Product.ProductName</a></td>
    <td>@detail.Quantity</td>
    <td>@detail.UnitPrice.ToString("c")</td>
    <td>@((detail.Quantity * detail.UnitPrice).ToString("c"))</td>
</tr>
}
```

This snippet takes server C# variables and inserts their values into the HTML code. (snippet taken from _customer.cshtml_ in **_NorthwindWeb_**) and helps to create table entries such as:

<tr align="center">
  <td>10302</td>
  <td>September 10, 1996</td>
  <td>
    <table class="table">
      <thead class="thead-inverse">
        <tr>
          <th>Product</th>
          <th>Quantity</th>
          <th>Unit Cost</th>
          <th>Subtotal</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>
            <a href="https://www.google.com/search?q=Alice Mutton">Alice Mutton</a>
          </td>
          <td>40</td>
          <td>$31.20</td>
          <td>$1,248.00</td>
        </tr>
        <tr>
          <td>
            <a href="https://www.google.com/search?q=Rössle Sauerkraut">Rössle Sauerkraut</a>
          </td>
          <td>28</td>
          <td>$36.40</td>
          <td>$1,019.20</td>
        </tr>
        <tr>
          <td>
            <a href="https://www.google.com/search?q=Ipoh Coffee">Ipoh Coffee</a>
          </td>
          <td>12</td>
          <td>$36.80</td>
          <td>$441.60</td>
        </tr>
      </tbody>
    </table>
    <p>Total: $2,708.80</p>
  </td>
</tr>

---

### NorthwindWeb / NorthwindEmployees

To create a new dotnet website, such as **_NorthwindWeb_**, use the command `dotnet new web` in the folder directory.
This will create a folder setup with files _Program.cs_ and _Startup.cs_. These files will hold all the configuration and settings for the site.

**_Program.cs_** is the "entry point" for the website as it holds the `Main` method.
This file also has a `CreateHostBuilder` method that specifies a `Startup` class used to configure the website.

**_Startup.cs_** has a `ConfigureServices` method which is used to add services such as [MVC](#building-websites-using-the-model-view-controller-mvc-pattern). The `Configure` method holds all the information for handling error messages when developing, routing for HTTP and HTTPS, and uses endpoints for requests such as `GET` HTTP requests.

To actually use Razor pages for dynamic websites, the service `services.AddRazorPages();` must be added to the `ConfigureServices(...)` in **_Startup.cs_** and the endpoint must be changes to `endpoints.MapRazorPages();` to tell the program to use the newly creates Razor pages.

**NorthwindEmployees** is using Razor class libraries to display the information listed from the database.

---

## Building Websites Using the Model-View-Controller (MVC) Pattern

Razor pages are perfect for simpler sites but more complicated sites are better handled with MVC for its use of separation between technical aspects such as **Models**, **Views**, and **Controllers**.

**Models**: classes that represent the data entities and view models used in the site.
**Views**: `.cshtml` files that render data in view models into HTML pages.
**Controllers**: execute code upon HTTP requests.

To create a new MVC model site, start with the command `dotnet new mvc --auth Individual`
Right after creating the new MVC site, use `dotnet run` to view the site in localhost. The site will already use redirect to HTTPS and have a navbar preset for logins.

This will also have created a lot of new files and folders in the site directory. Here is a short explanation:

- **_Areas:_** Files needed for features like **ASP.NET Core Identity** for authentication.
- **_bin, obj:_** Contains compiled assemblies.
- **_Controllers:_** C# classes that have methods(known as actions) that fetch a model and it to a view.
- **_Data:_** Entity Framework Core migration classes used by the ASP.NET Core Identity to provide data storage for authentication and authorization.
- **_Models:_** C# classes that represent all of the data gathered together by a controller and passed to a view.
- **_Properties:_** Configuration file for IIS and launching the website during development named launchSettings.json.
- **_Views:_** The `.cshtml` Razor files that combine HTML and C# code to dynamically generate HTML responses.
  `_ViewStart` file sets the default layout and the `_ViewImports` imports common namespaces used in all views like tag helpers
  - **_Home:_** Razor files for the home and privacy pages.
  - **_Shared:_** Razor files for the shared layout, an error page, and some partial views for logging in, accepting privacy policy, and managing the consent cookie.
- **_wwwroot:_** Static content used in the website, such as CSS for styling, images, JavaScript, and a favicon.ico file
- **_app.db:_** SQLite database that stores registered visitors.
- **_appsettings.json & appsettings.Development.json:_** Settings that your website can load at runtime, for example, the database connection string for the ASP.NET Identity system and logging levels.
- **_NorthwindMvc.csproj:_** Project settings like use of the Web.NET SDK, an entry to ensure that the app.db file is copied to the website's output folder, and a list of NuGet packages that your project requires, including:
  - Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
  - Microsoft.AspNetCore.Identity.EntityFrameworkCore
  - Microsoft.AspNetCore.Identity.UI
  - Microsoft.EntityFrameworkCore.Sqlite
  - Microsoft.EntityFrameworkCore.Tools
- **_Program.cs:_** Defines a class that contains the Main entry point that builds a pipeline for processing incoming HTTP requests and hosts the website using default options like configuring the Kestrel web server and loading `appsettings.json`. While building the host it calls the `UseStartup<T>()` method to specify another class that performs additional configuration.
- **_Startup.cs:_** Adds and configures services that your website needs, for example, ASP.NET Identity for authentication, SQLite for data storage, and so on, and routes for your application.
