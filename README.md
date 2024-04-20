# ДЗ по автотестам
**Selenium C#**
IDE VisualStudio 2022

В Setup используются мои Cookie для авторизации.

За неимением доступа к БД, в паре методов вызываются JS скрипты с API запросами
* CopyFile.js - очищает папку My/Отсюда
* ClearFolder.js - копирует файл avatar.gif из My/Setup в My/Отсюда
* ChangeDescription.js - изменяет описание сообщества HW_TestAPI на SETUP

## Тесты
- [x] Авторизация на сайт - Autorization()
- [x] Переход на страницу сообществ - NavigateToCommunities()
- [x] Появление заглушки (папка пуста) в разделе Файлы - EmptyStateDir()
- [x] Корректное описание сообщества после изменения - ChangeCommunityDesciption()
- [x] Новое обсуждение появляется в списке после создания - AddDiscussion()
