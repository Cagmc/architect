# architect
.Net core achitecture playground

## In progress
* 2 kind of view models: list item and detailed
* New base for query request filters
* Different service interface variants (w/o query)
* Interface review for query services

## TODO

* Type of Id should come from template => support of different Id types
* Implement a real feature (Book catalog)
* Host in Azure
* Angular UI
* Store SQL scripts in file system, and view names in a correct place
* Handling stored procedures (queries and midifying operations; find SP query template)
* QueryTypes (SQL and View)
* Background tasks (common job processing)
* CQRS (create readonly context for aggregates)
* Logging: Serilog http://hamidmosalla.com/2018/02/15/asp-net-core-2-logging-with-serilog-and-microsoft-sql-server-sink/ 
* Structured logging https://stackify.com/what-is-structured-logging-and-why-developers-need-it/
* Create another feature
* Authentication, authorization
* Custom headers
* C# 3.0 upgrade
* file (picture) handling

## DONE

* add more queries
* QueryExtensions
* Pagination
* Middleware
* Transaction
* Make EventDispatcher work
* Response handling
* Validation: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-2.2#rerun-validation
