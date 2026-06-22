# 🎮 GameBot API
 
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/postgresql-4169e1?style=for-the-badge&logo=postgresql&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white)
 
Бекенд на **ASP.NET Core**, що слугує хабом для ігрового чат-бота (Telegram/Discord). API агрегує інформацію про ігри з декількох зовнішніх джерел і керує базою даних користувачів.
 
---
 
## ✨ Можливості
 
- **Пошук ігор** — інтеграція з [RAWG API](https://rawg.io/apidocs): назва, дата релізу, рейтинг, платформи, обкладинка.
- **Відстеження знижок** — інтеграція з [CheapShark API](https://apidocs.cheapshark.com/): актуальна ціна та ціна без знижки в Steam, Epic Games, GOG та інших.
- **Безкоштовні ігри** — 5 найновіших ігор з [FreeToGame API](https://www.freetogame.com/api-doc).
- **Управління користувачами** — реєстрація, перевірка та збереження останньої переглянутої гри у PostgreSQL.
---
 
## 🛠 Стек технологій
 
| Компонент | Технологія |
|---|---|
| Фреймворк | ASP.NET Core Web API (C#) |
| База даних | PostgreSQL + Npgsql |
| HTTP-клієнт | RestSharp |
| JSON | Newtonsoft.Json |
| Конфігурація | DotNetEnv |
| Документація | Swagger / OpenAPI |
 
---
 
## 🚀 Запуск проекту
 
### 1. Клонування репозиторію
 
```bash
git clone https://github.com/valmt7/NewsApi.git
cd NewsApi
```
 
### 2. Налаштування змінних оточення
 
Скопіюй `.env.example` і заповни своїми даними:
 
```bash
cp .env.example .env
```
 
Вміст `.env`:
 
```env
RAWG_API_KEY=твій_ключ_rawg
SQL_Host=localhost
SQL_Username=postgres
SQL_Password=твій_пароль
SQL_Database=gamebot
```
 
> Отримати безкоштовний ключ RAWG: [rawg.io/apidocs](https://rawg.io/apidocs)
 
### 3. Створення бази даних
 
Підключись до PostgreSQL і виконай:
 
```sql
CREATE DATABASE gamebot;
 
\c gamebot
 
CREATE TABLE "users" (
    "id"       VARCHAR(255) PRIMARY KEY,
    "lastgame" INT NOT NULL DEFAULT -1
);
```
 
### 4. Запуск
 
```bash
cd NewsApi
dotnet run
```
 
Swagger UI буде доступний за адресою: `https://localhost:{port}/swagger`
 
---
 
## 📡 API Endpoints
 
### Ігри
 
| Метод | Endpoint | Опис |
|---|---|---|
| `GET` | `/api/game/find?gameName={name}` | Пошук гри за назвою (RAWG + CheapShark) |
| `GET` | `/api/game/fivegames?userId={id}` | 5 найновіших безкоштовних ігор |
 
**Приклад відповіді** `GET /api/game/find?gameName=Minecraft`:
 
```json
{
  "id": 22509,
  "name": "Minecraft",
  "released": "2011-11-18",
  "backgroundImage": "https://...",
  "metacritic": 4.47,
  "platforms": [{ "platform": { "id": 4, "name": "PC" } }],
  "isDealsActive": true,
  "saleprice": "19.99",
  "normalPrice": "26.99",
  "thumb": "https://..."
}
```
 
### Користувачі
 
| Метод | Endpoint | Опис |
|---|---|---|
| `POST` | `/api/users/add?id={userId}` | Реєстрація нового користувача |
| `GET` | `/api/users/isreg?id={userId}` | Перевірка чи зареєстрований користувач |
| `PUT` | `/api/users/editgame?id={userId}&lastGameId={id}` | Оновлення останньої переглянутої гри |
| `GET` | `/api/users/get` | Список усіх користувачів |
 
---
 
## 📁 Структура проекту
 
```
NewsApi/
├── Controllers/
│   ├── Game.cs          # Ендпоінти для ігор
│   └── User.cs          # Ендпоінти для користувачів
├── Service/
│   ├── Game/
│   │   ├── IGame.cs     # Інтерфейс ігрового сервісу
│   │   └── Game.cs      # Логіка RAWG + CheapShark + FreeToGame
│   └── PGSQL/
│       ├── IPGSQL.cs    # Інтерфейс БД
│       ├── PGSQL.cs     # Запити до PostgreSQL
│       └── Constant.cs  # Рядок підключення
├── Objects/             # Моделі даних (DTO)
├── .env.example         # Шаблон змінних оточення
├── Program.cs
└── NewsApi.csproj
```
 
---
 
## 🔒 Безпека
 
- Усі чутливі дані (ключі API, пароль БД) зберігаються виключно у файлі `.env`, який додано в `.gitignore`.
- SQL-запити використовують параметризацію — захист від SQL-ін'єкцій.
---
 
## 📦 Залежності
 
```xml
<PackageReference Include="DotNetEnv" Version="*" />
<PackageReference Include="Newtonsoft.Json" Version="*" />
<PackageReference Include="Npgsql" Version="*" />
<PackageReference Include="RestSharp" Version="*" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="*" />
```
