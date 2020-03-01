# Celo Demo
---
![standard-readme compliant](https://img.shields.io/badge/Test-passing-brightgreen)
![standard-readme compliant](https://img.shields.io/static/v1?label=ASP.NET-core-API&message=V2.2&color=brightgreen)

ASP.NET core Web application.

functions: Fetch users from external website, CRUD users.

## How it works

1. Use Package Manager Console to generate SQL
```
Add-Migration {DBname}
Update-Database
```
2. Click Start

3. Ways of get user list 

| Method | URL                                                          | Description                                                  |
| ------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| Get    | https://localhost:44332/user                                 | default 10 users                                             |
| Get    | https://localhost:44332/user?number=5                        | get top 5 users                                              |
| Get    | https://localhost:44332/user?lastname=oli                    | filter last name                                             |
| Get    | https://localhost:44332/user?firstname=oli                   | filter first name                                            |
| Get    | https://localhost:44332/user?firstname=oli&lastname=oli&number=10 | filter first name and last name, get top 10 users. If users less than 10, return all users |
| Get    | https://localhost:44332/user/2                               | get user whose id=2                                          |
| Put    | https://localhost:44332/user/2                               | update user whose id=2. The format of writing require body refer to the result of Get methods. |
| Delete | https://localhost:44332/user/2                               | delete user whose id=2                                       |
| Get    | https://localhost:44332/user/index                           | add 5 users (from external website) to database each time when this method is called. |

## Architecture for folders
1. Controllers
2. Data: DB context
3. Entities: classes
4. Models: view model classes
5. Profile: autoMapper configuration
6. Services: Interface and the implemented class



