# architect
.Net core achitecture playground

## TODO

* Move ModelBuilderExtensions to Common (CreateViews should search assemblyes for SQL code to apply)
* QueryTypes (SQL and View)
* Background tasks (common job processing)
* CQRS (create readonly context for aggregates)
* Logging: Serilog http://hamidmosalla.com/2018/02/15/asp-net-core-2-logging-with-serilog-and-microsoft-sql-server-sink/ 
* Structured logging https://stackify.com/what-is-structured-logging-and-why-developers-need-it/
* 2 kind of view models: list item and detailed
* Create another feature
* Authentication, authorization
* Custom headers

## DONE

* add more queries
* QueryExtensions
* Pagination
* Middleware
* Transaction
* Make EventDispatcher work
* Response handling
* Validation: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-2.2#rerun-validation
