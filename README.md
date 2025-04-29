
# Akademik Personel Başvuru Sistemi (APBS)

Bu proje, üniversitemizin akademik personel kadro ilanlarının başvuru ve değerlendirme süreçlerini elektronik ortamda yönetmek için geliştirilmiş bir web uygulamasıdır. Uygulama .NET 9.0 ile yazılmış, Entity Framework Core kullanılarak veri erişimi sağlanmış ve dosya yüklemeleri AWS S3 üzerinde depolanacak şekilde konfigüre edilmiştir. Ayrıca SMTP üzerinden e-posta gönderimi de desteklenmektedir.

------------


## Ön Koşullar

- .NET 9.0 SDK
- SQL Server 2019+
- AWS hesabı ve S3 bucket (dosya yüklemeleri için)
- SMTP e-posta sunucusu erişimi
- Tercihe bağlı: Visual Studio 2022 (17.x) veya VS Code

------------


## Proje Kurulumu

- Depoyu klonlayın:

        git clone https://github.com/umutgulfidan/Akademik-Personel-Basvuru-Sistemi-APBS-.git
        cd Akademik-Personel-Basvuru-Sistemi-APBS-

- Çözüm dosyasını açın:
 	1. Visual Studio kullanıyorsanız .sln dosyasını çift tıklayın.
 	2. VS Code kullanıyorsanız klasörü açın.

- Gerekli NuGet paketlerini geri yükleyin:
    dotnet restore

------------


## Çevresel Değişkenler (.env)
Projede AWS ve SMTP ayarlarını yönetmek için projenin 'WebAPI/bin/Debug/net9.0/' klasörüne bir .env dosyası oluşturun ya da mevcutsa env.example dosyasını kopyalayın ve geçerli belirtilen alanları doldurun.

    AWS_ACCESS_KEY=<YourAWSAccessKey>
    AWS_SECRET_KEY=<YourAWSSecretKey>
    AWS_BUCKET_NAME=<YourS3BucketName>
    AWS_REGION=<YourAWSRegion>
    SMTP_HOST=<smtp.example.com>
    SMTP_PORT=<587>
    SMTP_EMAIL=<your-email@example.com>
    SMTP_PASSWORD=<YourEmailPassword>
    SMTP_ENABLE_SSL=<true>

------------


### Veritabanı Kurulumu ve Migration

`Context.cs` dosyasındaki `OnConfiguring` metodunda yer alan bağlantı dizesini yapılandırın:

```csharp
optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=APBS_Database;Trusted_Connection=true");
```

Migration işlemleri için başlangıç projesini `DataAccess` olarak ayarlayın ve aşağıdaki komutları terminalden ya da Package Manager Console (PMC) üzerinden çalıştırın:

#### Migration ekleyin (ilk sefer için):

```bash
dotnet ef migrations add InitialCreate --project DataAccess/DataAccess.csproj
```

#### Veritabanını güncelleyin:

```bash
dotnet ef database update --project DataAccess/DataAccess.csproj
```

> **Not:** `--project` parametresi, Entity Framework komutlarının hangi katmanda çalıştırılacağını belirtmek içindir. Gerekirse yolunu kendi projenize göre güncelleyebilirsiniz.

------------


### Frontend Kurulumu

Frontend için [APBS-Frontend](https://github.com/Utku-Genc/APBS-Frontend) adresine gidin ve talimatları takip edin.

------------

## Proje Raporu

Proje raporuna [Rapor](https://github.com/user-attachments/files/19960362/yazlab_rapor.pdf) linkinden erişebilirsiniz.



### Hazırlayanlar

- Ahmet Efe Tosun - ahefto@gmail.com
- Umut Gülfidan - umutgulfidan41@gmail.com
- Utku Genç - utkugenc2003@gmail.com

------------


