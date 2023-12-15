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
cd ProjectFolder/loanapp/
dotnet ef migrations remove --project src\Loan.Domain.Repository\Loan.Domain.Repository.csproj --startup-project src\Loan.Service.Api\Loan.Service.Api.csproj --context Loan.Domain.Repository.Persistence.LoanDbContext --configuration Debug --force
dotnet ef migrations add --project src\Loan.Domain.Repository\Loan.Domain.Repository.csproj --startup-project src\Loan.Service.Api\Loan.Service.Api.csproj --context Loan.Domain.Repository.Persistence.LoanDbContext --configuration Debug Initial --output-dir Migrations
`


Veritananı değişikliklerini güncellemek için;
`
dotnet.exe ef database update --project src\Loan.Domain.Repository\Loan.Domain.Repository.csproj --startup-project src\Loan.Service.Api\Loan.Service.Api.csproj --context Loan.Domain.Repository.Persistence.LoanDbContext --configuration Debug
`
Veritabanını silmek için;
`
dotnet ef database drop --project src\Loan.Domain.Repository\Loan.Domain.Repository.csproj --force
`