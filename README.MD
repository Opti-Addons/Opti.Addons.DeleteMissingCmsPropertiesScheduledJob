[![NuGet Version](https://img.shields.io/nuget/v/Opti.Addons.DeleteMissingCmsPropertiesScheduledJob)](https://www.nuget.org/packages/Opti.Addons.DeleteMissingCmsPropertiesScheduledJob)

> [!CAUTION]
> This scheduled job was tested locally with only a few test cases. Please review the code and test it in your environment before running it in production. Please also make sure you have a backup of your database before running this scheduled job.
> The software is provided as is and you use it at your own risk.

# ℹ️ Delete Missing CMS Properties Scheduled Job

This scheduled job will delete any missing CMS properties from the database. This is useful when you have removed a property from your code and you want to remove it from the database. By default, Optimizely code does not remove properties from the database when you remove them from the code.

* **[Opti.Addons] Delete missing CMS data: List**
  * The one is read-only and will list all the missing properties.
* **[Opti.Addons] Delete missing CMS data: Delete**
  * The other one will delete all the missing properties.

# 🚀 Installation

NuGet URL: https://www.nuget.org/packages/Opti.Addons.DeleteMissingCmsPropertiesScheduledJob

Install the package and the scheduled jobs will be available in the list of scheduled jobs.

Add to the `Startup.cs`:

```csharp
services.AddDeleteMissingCmsProperties();
```

If the version of your .NET or CMS package are different, you may either:

* Fork the code and update the references.
* Just copy and paste the code into your solution.

# 📃 Version history

* 0.1.0
  * Initial version
