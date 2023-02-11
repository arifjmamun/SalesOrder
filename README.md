## To add migration, run from solution directory
```bash
dotnet ef migrations add Initial --project SalesOrder.Data --startup-project SalesOrder.API
```

## To remove migrations, run from solution directory
```bash
dotnet ef migrations remove --project SalesOrder.Data --startup-project SalesOrder.API
```

## To update database, run from solution directory
```bash
dotnet ef database update --project SalesOrder.Data --startup-project SalesOrder.API
```

## To test the API services run
```bash
dotnet test
```