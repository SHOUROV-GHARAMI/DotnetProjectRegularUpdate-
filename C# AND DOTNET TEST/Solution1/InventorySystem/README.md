# 📦 InventorySystem

A modern, flexible inventory management system built with .NET 9 MVC, featuring customizable collections, role-based access control, and real-time collaboration capabilities.

## 🌟 Overview

InventorySystem is designed to help organizations manage their inventory collections with maximum flexibility. Unlike traditional inventory systems with rigid schemas, this system allows you to define custom fields for each item, making it adaptable to various use cases—from office supplies to equipment tracking, product catalogs, and more.

### Key Highlights
- **Flexible Data Model**: Store items with custom JSON fields—no schema migration needed
- **Custom ID Generation**: Configure per-inventory ID patterns with date, sequence, and random tokens
- **Role-Based Access**: Admin, Creator, and User roles with granular inventory-level permissions
- **Real-Time Collaboration**: SignalR-powered discussion hub for item comments
- **Multi-Language Support**: Built-in localization framework (English & Bengali)
- **Optimistic Concurrency**: Prevent data conflicts with automatic version control

---

## 🚀 Quick Start

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (LocalDB, Express, or full version)
- A code editor (Visual Studio, VS Code, or JetBrains Rider)

### Installation Steps

1. **Clone or navigate to the project directory**
   ```cmd
   cd "c:\Users\Shourov GM\Documents\DOTNET_LERNING_FOLDER\C# AND DOTNET TEST\Solution1\InventorySystem"
   ```

2. **Restore dependencies**
   ```cmd
   dotnet restore
   ```

3. **Update database connection string** (Optional)
   
   Edit `appsettings.json` and update the connection string if needed:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InventorySystemDb;Trusted_Connection=True;"
     }
   }
   ```

4. **Apply database migrations**
   ```cmd
   dotnet ef database update
   ```

5. **Run the application**
   ```cmd
   dotnet run
   ```

6. **Open in browser**
   - HTTPS: `https://localhost:5001`
   - HTTP: `http://localhost:5000`

### Default Credentials
- **Email**: `admin@local`
- **Password**: `Admin123!`

> 💡 **Tip**: You can override these using environment variables `SEED_ADMIN_EMAIL` and `SEED_ADMIN_PASSWORD`

---

## 🐳 Docker Deployment

### Build the Docker image
```bash
docker build -t inventorysystem:latest .
```

### Run the container
```bash
docker run -d -p 8080:80 --name inventory-app inventorysystem:latest
```

### Access the application
Open `http://localhost:8080` in your browser

### Stop and remove the container
```bash
docker stop inventory-app
docker rm inventory-app
```

---

## 🏗️ Architecture

### Technology Stack
- **Framework**: ASP.NET Core 9.0 MVC
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity
- **Real-Time**: SignalR
- **Frontend**: Bootstrap 5, Razor Views
- **Localization**: ASP.NET Core Localization

### Project Structure
```
InventorySystem/
├── Controllers/         # MVC Controllers
│   ├── AccountController.cs      # Authentication
│   ├── AdminController.cs        # Admin panel
│   ├── HomeController.cs         # Landing page
│   ├── InventoryController.cs    # Inventory CRUD
│   └── ItemController.cs         # Item management
├── Models/              # Data models
│   ├── ApplicationUser.cs        # User entity
│   ├── Inventory.cs              # Collection entity
│   ├── Item.cs                   # Item entity
│   ├── Comment.cs                # Discussion comments
│   ├── ItemLike.cs               # Social features
│   └── InventoryAccess.cs        # Access control
├── Services/            # Business logic
│   └── CustomIdService.cs        # ID generation
├── Data/                # Database context
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs          # Seed data
├── Hubs/                # SignalR hubs
│   └── DiscussionHub.cs          # Real-time chat
├── Views/               # Razor views
├── wwwroot/             # Static files
└── Migrations/          # EF migrations
```

### Database Schema

**Core Entities:**
- `AspNetUsers` - User accounts (Identity)
- `Inventories` - Collections/categories
- `Items` - Inventory items with custom fields
- `InventoryAccess` - User permissions per inventory
- `Comments` - Item discussions
- `ItemLikes` - Social interactions

**Key Relationships:**
- One User → Many Inventories (owner)
- One Inventory → Many Items
- One Inventory → Many InventoryAccess
- One Item → Many Comments
- One Item → Many ItemLikes

---

## 🎯 Features

### ✅ Implemented Features

#### Authentication & Authorization
- [x] User registration with email
- [x] User login/logout
- [x] Role-based authorization (Admin, Creator, User)
- [x] Secure password hashing
- [x] Database-seeded roles and admin account

