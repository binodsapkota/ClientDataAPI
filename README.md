# ClientDataAPI

**ClientDataAPI** is a secure, high-performance .NET Web API designed for multi-client environments. It integrates with **Azure AD B2C** for OAuth2-based authentication, manages client secrets efficiently, and provides optimized access to large-scale SQL datasets.

---

## Features

* Secure **OAuth2 authentication** using Azure AD B2C
* Multi-client support with individual secrets
* Automated client secret management (via API)
* Optimized **SQL queries** for datasets of 20M+ rows
* Fully configurable **reverse proxy** support
* Easy-to-extend **.NET Web API architecture**
* Ready for deployment in Azure or on-prem environments

---

## Architecture

```
Client Application --> [OAuth2 Token Request] --> Azure AD B2C --> [Access Token]
Access Token --> ClientDataAPI (.NET Web API) --> Azure SQL Database
```

---

## Prerequisites

* [.NET 7 SDK](https://dotnet.microsoft.com/download)
* Azure Subscription with **Azure AD B2C**
* SQL Server or Azure SQL Database
* Postman or cURL for testing API requests

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/ClientDataAPI.git
cd ClientDataAPI
```

### 2. Configure Azure AD B2C

1. Create a **B2C tenant** in Azure.
2. Register your **API application** (`ClientDataAPI`):

   * Expose an API scope: `access_as_user`
3. Register **client applications** for multi-client access.
4. Create **user flows/policies** (Sign-up/Sign-in) for token issuance.
5. Save **Client IDs**, **Tenant ID**, and **Client secrets** for configuration.

---

### 3. Update `appsettings.json`

```json
{
  "AzureAdB2C": {
    "Instance": "https://<YOUR_TENANT>.b2clogin.com/",
    "Domain": "<YOUR_TENANT>.onmicrosoft.com",
    "ClientId": "<API_CLIENT_ID>",
    "TenantId": "<TENANT_ID>",
    "SignUpSignInPolicyId": "B2C_1_SignInSignUp"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=<SQL_SERVER>;Database=ClientData;User Id=<USER>;Password=<PASSWORD>;"
  }
}
```

---

### 4. Run the API

```bash
dotnet restore
dotnet build
dotnet run
```

The API runs at `https://localhost:5001`.

---

### 5. Request OAuth2 Token (Client Credentials Flow)

```bash
curl -X POST -H "Content-Type: application/x-www-form-urlencoded" \
-d "client_id=<CLIENT_APP_ID>&scope=https://<YOUR_TENANT>.onmicrosoft.com/clientdataapi/access_as_user&client_secret=<CLIENT_SECRET>&grant_type=client_credentials" \
"https://<YOUR_TENANT>.b2clogin.com/<YOUR_TENANT>.onmicrosoft.com/B2C_1_SignInSignUp/oauth2/v2.0/token"
```

Use the token to call the API:

```bash
curl -H "Authorization: Bearer <ACCESS_TOKEN>" https://localhost:5001/api/clients
```

---

## SQL Optimization

* Optimized for **large datasets (20M+ rows)**
* Indexing strategies for performance
* Efficient query design for multi-client scenarios

---

## Security & Best Practices

* OAuth2 authentication via **Azure AD B2C**
* Secure **client secret management**
* Token-based authorization for all endpoints
* HTTPS enforced
* Reverse proxy support for scalability

---

## Folder Structure

```
ClientDataAPI/
├─ Controllers/        # API Controllers
├─ Services/           # Business logic and data services
├─ Models/             # Data models and DTOs
├─ Data/               # DbContext and migrations
├─ appsettings.json    # Configuration
└─ Program.cs          # Entry point
```

---

## Future Enhancements

* Automated **client secret rotation**
* Swagger/OpenAPI documentation with JWT authentication
* Integration with Azure Functions for background data processing
* Multi-tenant client onboarding

---

## License

This project is licensed under the **MIT License** – see the [LICENSE](LICENSE) file for details.
