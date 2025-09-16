.NET Clean Architecture Template
Готовый к использованию шаблон для создания микросервисов на .NET с архитектурой Clean Architecture.

Николай Сыряев
👨‍💻 GitHub: @Zoron87
💬 Telegram: @ZoronAds

📋 Что создаёт шаблон
При использовании команды dotnet new Capi -n MyProject создаётся следующая структура:

MyProject/
── README.md                 # Инструкции по проекту
── MyProject.API/            # Слой Web API
── MyProject.Application/    # Бизнес-логика
── MyProject.Domain/         # Доменные модели
── MyProject.Infrastructure/ # Данные и внешние сервисы


🚀 Быстрый старт
1. Добавить источник GitHub Packages
Создайте Personal Access Token на GitHub с правами
read:packages и выполните:

Шаблон
dotnet nuget add source https://nuget.pkg.github.com/<GITHUB_USERNAME>/index.json \
  --name <CUSTOM_NUGET_NAME> \
  --username <YOUR_GITHUB_USERNAME> \
  --password <YOUR_PERSONAL_ACCESS_TOKEN> \
  --store-password-in-clear-text
  
Конкретный пример
dotnet nuget add source https://nuget.pkg.github.com/Zoron87/index.json \
  --name github-Zoron87 \
  --username Zoron87 \
  --password PCH2Y60YqR7qg8lfy1ZcjCP3BQ41yr \
  --store-password-in-clear-text
  
2. Установить шаблон
dotnet new install Zoron.cleanarchitecture.template
3. Использовать шаблон
# Создать новый проект
dotnet new Capi -n MyMicroservice

# Перейти в проект и запустить
cd MyMicroservice
dotnet build
dotnet run --project MyMicroservice.API

🛠️ Управление шаблоном
Проверить установку:
dotnet new list
Обновить шаблон:
dotnet new install Zoron87.cleanarchitecture.template --force
Удалить шаблон:
dotnet new uninstall Zoron87.cleanarchitecture.template
Удалить источник:
dotnet nuget remove source github-Zoron87
📚 Дополнительные команды
# Посмотреть все источники NuGet
dotnet nuget list source
📦 Публикация новых версий (для автора)
Внесите изменения в шаблон:

Отредактируйте файлы в working/content/Capi/
Обновите PackageVersion в Template.csproj
Закоммитьте и создайте тег:

git add .
git commit -m "Update template: добавлена новая функциональность"
git push origin main

# Создать и запушить тег версии
git tag v1.0.0
git push origin v1.0.0
Автоматическая публикация
GitHub Actions автоматически опубликует пакет в GitHub Packages при создании тега.

Проверить публикацию: Actions → статус workflow, Packages → новая версия.

🔧 Локальная разработка шаблона
# Клонировать репозиторий
git clone https://github.com/Zoron87/Catalog_Microservice_On_Net9.git
cd dotnet-clean-architecture-template

# Установить локально для тестирования
cd working
dotnet new install .

# Тестировать изменения
dotnet new Capi -n TestProject

# Удалить локальную версию
dotnet new uninstall "/полный/путь/к/working"
Подготовка
▸ dotnet --version
9.0.102
📋 Создание проекта
# В папке шаблона (Capi)
dotnet new web -n "Capi.API"   
dotnet new classlib -n "Capi.Domain"  
dotnet new classlib -n "Capi.Application"  
dotnet new classlib -n "Capi.Infrastructure"

# В корне репозитория
dotnet new gitignore   
⚒️ Как прописать зависимости
# Из проекта Capi.API
dotnet add reference "../Capi.Application"
dotnet add reference "../Capi.Infrastructure"

# Из проекта Capi.Application
dotnet add reference "../Capi.Domain" 

# Проект Capi.Domain не имеет внешних зависимостей

# Из проекта Capi.Infrastructure
dotnet add reference "../Capi.Application"

🛠️ Зависимости для функционирования
Swagger
Установка библиотеки:
dotnet add package "Swashbuckle.AspNetCore" --version "9.0.3"
Регистрация сервисов:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Конфигурация приложения:
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
Для использования IServiceCollection
dotnet add package "Microsoft.Extensions.DependencyInjection.Abstractions" --version "9.0.7"
Для использования IConfiguration
dotnet add package "Microsoft.Extensions.Configuration" --version "9.0.7"




