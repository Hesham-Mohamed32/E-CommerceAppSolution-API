# E-Commerce REST API

A RESTful API for an e-commerce platform built with **ASP.NET Core (.NET 8)**, featuring product catalog, shopping basket, order management, and JWT authentication.

## Tech Stack

| Technology | Purpose |
|------------|---------|
| **ASP.NET Core (.NET 8)** | Web API framework |
| **Entity Framework Core** | ORM & database access |
| **SQL Server** | Primary data store |
| **Redis** | Basket/session storage |
| **ASP.NET Core Identity** | User management |
| **JWT Bearer** | Authentication |
| **Swagger/OpenAPI** | API documentation |

## Project Structure

```
E-CommerceAppSolution-API/
├── E-CommerceApp/           # API host & configuration
├── Core/                    # Domain, services, business logic
├── Infrastructure/         # Persistence, controllers
└── Shared/                 # DTOs, enums, shared models
```

---

## Steps to Setup

### 1. Clone the application

```bash
git clone <repository-url>
cd E-CommerceAppSolution-API
```

### 2. Create SQL Server databases

The application uses two databases:

- **ECommerceApp** – Products, orders, delivery methods
- **ECommerceApp.Identity** – Users, roles, addresses

Create them manually or let EF migrations create them on first run. Ensure SQL Server is running.

### 3. Configure connection strings

Edit `E-CommerceApp/appsettings.json`:

```json
"ConnectionStrings": {
  "DeafultConnetion": "Server=.;Database=ECommerceApp;Trusted_Connection=True;TrustServerCertificate=True",
  "IdentityConnetion": "Server=.;Database=ECommerceApp.Identity;Trusted_Connection=True;TrustServerCertificate=True",
  "RedicConnection": "localhost"
}
```

Update `Server=.;` to your SQL Server instance (e.g. `Server=localhost;` or `Server=(localdb)\\mssqllocaldb;`).  
Ensure **Redis** is installed and running for basket storage.

### 4. Run the application

```bash
cd E-CommerceApp
dotnet run
```

The API runs at:

- **HTTPS:** https://localhost:7082  
- **HTTP:** http://localhost:5002  
- **Swagger UI:** https://localhost:7082/swagger  

---

## REST API Endpoints

Base URL: `https://localhost:7082/api` or `http://localhost:5002/api`

### Authentication

| Method | URL | Auth | Description |
|--------|-----|------|-------------|
| POST | `/Authentication/Register` | No | Register a new user |
| POST | `/Authentication/Login` | No | Log in and receive JWT token |
| GET | `/Authentication/EmailExist?email={email}` | No | Check if email is available |
| GET | `/Authentication` | Yes | Get current logged-in user |
| GET | `/Authentication/Address` | Yes | Get user's saved address |
| PUT | `/Authentication/Address` | Yes | Update user's address |

### Products

| Method | URL | Auth | Description |
|--------|-----|------|-------------|
| GET | `/Product` | No | Get paginated products (with filters) |
| GET | `/Product/{id}` | No | Get product by ID |
| GET | `/Product/Types` | No | Get all product types |
| GET | `/Product/Brands` | No | Get all product brands |

### Basket

| Method | URL | Auth | Description |
|--------|-----|------|-------------|
| GET | `/Basket?id={id}` | Yes | Get basket by ID |
| POST | `/Basket` | No* | Create or update basket |
| DELETE | `/Basket/{id}` | Yes | Delete basket |

\* POST allows anonymous for guest checkout; GET/DELETE require authentication.

### Orders

| Method | URL | Auth | Description |
|--------|-----|------|-------------|
| POST | `/Orders` | Yes | Create order from basket |
| GET | `/Orders` | Yes | Get all orders for current user |
| GET | `/Orders/{id}` | Yes | Get order by ID |
| GET | `/Orders/DeliveryMethod` | No | Get available delivery methods |

---

## Request & Response Examples

### Authentication

#### Register – `POST /api/Authentication/Register`

**Request:**
```json
{
  "email": "user@example.com",
  "password": "P@ssw0rd123",
  "phoneNumber": "1234567890",
  "dispalyName": "John Doe",
  "userName": "johndoe"
}
```

**Response:**
```json
{
  "displayName": "John Doe",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com"
}
```

#### Login – `POST /api/Authentication/Login`

**Request:**
```json
{
  "email": "user@example.com",
  "password": "P@ssw0rd123"
}
```

**Response:**
```json
{
  "displayName": "John Doe",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com"
}
```

#### Get User Address – `GET /api/Authentication/Address` (requires `Authorization: Bearer {token}`)

