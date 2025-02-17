# Kart Yönetim Uygulaması

Bu proje, kredi kartları ve banka kartları için temel CRUD (Create, Read, Update, Delete) işlemlerini gerçekleştiren bir uygulamadır. Proje, backend kısmı .NET (ASP.NET Core) ile geliştirilmiş olup, API endpoint’leri üzerinden veri işlemlerini yapmaktadır. Frontend kısmı ise React kullanılarak oluşturulmuştur.

>  **Not:** Proje halen geliştirme aşamasındadır. Gelişmiş hata yönetimi, responsive tasarım ve ek özellikler eklenmiştir. Yetkilendirme mekanizması uygulanmış olup, kullanıcı rolleri ve yetkiler belirlenmiştir.

---

## Proje Yapısı

Proje iki ana klasörden oluşmaktadır:

- **backend/**  
  .NET (ASP.NET Core) ile geliştirilmiş API uygulaması.  
  Çalışma Portu: [http://localhost:5283](http://localhost:5283)
  
- **frontend/**  
  React ile geliştirilmiş kullanıcı arayüzü.  
  Çalışma Portu: [http://localhost:3000](http://localhost:3000)

---

## Özellikler

### Backend (API)
- **Kart Listeleme:**  
  - Kredi kartları ve banka kartları ayrı ayrı listeleniyor.
- **Kart Ekleme:**  
  - Yeni kredi kartı ve banka kartı ekleme işlemleri (POST endpoint’leri).
- **Kart Düzenleme:**  
  - Mevcut kart bilgilerini güncelleyebileceğiniz endpoint’ler (PUT endpoint’leri).
- **Kart Silme:**  
  - Kartları ID’ye göre silebilmek için endpoint’ler (DELETE endpoint’leri).
- **Kullanıcı Oturum Yönetimi ve Yetkilendirme:**  
  - Kullanıcı giriş/çıkış işlemleri, token, rol ve izin bilgileri hem sessionStorage hem de Redis üzerinde saklanmaktadır.  
  - Redis, kullanıcı oturumlarını yönetmek ve önbellekleme işlemlerinde kullanılmaktadır. Oturum açıldıktan sonra, `user:{userName}` formatında key oluşturulur ve 1 saatlik TTL ile saklanır. Logout işleminde ilgili key silinir.

#### Örnek API Endpoint’leri

- **Kredi Kartları**
  - Listeleme: `GET http://localhost:5283/api/creditcards/all`
  - ID’ye Göre Getirme: `GET http://localhost:5283/api/creditcards/byId/{id}`
  - Ekleme: `POST http://localhost:5283/api/creditcards`
  - Güncelleme: `PUT http://localhost:5283/api/creditcards/updateById/{id}`
  - Silme: `DELETE http://localhost:5283/api/creditcards/deleteById/{id}`

- **Banka Kartları**
  - Listeleme: `GET http://localhost:5283/api/bankcards/all`
  - ID’ye Göre Getirme: `GET http://localhost:5283/api/bankcards/byId/{id}`
  - Ekleme: `POST http://localhost:5283/api/bankcards`
  - Güncelleme: `PUT http://localhost:5283/api/bankcards/updateById/{id}`
  - Silme: `DELETE http://localhost:5283/api/bankcards/deleteById/{id}`
 
- **Kullanıcı Giriş & Çıkış**
  - Giriş (Login):  
    `POST http://localhost:5283/api/login`
  - Çıkış (Logout):  
    `POST http://localhost:5283/api/logout`

### Frontend (React Uygulaması)

- **Kart Listeleme, Silme ve Düzenleme:**  
  - `CardsList.js` bileşeni ile kredi ve banka kartları listelenmekte, ilgili kartlar düzenlenip silinebilmektedir.
  - `EditCard.js` bileşeni ile seçilen kartın bilgileri düzenlenip güncellenebilmektedir.
  - `AddCard.js` bileşeni ile yeni kart ekleme işlemleri gerçekleştirilmektedir.

- **Navigasyon:**  
  - React Router ile farklı sayfalar arasında yönlendirme sağlanmaktadır.
  - ProtectedRoute bileşeni ile oturum açılmadan yetkisiz erişim engellenmektedir.

- **HTTP İstekleri:**  
  - Axios kütüphanesi kullanılarak API ile iletişim kurulmaktadır.

- **Kullanıcı Oturum Yönetimi ve Yetkilendirme:**  
  - Kullanıcı giriş/çıkış işlemleri, token, rol ve izin bilgileri hem sessionStorage hem de Redis üzerinde saklanmaktadır.
  - Redis, kullanıcı oturumlarını yönetmek ve önbellekleme işlemlerinde kullanılmaktadır. Oturum açıldıktan sonra, `user:{userName}` formatında key oluşturulur ve 1 saatlik TTL ile saklanır. Logout işleminde ilgili key silinir.
  - Yetkilendirme, kullanıcı rollerine göre belirlenen izinlerle yönetilmektedir.


#### **Yetkilendirme Rolleri ve İzinler**

**Role 0 - Şube Kullanıcısı:**
- `AuthController`
  - Login
  - Logout
- `CreditCardController`
  - GetAll
  - GetById
- `BankCardController`
  - GetAll
  - GetById

**Role 1 - Admin Kullanıcısı:**
- `AuthController`
  - Login
  - Logout
- `BankCardController`
  - GetAll
  - GetById
  - Create
  - UpdateById
  - DeleteById
- `CreditCardController`
  - GetAll
  - GetById
  - Create
  - UpdateById
  - DeleteById
---

## Veritabanı Yapılandırması ve Entity Framework Kullanımı

Backend tarafında, Entity Framework Core (EF Core) kullanılarak veritabanı işlemleri gerçekleştirilmekte ve veri modelleri üzerinden tablolar otomatik olarak oluşturulmaktadır.  
Proje, `appsettings.json` dosyasında yer alan bağlantı dizesi ile yerel SQL Server’a bağlanmaktadır:
    
    "DefaultConnection": "Server=localhost;Database=CardsDatabase;Trusted_Connection=True;TrustServerCertificate=True;"
    
> **Dikkat:**
Yukarıdaki bağlantı dizesi, yerel geliştirme ortamı içindir. Üretim ortamında veya farklı veritabanlarında çalışırken, bu ayarları kendi ortamınıza göre güncellemeniz gerekmektedir.

## Redis Kullanımı

Backend, kullanıcı oturum yönetimi ve performans iyileştirmeleri amacıyla Redis kullanmaktadır:

- **Oturum Verileri:**  
  - Kullanıcı giriş işlemi sonrası, oturum bilgileri (token, rol vb.) `user:{userName}` formatında Redis'e kaydedilir.  
  - Bu key, 1 saatlik TTL ile saklanır.  
  - Kullanıcı logout olduğunda ilgili key silinerek oturum verileri temizlenir.

- **Performans ve Cache:**  
  - İlerleyen aşamalarda, sık kullanılan verileri Redis üzerinde cache’lemek ve veritabanı sorgularını hızlandırmak amacıyla Redis kullanılabilir.  
  - Cache verilerine yönelik stratejiler (eviction, TTL vb.) proje ihtiyaçlarına göre konfigüre edilebilir.

## Kurulum ve Çalıştırma

### Backend

1. **Projeyi Klonlayın:**

   ```bash
   git clone https://github.com/Dawnfairy/Bank-Card-Management-System.git
   cd Bank-Card-Management-System/backend/BankCardProject
   ```

2. **Bağımlılıkları Yükleyin:**
   Proje klasörüne geçtikten sonra aşağıdaki komutu çalıştırarak `.csproj` dosyasında tanımlı olan tüm bağımlılıkları otomatik olarak yükleyebilirsiniz:

   ```bash
   dotnet restore
   ```

3. **Veritabanı Migrasyonlarını Uygulayın:**
   Eğer veritabanı migrasyonlarını oluşturmadıysanız aşağıdaki komutları çalıştırarak gerekli tabloları oluşturabilirsiniz:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

   Eğer proje içerisinde mevcut migrasyonlar bulunuyorsa sadece aşağıdaki komutu çalıştırarak en son yapılan değişiklikleri veritabanına uygulayabilirsiniz:

   ```bash
   dotnet ef database update
   ```

4. **Redis İçin Docker Kullanımı:**
   Redis'i çalıştırmak için aşağıdaki Docker komutunu kullanabilirsiniz:

   ```bash
   docker run --name redis-container -d -p 6379:6379 redis
   ```

   Eğer Redis'in otomatik başlatılmasını istiyorsanız:

   ```bash
   docker start redis-container
   ```

5. **Backend API'yi Başlatın:**
   Visual Studio üzerinden veya aşağıdaki .NET CLI komutunu kullanarak çalıştırın:
   
   ```bash
   dotnet run
   ```

   Backend, [http://localhost:5283](http://localhost:5283) adresinde çalışacaktır.
   
### Frontend

1. **Frontend Klasörüne Geçin:**

   ```bash
   cd ../frontend
   ```

2. **Gerekli Bağımlılıkları Yükleyin:**
   ```bash
   npm install
   ```

3. **Uygulamayı Başlatın:**
   ```bash
   npm start
   ```

   Uygulama, varsayılan olarak [http://localhost:3000](http://localhost:3000) adresinde çalışacaktır.





