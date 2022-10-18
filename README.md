# Microservices course


<img src="https://github.com/kuzmiich/MicroservicesFullCourse/tree/master/Readme/png/get.png">
<img src="./Readme/png/post.png">
<img src="Readme/png/put.png">
<img src="/Readme/png/delete.png">

![get](https://github.com/kuzmiich/MicroservicesFullCourse/Readme/png/get.png)
![delete](Readme/png/post.png)
![delete](./Readme/png/put.png)
![delete](/Readme/png/delete.png)

## Table of Contents

- [Installation Guide](#installation-guide)
- [Rest API Endpoints (Local)](#rest-api-endpoints-local)
- [Rest API Endpoints (K8S)](#rest-api-endpoints-k8s)

## Installation Guide
1. Local
    1.1. clone from github https://github.com/kuzmiich/MicroservicesFullCourse
    1.2.
    1.3.
    1.4.

2. Deploy to K8S

## Rest API Endpoints Local




## Rest API Endpoints K8S

### Platform Service. 1. Get all platform

#### Request
`<img src="/Readme/png/get.png"> All`
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

### Platform Service. 2. Get platform by id
#### Request
`<img src="/Readme/png/get.png"> platform with id=1`
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

### Platform Service. 3. Post Platform to DB
#### Request
`<img src="/Readme/png/post.png"> platform`
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
### Platform Service. 4. Put Platform in DB

#### Request
`<img src="/Readme/png/put.png"> platform` 
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

### Platform Service. 5. Delete Platform in DB

#### Request
`<img src="/Readme/png/delete.png"> platform by id=1`
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

### Comman Service. 1. Get all platform

#### Request

`<img src="/Readme/png/get.png"> all`
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

### Comman Service. 2. Get all command by platform id

#### Request

`<img src="/Readme/png/get.png"> commands by platformId=1`
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

### Comman Service. 3. Get command by commandId with dependent platformId

#### Request

`<img src="./Readme/png/get.png"> command with commandId=1 & platformId=1`
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

### Comman Service. 4. Post command with dependent platformId

#### Request
`<img src="./Readme/png/post.png"> command with platformId=1`
```
http://acme.com/api/c/platform/{platformId}/command
```
```json
{
    "howToDoActivity": "string",
    "commandLine": "string"
}
```