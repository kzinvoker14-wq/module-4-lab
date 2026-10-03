# CSE 5032 — Разработка веб-сервисов
## Модуль 04. Лабораторная работа: Товары (EF Core, Repository, DTO, AutoMapper)

REST API для управления товарами (`Product`) на ASP.NET Core с базой данных
SQLite (EF Core, Code First), Repository-слоем, DTO, AutoMapper и единым
форматом ответа `ReturnResult`.

## Цепочка (как в задании)

```
Database → EF Core → Repository → AutoMapper → DTO → Controller → API Response
```

Все обращения к `AppDbContext` идут только через `ProductRepository` —
контроллер его вообще не видит.

## Формат ответа API

Строго по формату из задания:

```json
{
  "isSuccess": true,
  "result": { "id": 1, "name": "Ноутбук", "price": 350000 },
  "errorMessage": []
}
```

`IsSuccess` / `Result` / `ErrorMessage` в C# превращаются в `isSuccess` /
`result` / `errorMessage` в JSON автоматически — ASP.NET Core по умолчанию
переводит имена свойств в camelCase.

## Важный нюанс: ProductDto без Category

`ProductDto` намеренно не содержит `Category` (так и указано в задании) —
это показывает разницу между Entity (как товар хранится в БД) и DTO (что
реально видит клиент API). Из-за этого при создании товара через POST
`Category` всегда останется пустым — это не баг, а то, что должно быть
видно и объяснено на защите.

## Структура проекта

```
ProductsApi/
├── Controllers/ProductsController.cs
├── Entities/Product.cs
├── Dtos/ProductDto.cs
├── Repositories/IProductRepository.cs, ProductRepository.cs
├── Mapping/AutoMapperProfile.cs
├── Common/ReturnResult.cs
├── Data/AppDbContext.cs
├── Migrations/              ← появится после миграции, его пока нет в архиве
├── Program.cs
├── appsettings.json          ← здесь строка подключения к БД
└── ProductsApi.csproj
```

## Как запустить

```bash
cd ProductsApi
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet run
```

Swagger откроется сам, либо вручную `http://localhost:5000/swagger`.

## Эндпоинты

| HTTP   | Маршрут            | Назначение        |
| ------ | ------------------ | ------------------ |
| GET    | /api/products       | Все товары          |
| GET    | /api/products/{id}  | Один товар по Id    |
| POST   | /api/products       | Добавить товар      |
| PUT    | /api/products/{id}  | Изменить товар      |
| DELETE | /api/products/{id}  | Удалить товар       |

Пример тела запроса для POST/PUT:

```json
{
  "name": "Ноутбук",
  "price": 350000
}
```

## Порядок демонстрации через Swagger

1. **POST** `/api/products` — создай товар.
2. **GET** `/api/products` — получи список, убедись что товар там.
3. **GET** `/api/products/{id}` — получи один товар по Id.
4. **PUT** `/api/products/{id}` — измени его.
5. **DELETE** `/api/products/{id}` — удали.

В самом документе задания нет отдельного списка «что сдать» (в отличие от
прошлых работ) — судя по всему, упор на сам рабочий API и способность
объяснить цепочку Database → EF Core → Repository → AutoMapper → DTO.
На всякий случай возьми пару скриншотов из Swagger, как и раньше — лишним
не будет.
