Entity framework cli arac�n�n kurulu oldu�undan emin olun.
`
dotnet ef
`
kurulu de�il ise
`
dotnet tool install --global dotnet-ef
`
kurulu ise g�ncellemek i�in;
`
dotnet tool update --global dotnet-ef
`

Model de�i�iklikleri i�in
`
dotnet ef migrations remove
dotnet ef migrations add -s ..\Loan.Service.Api InitialMigration --verbose
`
Veritanan� g�ncellemek i�in
`
dotnet ef database update
`