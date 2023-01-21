
## 💻 Dev Freela

Repository developed during the ASP .NET Core Training course maintained by the company [Luis Dev](https://www.linkedin.com/in/luisdeol/). In this project, concepts of development of Web APIs using .NET 6, Clean Architecture, CQRS, Entity Framework Core, Dapper, Repository Pattern, Unit Tests, Authentication and Authorization with JWT, Messaging and Microservices were applied.

## 📫  Routes

### Projects Controller

<img src="https://img.shields.io/badge/-GET-%2361AFFE" height="30" />

**"/api/projects"**

_Get all Projects_

**required headers:**
`Authorization: Bearer {token JWT}`

**roles:**
`"client", "freelancer"`

**query params:**
`query?: string`

**response:**
```
[
  {
     "id": int,
     "title": string,
     "createdAt": DateTime
  }
]
```

<hr>

<img src="https://img.shields.io/badge/-GET-%2361AFFE" height="30" />

**"/api/projects/{id}"**

_Get Project by your id_

**required headers:**
`Authorization: Bearer {token JWT}`

**roles:**
`"client", "freelancer"`

**route params:**
`id: int`

**response:**
```
{
  "id": int,
  "title": string,
  "description": string,
  "totalCost": decimal,
  "startedAt"?: DateTime,
  "finishedAt"?: DateTime,
  "clientFullName": string,
  "freelancerFullName": string
}
```

<hr>

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/api/projects"**

_Create a new Project_

**required headers:**
`Authorization: Bearer {token JWT}`

**roles:**
`"client"`

**body:**
```
{
   "title": string,
   "description": string,
   "idClient": int,
   "idFreelancer": int,
   "totalCost": decimal
}
```

**response:**
```
{
   "title": string,
   "description": string,
   "idClient": int,
   "idFreelancer": int,
   "totalCost": decimal
}
```

<hr>

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/api/projects/{id}/comments"**

_Adds a comment to an existing project using your id_

**required headers:**
`Authorization: Bearer {token JWT}`

**roles:**
`"client", "freelancer"`

**route params:**
`id: int`

**body:**
```
{
   "content": string,
   "idProject": int,
   "idUser": int	
}
```

**response:**
_No content_

<hr>

<img src="https://img.shields.io/badge/-PUT-%23FCA130" height="30" />

**"/api/projects/{id}"**

_Update a Project by your id_

**required headers:**
`Authorization: Bearer {token JWT}`

**roles:**
`"client"`

**route params:**
`id: int`

**body:**
```
{
  "id": int,
  "title": string,
  "description": string,
  "totalCost": decimal
}
```

**response:**
_No content_

<hr>

<img src="https://img.shields.io/badge/-PUT-%23FCA130" height="30" />

**"/api/projects/{id}/start"**

_Changes the status of a project to "InProgress" by your id_

**required headers:**
`Authorization: Bearer {token JWT}`

**roles:**
`"client"`

**route params:**
`id: int`

**response:**
_No content_

<hr>

<img src="https://img.shields.io/badge/-PUT-%23FCA130" height="30" />

**"/api/projects/{id}/finish"**

_Changes the status of a project to "PaymentPending" by your id and sends a message to the payment microservice using RabbitMQ_

**required headers:**
`Authorization: Bearer {token JWT}`

**roles:**
`"client"`

**route params:**
`id: int`

**body:**
```
{
  "idProject": int,
  "creditCardNumber": string,
  "cvv": string,
  "expiresAt": string,
  "fullName": string,
  "amount": decimal
}
```

**response:**
_Accepted_

<hr>

<img src="https://img.shields.io/badge/-DELETE-%23F93E3E" height="30" />

**"/api/projects/{id}"**

_Delete a project by your id_

**required headers:**
`Authorization: Bearer {token JWT}`

**roles:**
`"client"`

**route params:**
`id: int`

**response:**
_No content_

<hr>

### Skills Controller

<img src="https://img.shields.io/badge/-GET-%2361AFFE" height="30" />

**"/api/skills"**

_Get all Skills_

**response:**
```
[
  {
     "id": int,
     "description": string
  }
]
```

<hr>

### Users Controller

<img src="https://img.shields.io/badge/-GET-%2361AFFE" height="30" />

**"/api/users/{id}"**

_Get a User by your id_

**required headers:**
`Authorization: Bearer {token JWT}`

**route params:**
`id: int`

**response:**
```
{
  "fullName": string,
  "email": string
}
```

<hr>

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/api/users"**

_Register a new User_

**body:**
```
{
  "fullName": string,
  "password": string,
  "email": string,
  "birthDate": DateTime,
  "role": string
}
```

**response:**
```
{
  "fullName": string,
  "password": string,
  "email": string,
  "birthDate": DateTime,
  "role": string
}
```

<hr>

<img src="https://img.shields.io/badge/-PUT-%23FCA130" height="30" />

**"/api/users/login"**

_Generate a new JWT token_

**body:**
```
{
  "email": string,
  "password": string
}
```

**response:**
```
{
  "email": string,
  "token": string
}
```

## 🌐 Status
<p>Finished project ✅</p>

## 🧰 Prerequisites

- .NET 6.0 or +

- Connection string to SQL Server BD in dev-freela/DevFreela.API/appsettings.json named as ConnectionStrings.DevFreelaCs

- URL to [Payment Microservice](https://github.com/AllanDutra/dev-freela-payments) in dev-freela/DevFreela.API/appsettings.json named as Services.Payments

- RabbitMQ

## 🔧 Installation
`$ git clone https://github.com/AllanDutra/dev-freela.git`

`$ cd dev-freela/DevFreela.API`

`$ dotnet restore`

`$ cd ../DevFreela.Infrastructure`

`$ dotnet ef database update -s ../DevFreela.API/DevFreela.API.csproj`

`$ cd ../DevFreela.API`

`$ dotnet run`

**Server listenning at  [https://localhost:5000/](https://localhost:5000/)!**

## 🔨 Tools used

<div>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="80" /> 
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" width="80" />
<img src="https://cdn.freebiesupply.com/logos/large/2x/rabbitmq-logo-png-transparent.png" height="80" />

</div>
