# FUNewsManagementSystem

## 1. Introduction
FUNewsManagementSystem (FUMS) is a news management system for universities and educational institutions. The system allows you to:

- Manage user accounts (Admin/Staff/Lecturer)  
- Manage news articles, categories, and tags  
- Perform CRUD operations and advanced search  
- Track edit history (Change Audit)  

The Frontend (FE) uses **ASP.NET Core Razor Pages** or MVC, while the Backend (BE) is **ASP.NET Core Web API** with OData and EF Core.  

---

## 2. Configuration & Running

### Backend
#### 1. Open the solution `_BE/DoHuuTuan_SE1842-Net_A01_BE.sln` in Visual Studio 2019 or later

#### 2. Configure the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TUAN\\TUAN;Database=FUNewsManagement;User Id=sa;Password=123;TrustServerCertificate=True"
}
```

#### 3. Scaffold the DbContext (if not yet created)
```bash
dotnet ef dbcontext scaffold "server=TUAN\\TUAN;database=FUNewsManagement;uid=sa;pwd=123;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models
```

### Frontend
#### 1. Open the solution `_FE/DoHuuTuan_SE1842-Net_A01_FE.sln`

#### 2. Open a web browser to access the application

#### 3. API Endpoints Overview (Key Endpoints)

| Method                  | Endpoint           | Description                     | Role          |
|-------------------------|------------------|--------------------------------|---------------|
| POST                    | /api/Auth/Login   | Authenticate user               | All           |
| GET                     | /api/News         | List all news articles          | Staff/Admin           |
| GET                     | /api/NewsPublic         | List all news articles          | All           |
| POST                    | /api/News         | Create news article             | Staff/Admin   |
| PUT                     | /api/News/{id}    | Update news article             | Staff/Admin   |
| DELETE                  | /api/News/{id}    | Delete news article             | Staff/Admin   |
| GET                     | /api/NewsAudit    | View last editor info           | Admin         |
| GET/POST/PUT/DELETE     | /api/Category     | CRUD categories                 | Staff/Admin   |
| GET/POST/PUT/DELETE     | /api/Account      | CRUD accounts                   | Admin         |
| GET/POST/PUT/DELETE     | /api/Tag          | CRUD tags                        | Staff/Admin   |
| GET                     | /api/Report       | Reporting & statistics          | Admin         |

> Other endpoints (e.g., advanced OData filtering/search) can be found in the Swagger documentation.


#### 4. Roles & Login Credentials

| Role      | Email                           | Password    |
|-----------|---------------------------------|------------|
| Admin     | admin@FUNewsManagementSystem.org | @@abc123@@ |
| Lecturer     | EmmaWilliam@FUNewsManagement.org | @1  |
| Staff  | IsabellaDavid@FUNewsManagement.org | @1 |

**Role Permissions:**

- **Admin**: full control, CRUD accounts, generate reports  
- **Staff**: manage categories, articles, own profile  
- **Lecturer**: read/search articles only

#### 5. Features

##### 5.1 News Management
- CRUD articles with tags  
- `CreatedByID`, `CreatedDate` auto-set  
- `UpdatedByID`, `ModifiedDate` auto-set on update  
- “Duplicate Article” option  

##### 5.2 Category & Tag Management
- CRUD with foreign key checks  
- Search & filter  
- Count articles per category  

##### 5.3 Account Management (Admin)
- CRUD accounts, password update with verification  
- Cannot delete accounts with created articles  

##### 5.4 Reporting (Admin)
- Filter by CreatedDate, group by Category/Author  
- Active vs Inactive totals  
- Sort by CreatedDate descending  

##### 5.5 Advanced Search & Related News
- Global keyword search across title, headline, content  
- Filter by Category, Tag, Author, CreatedDate  
- Related articles based on shared Tags  

##### 5.6 Change Audit
- Track last editor and modification date  
- Admin can view via `/api/NewsAudit`  

##### 5.7 Client Application
- Bootstrap 5 modals for CRUD  
- AJAX / Fetch API calls to backend  
- Pagination, status color indicators  
- Confirmation on delete  

#### 6. Notes
- Input validation on both client & server  
- Prevent duplicate records (emails, tag names, category names)  
- Confirmation dialogs for delete actions  
- All date/time values in local timezone  