**Response:**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "country": "USA",
  "city": "New York",
  "street": "123 Main St"
}
```

---

### Products

#### Get Products (Paginated) – `GET /api/Product`

**Query Parameters:**

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `TypeId` | int? | - | Filter by product type |
| `BrandId` | int? | - | Filter by brand |
| `sort` | enum | - | 1=NameAsc, 2=NameDesc, 3=PriceAsc, 4=PriceDesc |
| `Search` | string? | - | Search in product name |
| `PageIndex` | int | 1 | Page number |
| `PageSize` | int | 5 | Items per page (max 10) |

**Example:** `GET /api/Product?BrandId=1&sort=3&PageIndex=1&PageSize=5`

**Response:**
```json
{
  "pageIndex": 1,
  "pageSize": 5,
  "tolatCount": 42,
  "data": [
    {
      "id": 1,
      "name": "Product Name",
      "description": "Product description",
      "pictureUrl": "https://example.com/image.jpg",
      "price": 29.99,
      "brandName": "Brand Name",
      "typeName": "Type Name"
    }
  ]
}
```

#### Get Product by ID – `GET /api/Product/{id}`

**Response:**
```json
{
  "id": 1,
  "name": "Product Name",
  "description": "Product description",
  "pictureUrl": "https://example.com/image.jpg",
  "price": 29.99,
  "brandName": "Brand Name",
  "typeName": "Type Name"
}
```

#### Get Types – `GET /api/Product/Types`

**Response:**
```json
[
  { "id": 1, "name": "Electronics" },
  { "id": 2, "name": "Clothing" }
]
```

#### Get Brands – `GET /api/Product/Brands`

**Response:**
```json
[
  { "id": 1, "name": "Brand A" },
  { "id": 2, "name": "Brand B" }
]
```

---

### Basket

#### Create/Update Basket – `POST /api/Basket`

**Request:**
```json
{
  "id": "basket-guid-string",
  "basketItems": [
    {
      "id": 0,
      "productName": "Product Name",
      "price": 29.99,
      "pictureUrl": "https://example.com/image.jpg",
      "quantity": 2
    }
  ]
}
```

**Response:**
```json
{
  "id": "basket-guid-string",
  "basketItems": [
    {
      "id": 1,
      "productName": "Product Name",
      "price": 29.99,
      "pictureUrl": "https://example.com/image.jpg",
      "quantity": 2
    }
  ]
}
```

#### Get Basket – `GET /api/Basket?id={id}` (requires auth)

**Response:** Same structure as Create/Update response above.

---

### Orders

#### Create Order – `POST /api/Orders` (requires auth)

**Request:**
```json
{
  "basketId": "basket-guid-string",
  "shippingAddress": {
    "firstName": "John",
    "lastName": "Doe",
    "country": "USA",
    "city": "New York",
    "street": "123 Main St"
  },
  "deliveryMethodId": 1
}
```

**Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userEmail": "user@example.com",
  "shippingAddress": {
    "firstName": "John",
    "lastName": "Doe",
    "country": "USA",
    "city": "New York",
    "street": "123 Main St"
  },
  "orderItems": [
    {
      "productId": 1,
      "productName": "Product Name",
      "pictureUrl": "https://example.com/image.jpg",
      "price": 29.99,
      "quantity": 2
    }
  ],
  "paymentStatus": "Pending",
  "deliveryMethod": "UPS Ground",
  "deliveryMethodId": 1,
  "subTotal": 59.98,
  "total": 69.98,
  "orderDate": "2025-03-02T12:00:00Z",
  "paymentIntentId": ""
}
```

#### Get Delivery Methods – `GET /api/Orders/DeliveryMethod`

**Response:**
```json
[
  {
    "id": 1,
    "shortName": "UPS",
    "description": "Fast delivery",
    "price": 10.00,
    "deliveryTime": "1-2 Days"
  }
]
```

---

## Authentication

Protected endpoints require a JWT in the header:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Obtain a token via `POST /api/Authentication/Login` or `POST /api/Authentication/Register`.

---

## Error Responses

| Status | Use |
|--------|-----|
| 400 | Validation errors |
| 404 | Resource not found |
| 500 | Internal server error |

**Validation Error (400):**
```json
{
  "statesCode": 400,
  "errorMessage": "Validation failed",
  "errors": [
    { "field": "Email", "errors": ["Invalid format"] }
  ]
}
```

**Not Found (404):**
```json
{
  "statusCode": 404,
  "errorMessage": "Product not found",
  "errors": []
}
```

---

## Testing the API

Use **Postman**, **curl**, or the built-in **Swagger UI** at `/swagger` to call the endpoints.

Example with curl after login:

```bash
# Login
curl -X POST https://localhost:7082/api/Authentication/Login \
  -H "Content-Type: application/json" \
  -d '{"email":"user@example.com","password":"P@ssw0rd123"}'

# Get products (with token)
curl -X GET "https://localhost:7082/api/Product?PageIndex=1&PageSize=5" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

---

## Seed Data

On startup, the app seeds:

- Product types, brands, and sample products
- Delivery methods
- Identity roles (Admin, SuperAdmin) and demo users

Adjust seed data in `Infrastructure/Persistence/Data/DataSeed/` as needed.