#### Inventory Management
- [x] Create inventories (collections)
- [x] List all inventories (public)
- [x] Configure custom ID patterns per inventory
- [x] Preview ID generation before saving
- [x] Owner association and tracking

#### Item Management
- [x] Add items to inventories
- [x] Auto-generate custom IDs (per pattern)
- [x] Store flexible custom fields as JSON
- [x] Edit item details
- [x] View item details page
- [x] Optimistic concurrency control

#### Custom ID System
- [x] Date tokens: `{YYYY}`, `{MM}`, `{DD}`
- [x] Sequence token: `{SEQ}`
- [x] Random token: `{RANDOM:N}`
- [x] Per-inventory sequence tracking
- [x] Collision detection and retry logic
- [x] Pattern preview functionality

#### Infrastructure
- [x] Entity Framework Core with migrations
- [x] SQL Server database support
- [x] Docker containerization
- [x] Localization framework (en-US, bn-BD)
- [x] SignalR hub configuration

### 🚧 In Progress

#### Real-Time Features
- ⚠️ SignalR hub (backend ready)
- ⚠️ Frontend client integration (pending)
- ⚠️ Comment persistence (pending)

#### Social Features
- ⚠️ Like/unlike items (model ready, UI pending)
- ⚠️ Comments display (model ready, UI pending)

### 📋 Planned Features

<details>
<summary>Click to expand full roadmap</summary>

#### Phase 1: Core Enhancements
- [ ] Edit and delete inventories
- [ ] Edit and delete items
- [ ] User profile management
- [ ] Email confirmation
- [ ] Password reset functionality

#### Phase 2: Advanced Features
- [ ] Full-text search across items
- [ ] Advanced filtering and sorting
- [ ] Tag/category system
- [ ] File attachments for items
- [ ] Item history/audit trail

#### Phase 3: Collaboration
- [ ] Real-time comments (complete SignalR integration)
- [ ] Like/favorite items
- [ ] User activity feeds
- [ ] Notifications system
- [ ] @mentions in comments

#### Phase 4: Admin & Management
- [ ] Admin dashboard with statistics
- [ ] User management (list, edit roles, block)
- [ ] Content moderation tools
- [ ] System configuration UI
- [ ] Audit logs viewer

#### Phase 5: UI/UX
- [ ] Dark/light theme toggle
- [ ] Theme persistence
- [ ] Toast notifications
- [ ] Loading indicators
- [ ] Drag-and-drop file uploads
- [ ] Data tables with pagination

#### Phase 6: Integration & API
- [ ] RESTful API
- [ ] JWT authentication
- [ ] Swagger documentation
- [ ] CSV/Excel import
- [ ] CSV/Excel export
- [ ] Webhooks
- [ ] Third-party integrations (Jira, Salesforce)

#### Phase 7: Performance & Security
- [ ] Caching layer (Redis)
- [ ] Background jobs (Hangfire)
- [ ] Rate limiting
- [ ] Enhanced security headers
- [ ] Input sanitization
- [ ] SQL injection prevention audit

#### Phase 8: Testing & Documentation
- [ ] Unit tests
- [ ] Integration tests
- [ ] API documentation
- [ ] User guide
- [ ] Deployment guide

</details>

---

## 🎨 Custom ID Patterns

The Custom ID Service allows you to define flexible ID patterns for each inventory.

### Supported Tokens

| Token | Description | Example Output |
|-------|-------------|----------------|
| `{YYYY}` | Four-digit year | `2025` |
| `{MM}` | Two-digit month | `11` |
| `{DD}` | Two-digit day | `05` |
| `{SEQ}` | Auto-incrementing sequence | `1`, `2`, `3`... |
| `{RANDOM:N}` | N-digit random number | `{RANDOM:5}` → `47382` |

### Pattern Examples

```
INV-{YYYY}-{SEQ}           → INV-2025-1, INV-2025-2
PROD-{YYYY}{MM}-{SEQ}      → PROD-202511-1
ITEM-{RANDOM:6}            → ITEM-847293
OFF-{YYYY}-{MM}-{DD}-{SEQ} → OFF-2025-11-05-1
```

### How to Configure

1. Navigate to **Inventories** → Select an inventory
2. Click **Settings**
3. Enter your desired pattern in the "Custom ID Pattern" field
4. Click **Preview** to see a sample ID
5. Click **Save** to apply

---

## 🔧 Configuration

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `SEED_ADMIN_EMAIL` | Admin user email | `admin@local` |
| `SEED_ADMIN_PASSWORD` | Admin user password | `Admin123!` |
| `ASPNETCORE_ENVIRONMENT` | Environment name | `Development` |

