# Error radar
Расширенный сервис мониторинга ошибок для информационной системы
## Описание
Веб - приложение предназначено для управления инцидентами, возникающими при работе программного обеспечения, а также для анализа проблем, сформированных на основе инцидентов.
## Основные функции
* Автоматическое получение уведомлений об ошибках из сторонней информационной системы.
* Формирование записей инцидентов на основе полученных ошибок в соответствии с моделью инцидента.
* Возможность управления инцидентами: изменение статуса, добавление комментариев по решению.
* Возможность фильтровать инциденты по степени их критичности и природе возникновения.
* Формирование записей о проблемах с количеством инцидентов по этим проблемам в различных статусах.
* Построение диаграммы для отслеживания количества инцидентов по разным проблемам в заданные периоды времени.
* Оперативное оповещение главных лиц предприятия о возникновении сбоя в информационной системе через телеграмм-бот.
```
@ErrorRadar_bot
```
## Инструкция по запуску приложения
1. Установите Docker
2. Склонируйте репозиторий с проектом на свое устройство с помощью команды:
```
git clone https://github.com/Eva0220/ErrorRadar
```
3. Откройте файл docker-compose.yml, который находится в папке с исходным кодом проекта.
4. В конфигурации сервиса Kafka укажите порт, по которому ваша система будет взаимодействовать с Kafka. Для этого измените в секции ports первый номер порта
5. Сохраните отредактированный файл
6. Далее необходимо настроить название темы и группы, в которую ваша система будет отправлять сообщения. Для этого перейдите из корневой папки проекта с исходным кодом в папку Ekas.Common и откройте файл AppData
7. Измените значение свойств Topic и GroupId в соответствие с названием вашей темы и идентификатором группы соответственно
8. Откройте Docker
9. Вернитесь в корневую папку проекта с исходным кодом.
10. В текущем каталоге выполните команду:
```
docker-compose up -d 
```
11. Проверьте состояние сервисов выполнив команду:
```
docker ps -a
```
