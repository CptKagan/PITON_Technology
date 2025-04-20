# Görev Yöneticisi

Bu proje, kullanıcıların günlük, haftalık veya aylık görevlerini yönetmesini sağlayan bir Web API uygulamasıdır.

## Kullanılan Teknolojiler

- **ASP.NET Core Web API**
- **Entity Framework Core (MSSQL)**
- **JWT Authentication**
- **Swagger**

## Özellikler

- Kullanıcı kayıt ve giriş işlemleri
- JWT ile kimlik doğrulama
- Görev oluşturma, listeleme, güncelleme ve silme
- Günlük, haftalık ve aylık görev periyotları
- Swagger UI ile API dokümantasyonu

## Kurulum
### Projeyi klonlayın:
```bash
git clone https://github.com/CptKagan/PITON_Technology
cd PITON_Technology
```

### Gerekli Bağımlılıkları Yükleyin
- https://dotnet.microsoft.com/en-us/download .NET 9.0 kurulu olmalıdır.
- https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads MSSQL Server kurulu olmalıdır.

### appsettings.json dosyanızı güncelleyin
- Yeni bir veri tabanı oluşturun ve bağlantınızı aşağıdaki gibi güncelleyin:
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PITON_Project;Trusted_Connection=True;Encrypt=False;"
  },
```

### Veritabanı Oluşturma
- Proje çalıştırıldığında EF Core otomatik olarak veritabanını oluşturur.

### Projeyi Çalıştırın
```bash
dotnet build
dotnet run
```

## API Kullanımı

Swagger üzerinden kullanabilirsiniz:
```bash
http://localhost:port
```

### Başlangıç Admin Kullanıcısı
- Projede otomatik olarak aşağıdaki admin hesabı oluşturulmaktadır: 
```bash
username: admin
password: Admin123
Role: Admin
```

### Hesap Oluşturma
```json
POST /api/user/register
Content‑Type: application/json

{
  "userName":"test1",
  "email":"a@b.com",
  "password":"12345"
}

{
  "success": true,
  "message": "Account created successfully!"
}
```

### Giriş Yapma
```json
POST /api/user/login
Content‑Type: application/json

{
  "userName":"test1",
  "password":"12345"
}

{
  {token}
}
```

### Authorize
- Swagger arayüzüne giriş yaptıktan sonra, sağ üst köşede "Authorize" adlı bir buton görülecektir.
- Başarıyla giriş yaptıktan sonra size verilen token'ı aşağıda beliren "Value:" kısmına , aşağıda verilen örnek ile doldurmanız halinde korumalı endpointlere erişim sağlayabileceksiniz.
```bash
Example: "Bearer {token}" -> Authorize
```

### Görevleri Listeleme
- pageNumber: Hangi sayfanın getirileceği bilgisi
- pageSize: Bir sayfada kaç görev listeleneceği bilgisi
- period: 0 -> Günlük görevler | 1 -> Haftalık görevler | 2 -> Aylık görevler
- completed: Tamamlanmış yada tamamlanmamış görev bilgisi
```bash
/api/task/gettasks?pageNumber=1&pageSize=10&period=1&completed=false
```