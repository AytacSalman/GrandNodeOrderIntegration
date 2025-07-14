# 🚀 GrandNodeOrderIntegration
Bu proje, .NET Core ile geliştirilmiş bir "Background Worker Service" uygulamasıdır. Trendyol Order modelini GrandNode2-2.0.0 E-Ticaret altyapısına entegre etmek için tasarlanmıştır.

## 📦 Teknolojiler
- .NET Core Worker Service (`Microsoft.Extensions.Hosting`)
- Dependency Injection (DI)
- AppSettings (IConfiguration)
- Logging (Console)
- AutoMapper

---

## 🛠️ Kurulum

### 1. Kaynak Kodu Klonla

```bash
git clone https://github.com/AytacSalman/GrandNodeOrderIntegration.git
cd GrandNodeOrderIntegration

MyWorkerService/
├── Program.cs           --> Giriş noktası
├── Worker.cs            --> Arka plan iş mantığı
├── Services/            --> Özel servisler
├── Models/              --> Veri modelleri
├── Mappers/             --> Model maplemeleri
├── appsettings.json     --> Yapılandırmalar

📬 İletişim
Herhangi bir soru ya da öneri için aytacslmn@gmail.com adresinden iletişime geçebilirsiniz.

