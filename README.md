# 🎮 GameBot API (NewsApi)

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/postgresql-4169e1?style=for-the-badge&logo=postgresql&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white)

**GameBot API** — це потужний бекенд на ASP.NET Core, створений для обслуговування ігрового чат-бота (Telegram/Discord). API виступає в ролі єдиного хаба: воно агрегує інформацію про ігри, шукає найкращі знижки та керує базою даних користувачів.

## ✨ Ключові можливості

* 📖 **Енциклопедія ігор:** Інтеграція з [RAWG API](https://rawg.io/apidocs) для пошуку детальної інформації про ігри (рейтинги, дати релізу, платформи).
* 💸 **Мисливець за знижками:** Інтеграція з [CheapShark API](https://apidocs.cheapshark.com/) для відстеження найнижчих цін у цифрових магазинах (Steam, Epic Games, GOG тощо).
* 👥 **Керування користувачами:** Власна база даних на PostgreSQL для реєстрації користувачів та відстеження їхньої останньої переглянутої гри.
* 🔒 **Безпека:** Робота зі змінними оточення через `.env` файли (жодних "злитих" ключів у репозиторії).

## 🛠 Стек технологій

* **Фреймворк:** ASP.NET Core Web API (C#)
* **База даних:** PostgreSQL (через `Npgsql`)
* **HTTP Клієнти:** `RestSharp`
* **Робота з JSON:** `Newtonsoft.Json`
* **Конфігурація:** `DotNetEnv`
* **Документація:** Swagger / OpenAPI

## 🚀 Швидкий старт (Встановлення та запуск)

### 1. Клонування репозиторію
```bash
git clone [https://github.com/NewsApi/NewsApi.git](https://github.com/valmt7/NewsApi.git)
cd NewsApi
