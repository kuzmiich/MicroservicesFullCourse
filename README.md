# Microservices course
[Reminder] Add comments about course

### Used tools:
* MS SQL Server & MS SQL Studio 18
* Rider 2022.1
* Docker Desktop
* Postman

## Table of Contents

- [Installation Guide](#installation-guide)
- [Rest API Endpoints (Local)](#rest-api-endpoints-local)
    -  [Platform Service. - 1.Get all platform](#1-platform-service-get-all-platform)
    -  [Platform Service. - 2.Get platform by id](#2-platform-service-get-platform-by-id)
    -  [Platform Service. - 3.Post Platform to DB](#3-platform-service-post-platform-to-db)
    -  [Platform Service. - 4.Put Platform in DB](#4-platform-service-put-platform-in-db)
    -  [Platform Service. - 5.Delete Platform in DB](#5-platform-service-delete-platform-in-db)
    -  [Command Service. - 6.Get all platform](#6-command-service-get-all-platform)
    -  [Command Service. - 7.Get All command by platform id](#7-command-service-get-all-command-by-platform-id)
    -  [Command Service. - 8.Get Command by commandId with dependent platformId in DB](#8-command-service-get-command-by-commandid-with-dependent-platformid)
    -  [Command Service. - 9.Post Commmand with dependent platformId](#9-command-service-post-command-with-dependent-platformid)
- [Rest API Endpoints (K8S)](#rest-api-endpoints-k8s)
    -  [Platform Service. - 1.Get all platform](#10-platform-service-get-all-platform)
    -  [Platform Service. - 2.Get platform by id](#11-platform-service-get-platform-by-id)
    -  [Platform Service. - 3.Post Platform to DB](#12-platform-service-post-platform-to-db)
    -  [Platform Service. - 4.Put Platform in DB](#13-platform-service-put-platform-in-db)
    -  [Platform Service. - 5.Delete Platform in DB](#14-platform-service-delete-platform-in-db)
    -  [Command Service. - 6.Get all platform](#15-command-service-get-all-platform)
    -  [Command Service. - 7.Get All command by platform id](#16-command-service-get-all-command-by-platform-id)
    -  [Command Service. - 8.Get Command by commandId with dependent platformId in DB](#17-command-service-get-command-by-commandid-with-dependent-platformid)
    -  [Command Service. - 9.Post Commmand with dependent platformId](#18-command-service-post-command-with-dependent-platformid)
- [Link to the video](#link-to-the-video)
## Installation Guide
1. Local
- 1.1. Clone repository from github https://github.com/kuzmiich/MicroservicesFullCourse
- 1.2. Update database to both microservices <code>dotnet ef database update</code> by terminal
2. Deploy microservices to K8S
- 2.1.
- 2.2. 
- 2.3.
- 2.4.
- 2.5.

## Rest API Endpoints Local

### 1 Platform Service Get All Platform

#### Request
***Get all***
```
https://localhost:5001/api/platform/getAll
```
#### Response

```json
[
    {
        "id": 1,
        "name": "Dot net 2",
        "publisher": "Apple",
        "cost": "Free"
    },
    {
        "id": 2,
        "name": "Sql Server Express",
        "publisher": "Microsoft",
        "cost": "Free"
    },
    {
        "id": 3,
        "name": "Kubernetes",
        "publisher": "Cloud Native Computing Foundation",
        "cost": "Free"
    }
]
```

### 2 Platform Service Get Platform By Id
#### Request
***Get platform with id=1***
```
https://localhost:5001/api/platform/{id}
```
#### Response
```json
{
    "id": 1,
    "name": "Dot net 2",
    "publisher": "Apple",
    "cost": "Free"
}
```

### 3 Platform Service Post Platform to DB
#### Request
***Post platform***
```
https://localhost:5001/api/platform/
```

body

```json
{
    "Name": "Dot net",
    "Publisher": "Facebook",
    "Cost": "Free"
}    
```
#### Response
```json
{
    "id": 4,
    "name": "Dot net",
    "publisher": "Facebook",
    "cost": "Free"
}
```
### 4 Platform Service Put Platform in DB

#### Request
***Put platform***
```
https://localhost:5001/api/platform/
```
body
```json
{
    "id": 1,
    "Name": "Dot net 3",
    "Publisher": "Apple",
    "Cost": "Free"
}    
```
#### Response
```json
{
    "id": 1,
    "name": "Dot net 3",
    "publisher": "Apple",
    "cost": "Free"
}
```

### 5 Platform Service Delete Platform in DB

#### Request
***Delete platform by id=1***
```
https://localhost:5001/api/platform/{id}
```
#### Response

```json
{
    "id": 1,
    "name": "Dot net 3",
    "publisher": "Apple",
    "cost": "Free"
}
```

### 6 Command Service Get All Platform

#### Request

***Get all***
```
https://localhost:6001/api/c/platform/GetAll
```
#### Response

```json
[
    {
        "id": 1,
        "name": "Platform 1"
    },
    {
        "id": 2,
        "name": "Platform 2"
    },
    {
        "id": 3,
        "name": "Platform 3"
    }
]
```

### 7 Command Service Get All Command By Platform Id

#### Request

***Get commands by platformId=1***
```
https://localhost:6001/api/c/platform/{platformId}/command/GetAll
```
#### Response

```json
[
    {
        "id": 1,
        "platformId": 1,
        "howToDoActivity": "Build image",
        "commandLine": "docker build -t (image_id/image_name) -f (Dockerfile_Name) ."
    },
    {
        "id": 5,
        "platformId": 1,
        "howToDoActivity": "string",
        "commandLine": "string"
    },
    {
        "id": 6,
        "platformId": 1,
        "howToDoActivity": "string",
        "commandLine": "string"
    }
]
```

### 8 Command Service Get command by commandId with dependent platformId

#### Request

***Get command with commandId=1 & platformId=1***
```
https://localhost:6001/api/c/platform/{platformId}/command/{commandId}
```
#### Response

```json
{
    "id": 1,
    "platformId": 1,
    "howToDoActivity": "Build image",
    "commandLine": "docker build -t (image_id/image_name) -f (Dockerfile_Name) ."
}
```

### 9 Command Service Post command with dependent platformId

#### Request
***Post command with platformId=1***
```
https://localhost:6001/api/c/platform/{platformId}/command
```
```json
{
    "howToDoActivity": "string",
    "commandLine": "string"
}
```
#### Response
```json
{
    "id": 0,
    "platformId": 1,
    "howToDoActivity": "string",
    "commandLine": "string"
}
```

## Rest API Endpoints K8S

### 10 Platform Service Get All Platform

#### Request
***Get all***
```
http://acme.com/api/platform/getAll
```
#### Response

```json
[
    {
        "id": 1,
        "name": "Dot net 2",
        "publisher": "Apple",
        "cost": "Free"
    },
    {
        "id": 2,
        "name": "Sql Server Express",
        "publisher": "Microsoft",
        "cost": "Free"
    },
    {
        "id": 3,
        "name": "Kubernetes",
        "publisher": "Cloud Native Computing Foundation",
        "cost": "Free"
    }
]
```

### 11 Platform Service Get platform by id
#### Request
***Get platform with id=1***
```
http://acme.com/api/platform/{id}
```
#### Response
```json
{
    "id": 1,
    "name": "Dot net 2",
    "publisher": "Apple",
    "cost": "Free"
}
```

### 12 Platform Service Post Platform to DB
#### Request
***Post platform***
```
http://acme.com/api/platform/
```

body

```json
{
    "Name": "Dot net",
    "Publisher": "Facebook",
    "Cost": "Free"
}    
```
#### Response
```json
{
    "id": 4,
    "name": "Dot net",
    "publisher": "Facebook",
    "cost": "Free"
}
```
### 13 Platform Service Put Platform in DB

#### Request
***Put platform***
```
http://acme.com/api/platform/
```
body
```json
{
    "id": 1,
    "Name": "Dot net 3",
    "Publisher": "Apple",
    "Cost": "Free"
}    
```
#### Response
```json
{
    "id": 1,
    "name": "Dot net 3",
    "publisher": "Apple",
    "cost": "Free"
}
```

### 14 Platform Service Delete Platform in DB

#### Request
***Delete platform by id=1***
```
http://acme.com/api/platform/{id}
```
#### Response

```json
{
    "id": 1,
    "name": "Dot net 3",
    "publisher": "Apple",
    "cost": "Free"
}
```

### 15 Command Service Get all platform

#### Request

***Get all***
```
http://acme.com/api/c/platform/GetAll
```
#### Response

```json
[
    {
        "id": 1,
        "name": "Platform 1"
    },
    {
        "id": 2,
        "name": "Platform 2"
    },
    {
        "id": 3,
        "name": "Platform 3"
    }
]
```

### 16 Command Service Get all command by platform id

#### Request

***Get commands by platformId=1***
```
http://acme.com/api/c/platform/{platformId}/command/GetAll
```
#### Response

```json
[
    {
        "id": 1,
        "platformId": 1,
        "howToDoActivity": "Build image",
        "commandLine": "docker build -t (image_id/image_name) -f (Dockerfile_Name) ."
    },
    {
        "id": 5,
        "platformId": 1,
        "howToDoActivity": "string",
        "commandLine": "string"
    },
    {
        "id": 6,
        "platformId": 1,
        "howToDoActivity": "string",
        "commandLine": "string"
    }
]
```

### 17 Command Service Get command by commandId with dependent platformId

#### Request

***Get command with commandId=1 & platformId=1***
```
http://acme.com/api/c/platform/{platformId}/command/{commandId}
```
#### Response

```json
{
    "id": 1,
    "platformId": 1,
    "howToDoActivity": "Build image",
    "commandLine": "docker build -t (image_id/image_name) -f (Dockerfile_Name) ."
}
```

### 18 Command Service Post command with dependent platformId

#### Request
***Post command with platformId=1***
```
http://acme.com/api/c/platform/{platformId}/command
```
```json
{
    "howToDoActivity": "string",
    "commandLine": "string"
}
```
#### Response
```json
{
    "id": 0,
    "platformId": 1,
    "howToDoActivity": "string",
    "commandLine": "string"
}
```

## Link to the video
Thank a lot Les Jackson for course. Very huge count of information 

https://www.youtube.com/watch?v=DgVjEo3OGBI 