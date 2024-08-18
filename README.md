1. Клонируйте репозиторий на свой компьютер:
  git clone https://github.com/username/repository.git
  cd repository

2. Настройка строки подключения к базе данных
   1. Откройте проект в Visual Studio.
   2. Перейдите к файлу appsettings.json.
   3. Найдите секцию ConnectionStrings и отредактируйте строку подключения к базе данных
    "ConnectionStrings": {
    "DefaultConnection": "Server=YourServerName;Database=FinalDentistry;Trusted_Connection=True;"
}

3. Применение миграций к базе данных
   Update-Database
4. Запуск приложения
  
