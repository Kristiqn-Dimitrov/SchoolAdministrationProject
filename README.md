# SchoolLibrary (ASP.NET Core MVC, EF Core Code First, SQL Server)

## Стартиране
1) Инсталирайте .NET 8 SDK и SQL Server (или SQL Express).
2) Отворете `appsettings.json` и коригирайте ConnectionString при нужда.
3) В конзола в папката на проекта:

```bash
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

## Демо админ (seed)
- Email: admin@school.local
- Password: Admin!23456

След първия вход сменете паролата (по избор) или изтрийте RegisterAdmin екрана.

## Функционалност
- Admin login/logout
- CRUD: Categories, Books, Users (Student/Teacher)
- Loans: create loan, return loan, active/history


## Регистрация
- /Account/RegisterAdmin (само за демо/учебен проект)


## Админи
- След вход като Admin: меню **Админи** → създай нови администратори.



## ✅ NEW (vNext): Портал + „Учебен график“ (rule-based генератор) + „Училищни ресурси“

### Модулни Layout-и
След избор на модул (Библиотека/График/Ресурси) интерфейсът използва отделен layout и странично меню само с функциите за този модул.

### Нови таблици
Добавени са:
- `Resources`
- `Rooms`, `SchoolClasses`, `Subjects`, `ScheduleEntries`
- `ClassSubjectRequirements` (часове/седмица)
- `TeacherSubjects` (кой учител кои предмети води)
- `TeacherAvailabilities` (ограничения/забранени часове)

Ако вече имаш база данни от стара версия, изпълни:

```bash
dotnet ef migrations add AddScheduleRulesAndGenerator
dotnet ef database update
```

### Как да генерираш график
1) Въведи **Класове**, **Кабинети**, **Предмети**
2) Въведи **Изисквания (часове/седмица)**
3) Въведи **Учител→Предмет** и (по избор) **Ограничения**
4) От меню **График → Генерирай** настрои условията и натисни **Генерирай**

