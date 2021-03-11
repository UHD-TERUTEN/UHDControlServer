# UHD Control Server
파일 접근 환경을 중앙에서 관리하는 서버

## Features
- 에이전트 프로그램이 설치된 컴퓨터의 파일 접근 차단 로그 및 시스템 로그 관리
- 관리자 판단에 의해 차단된 파일 접근 허용
- 화이트리스트 버전 관리

## GoogleTest
![210311](https://user-images.githubusercontent.com/31408641/110795376-199a4280-82ba-11eb-9dda-a44d2b11186e.png)

## Development Environment
- dotnet 5.0.x SDK
- ASP.NET Core Runtime 5.0.x
- dotnet EntityFramework (dotnet ef)
- yarn

## Usage
1. yarn dependency 설치
``` powershell
$ cd <project_directory>/ClientApp
$ yarn install
```
2. sqlite database 생성 (더미 데이터)
``` powershell
$ dotnet tool install --global dotnet-ef
$ cd <project_directory>
$ dotnet ef migrations add [migration_name]
$ dotnet ef database update
```
3. 서버 애플리케이션 실행
``` powershell
$ cd <project_directory>
$ dotnet watch run
```
``` powershell
$ cd <project_directory>/ClientApp
$ yarn run dev 
```
또는 Visual Studio에서 ctrl+f5   
(오류 메시지 무시하고 일정 시간 뒤 새로고침)

## Backend API
### file-access-reject-log
|Implemented|Method|URL|Example|
|--|--|--|--|
|Yes|GET|api/file-access-reject-log?page={page}|curl -X GET http://localhost:50598/api/file-access-reject-log?page=1|
|Yes|GET|api/file-access-reject-log/{id}|curl -X GET http://localhost:50598/api/file-access-reject-log/1|
|Yes|GET|api/file-access-reject-log/{id}/inquiries/{inquiry-id}|curl -X GET http://localhost:50598/api/file-access-reject-log/1/inquiries/1|
|Yes|PUT|api/file-access-reject-log/{id}|curl -X PUT http://localhost:50598/api/file-access-reject-log -H "Content-Type: application/json" -d '{"id":1,"agentId":99,"dateTime":"2021-02-09T00:00:00.0000000","programName":"pn","details":"d","isAllowed":true,"inquiries":[]}'|

### whitelist
|Implemented|Method|URL|Example|
|--|--|--|--|
|Yes|GET|api/whitelist?page={page}|curl -X GET http://localhost:50598/api/whitelist?page=1|
|Yes|GET|api/whitelist?version={version}|curl -X GET http://localhost:50598/api/whitelist?version=1.0.1|
|Yes|GET|api/whitelist/{id}|curl -X GET http://localhost:50598/api/whitelist/1|
|Yes|GET|api/whitelist/latest|curl -X GET http://localhost:50598/api/whitelist/latest|

### system-log
|Implemented|Method|URL|Example|
|--|--|--|--|
|Yes|GET|api/system-log?page={page}|curl -X GET http://localhost:50598/api/system-log?page=1|
|Yes|GET|api/system-log/{id}|curl -X GET http://localhost:50598/api/system-log/1|

> powershell 환경에서는 [Invoke-WebRequest](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/invoke-webrequest?view=powershell-7.1) 참조
