Entity framework cli aracýnýn kurulu olduðundan emin olun.
`
dotnet ef
`
kurulu deðil ise
`
dotnet tool install --global dotnet-ef
`
kurulu ise güncellemek için;
`
dotnet tool update --global dotnet-ef
`

Model deðiþiklikleri için
`
dotnet ef migrations remove
dotnet ef migrations add -s ..\Loan.Service.Api InitialMigration --verbose
`
Veritananý güncellemek için
`
dotnet ef database update
`