### appsettings.json

Key configuration sections:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InventorySystemDb;Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

---

## 🐛 Troubleshooting

### Database Connection Issues

**Problem**: Cannot connect to SQL Server

**Solutions**:
1. Verify SQL Server is running: `sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT @@VERSION"`
2. Check connection string in `appsettings.json`
3. Try using SQL Server Express or full SQL Server
4. For LocalDB, ensure it's installed with Visual Studio

### Migration Errors

**Problem**: `dotnet ef database update` fails

**Solutions**:
1. Install EF Core tools: `dotnet tool install --global dotnet-ef`
2. Rebuild the project: `dotnet build`
3. Delete the database and try again
4. Check for pending migrations: `dotnet ef migrations list`

### Port Already in Use

**Problem**: Cannot start the application (port 5000/5001 in use)

**Solutions**:
1. Change ports in `Properties/launchSettings.json`
2. Stop other applications using those ports
3. Use `netstat -ano | findstr :5000` to find the process

### Docker Build Fails

**Problem**: Docker build errors

**Solutions**:
1. Ensure Docker Desktop is running
2. Check Dockerfile syntax
3. Build with verbose output: `docker build --progress=plain -t inventorysystem:latest .`
4. Clear Docker cache: `docker system prune -a`

### Admin Login Not Working

**Problem**: Cannot login with default credentials

**Solutions**:
1. Check if database was seeded: look for logs during startup
2. Try re-running database update: `dotnet ef database update`
3. Check `DbInitializer.cs` for any errors
4. Manually create admin user through registration and assign Admin role

---

## 🧪 Development

### Adding a New Migration

```cmd
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### Running in Development Mode

```cmd
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

### Watching for Changes

```cmd
dotnet watch run
```

This enables hot reload for faster development.

### Database Commands

```cmd
# List all migrations
dotnet ef migrations list

# Remove last migration (if not applied)
dotnet ef migrations remove

# Drop database
dotnet ef database drop

# Generate SQL script
dotnet ef migrations script -o migration.sql
```

---

## 📚 API Endpoints (MVC Routes)

### Account Routes
- `GET /Account/Register` - Registration page
- `POST /Account/Register` - Register new user
- `GET /Account/Login` - Login page
- `POST /Account/Login` - Authenticate user
- `POST /Account/Logout` - Logout user

### Inventory Routes
- `GET /Inventory` - List all inventories
- `GET /Inventory/Create` - Create inventory form
- `POST /Inventory/Create` - Save new inventory
- `GET /Inventory/Settings/{id}` - Inventory settings
- `POST /Inventory/Settings/{id}` - Update settings
- `GET /Inventory/PreviewCustomId` - Preview ID pattern

### Item Routes
- `GET /Item/Index?inventoryId={id}` - List items
- `GET /Item/Create?inventoryId={id}` - Create item form
- `POST /Item/Create` - Save new item
- `GET /Item/Details/{id}` - Item details
- `POST /Item/Edit` - Update item

### Admin Routes
- `GET /Admin` - Admin dashboard (requires Admin role)

---

## 🤝 Contributing

We welcome contributions! Here's how you can help:

### Getting Started
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Make your changes
4. Test thoroughly
5. Commit with clear messages: `git commit -m "Add: Your feature description"`
6. Push to your fork: `git push origin feature/your-feature-name`
7. Open a Pull Request

### Coding Standards
- Follow C# naming conventions
- Use async/await for database operations
- Add XML comments to public methods
- Keep controllers thin, use services for business logic
- Write unit tests for new features

### Commit Message Format
```
Type: Brief description

Detailed explanation if needed

Types: Add, Update, Fix, Remove, Refactor, Test, Docs
```

---

## 📄 License

This project is currently unlicensed. Please contact the project owner for usage permissions.

---

## 👥 Support

### Need Help?
- 📧 Email: [Your email]
- 🐛 Issues: [GitHub Issues URL]
- 💬 Discussions: [GitHub Discussions URL]

### Useful Resources
- [.NET 9 Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- [SignalR Documentation](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction)

---

## 🎯 Project Status

**Current Version**: 0.1.0 (Alpha)  
**Last Updated**: November 5, 2025  
**Status**: 🟡 Active Development

### Recent Updates
- ✅ Initial project setup
- ✅ Authentication system implemented
- ✅ Custom ID generation service
- ✅ Basic CRUD operations
- 🚧 SignalR integration in progress

---

**Built with ❤️ using .NET 9**
