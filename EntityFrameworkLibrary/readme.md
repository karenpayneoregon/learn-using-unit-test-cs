# About

Provides data access for unit test in SalesUnitTestProject.

:grey_exclamation: Run `script.sql` to generate database, table and populate table.


[EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools) was used to generate classes.

---

# Validation queries for test

```sql
SELECT Id, 
       SaleDate, 
       ShipCountry
FROM dbo.Sales;

DECLARE @LastWeek NVARCHAR(10)= '2021-06-18';

SELECT Id, 
       SaleDate, 
       ShipCountry
FROM dbo.Sales
WHERE CONVERT(DATE, SaleDate) > CAST(@LastWeek AS DATE)
      AND dbo.Sales.ShipCountry = 1;
```