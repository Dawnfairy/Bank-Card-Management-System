# Kart Yönetim Uygulaması

Bu proje, kredi kartları ve banka kartları için temel CRUD (Create, Read, Update, Delete) işlemlerini gerçekleştiren bir uygulamadır. Proje, backend kısmı .NET (ASP.NET Core) ile geliştirilmiş olup, API endpoint’leri üzerinden veri işlemlerini yapmaktadır. Frontend kısmı ise React kullanılarak oluşturulmuştur.

> **Not:** Proje halen geliştirme aşamasındadır. Gelecekte yetkilendirme, gelişmiş hata yönetimi, responsive tasarım ve ek özellikler eklemeyi planlıyorum.

## Proje Yapısı

Proje iki ana klasörden oluşmaktadır:
- **backend/**  
  .NET (ASP.NET Core) ile geliştirilmiş API uygulaması.  
  Çalışma Portu: [http://localhost:5283](http://localhost:5283)
- **frontend/**  
  React ile geliştirilmiş kullanıcı arayüzü.  
  Çalışma Portu: [http://localhost:3000](http://localhost:3000)

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

### Frontend (React Uygulaması)
- **Kart Listeleme ve Silme:**  
  - `CardsList.js` bileşeni ile kredi ve banka kartları listelenmekte ve ilgili kartlar düzenlenip silinebilmektedir.
- **Kart Düzenleme:**  
  - `EditCard.js` bileşeni ile seçilen kartın bilgileri düzenlenip güncellenebilmektedir.
- **Navigasyon:**  
  - React Router ile farklı sayfalar arasında yönlendirme sağlanmaktadır.
- **HTTP İstekleri:**  
  - Axios kütüphanesi kullanılarak API ile iletişim kurulmaktadır.
 
## Veritabanı Yapılandırması ve Entity Framework Kullanımı

Backend tarafında, Entity Framework Core (EF Core) kullanılarak veritabanı işlemleri gerçekleştirilmekte ve veri modelleri üzerinden tablolar otomatik olarak oluşturulmaktadır.  
Proje, `appsettings.json` dosyasında yer alan bağlantı dizesi ile yerel SQL Server’a bağlanmaktadır:
    
    "DefaultConnection": "Server=localhost;Database=CardsDatabase;Trusted_Connection=True;TrustServerCertificate=True;"
    
> **Dikkat:**
Yukarıdaki bağlantı dizesi, yerel geliştirme ortamı içindir. Üretim ortamında veya farklı veritabanlarında çalışırken, bu ayarları kendi ortamınıza göre güncellemeniz gerekmektedir.

## Kurulum ve Çalıştırma

### Backend

1. **Projeyi Klonlayın:**

   ```bash
   git clone https://github.com/Dawnfairy/Bank-Card-Management-System.git
   cd Bank-Card-Management-System/backend

2. **Gerekli Bağımlılıkları yükleyin:**
   
   Visual Studio veya .NET CLI kullanarak projeyi açın. Gerekli NuGet paketlerini yükleyin.

3. **Backend API'yi Başlatın:**
   Visual Studio üzerinden veya aşağıdaki .NET CLI komutunu kullanarak çalıştırın:
   ```bash
   dotnet run

  API, http://localhost:5283 adresinde çalışacaktır.
### Frontend
1. **Frontend Klasörüne Geçin:**

   ```bash
   cd ../frontend

2. **Gerekli Bağımlılıkları yükleyin:**
   
   ```bash
   npm install

3. **Uygulamayı Başlatın:**
   ```bash
   npm start
  Uygulama, varsayılan olarak http://localhost:3000 adresinde çalışacaktır.



