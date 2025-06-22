
# Hockeyshop – Business Web Application

A modern, modular ASP.NET Core MVC solution for managing a hockey equipment store.

---

## Projects in the Solution

- **Hockeyshop.PortalWWW** – Public-facing e-commerce website
- **Hockeyshop.Intranet** – Internal admin panel
- **Hockeyshop.Data** – Entity models and DbContext
- **Hockeyshop.Interfaces** – Service interfaces
- **Hockeyshop.Services** – Business logic/service implementations

---

## Features

- Product catalog, shopping cart, and order management
- Contact form (messages stored in DB, managed via Intranet)
- User authentication and roles
- Real-time notifications (SignalR) for new contact messages
- Admin CRUD for products, orders, users, marketing, etc.

---

## Technology Stack

- **Backend:** ASP.NET Core MVC (.NET 8), Entity Framework Core
- **Frontend:** Razor Views, Bootstrap 5, jQuery, AJAX
- **Database:** SQL Server
- **Notifications:** SignalR (real-time)
- **Architecture:** Repository & Service pattern, Dependency Injection, Clean separation


---

## Getting Started

1. **Clone the repo:**
   ```
   git clone https://github.com/kriskensy/WSB_PIAB_Hockeyshop
   ```
2. **Configure connection strings** in `appsettings.json` for both web projects.
3. **Apply migrations:**
   ```
   dotnet ef database update --project Hockeyshop.Data
   ```
4. **Run both PortalWWW and Intranet** (different ports).
5. **Trust the dev HTTPS certificate (if needed):**
   ```
   dotnet dev-certs https --trust
   ```

---

## Development Notes

- Update `intranetApiUrl` in PortalWWW to match your Intranet API endpoint.
- For SignalR notifications, ensure Intranet is running and hosts the hub.
- Use `appsettings.json` to configure ports.

---

## ⚠️ Missing Features

This project is under development.  
**Some typical e-commerce features are currently missing and may be added in the future.**

---

## License

MIT License

---
