# UHD Control Server
파일 접근 환경을 중앙에서 관리하는 서버

## Features
- 에이전트 프로그램이 설치된 컴퓨터의 파일 접근 차단 로그 및 시스템 로그 관리
- 관리자 판단에 의해 차단된 파일 접근 허용
- 화이트리스트 버전 관리

## Development Environment
- dotnet 5.0.x SDK
- ASP.NET Core Runtime 5.0.x
- dotnet EntityFramework (dotnet ef)

## Usage
1. sqlite database 생성 (더미 데이터)
``` powershell
$ dotnet tool install --global dotnet-ef
$ cd <project_directory>
$ dotnet ef migrations add [migration_name]
$ dotnet ef database update
```
2. 서버 애플리케이션 실행
``` powershell
$ dotnet watch run
```

## Backend API
### file-access-reject-logs
|Implemented|Method|URL|Example|
|--|--|--|--|
|Yes|GET|api/file-access-reject-logs?page={page}|curl -XGET http://localhost:50598/api/file-access-reject-logs?page=1|
|Yes|GET|api/file-access-reject-logs/{id}|curl -XGET http://localhost:50598/api/file-access-reject-logs/1|
|Yes|GET|api/file-access-reject-logs/{id}/inquiries/{inquiry-id}|curl -XGET http://localhost:50598/api/file-access-reject-logs/1/inquiries/1|
|Yes|PUT|api/file-access-reject-logs/{id}|curl -XPUT http://localhost:50598/api/file-access-reject-logs/1|

### whitelist
|Implemented|Method|URL|Example|
|--|--|--|--|
|Yes|GET|api/whitelist?page={page}|curl -XGET http://localhost:50598/api/whitelist?page=1|
|Yes|GET|api/whitelist?version={version}|curl -XGET http://localhost:50598/api/whitelist?version=1.0.1|
|Yes|GET|api/whitelist/{id}|curl -XGET http://localhost:50598/api/whitelist/1|
|No|GET|api/whitelist?latest-version|curl -XGET http://localhost:50598/api/whitelist?latest-version|

### system-logs
|Implemented|Method|URL|Example|
|--|--|--|--|
|Yes|GET|api/system-logs?page={page}|curl -XGET http://localhost:50598/api/system-logs?page=1|
|Yes|GET|api/system-logs/{id}|curl -XGET http://localhost:50598/api/system-logs/1|
