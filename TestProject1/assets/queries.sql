/*
    Queries for unit test validation
*/
USE NorthWind2020;

SELECT Customers.CompanyName, 
       Countries.Name
FROM Customers
     INNER JOIN Countries ON Customers.CountryIdentifier = Countries.CountryIdentifier
GROUP BY Customers.CompanyName, 
         Countries.Name
ORDER BY Countries.Name;

SELECT C.[Name], COUNT(C.Name) AS CountryCount FROM Customers AS Cust INNER JOIN Countries AS C ON Cust.CountryIdentifier = C.CountryIdentifier GROUP BY C.[Name] ORDER BY C.[Name];