## kurulum
Entity framework cli arac�n�n kurulu oldu�undan emin olun.
`
dotnet ef
`
kurulu değil ise
`
dotnet tool install --global dotnet-ef
`
kurulu ise güncellemek için;
`
dotnet tool update --global dotnet-ef
`

Model değişiklikleri için;
`
dotnet ef migrations remove
dotnet ef migrations add -s ..\Loan.Service.Api InitialMigration --verbose
`
Veritananı değişikliklerini güncellemek için;
`
dotnet ef database update
`

##problem
Case g�rmek i�in [buraya] t�klay�n.(use-case.md)
