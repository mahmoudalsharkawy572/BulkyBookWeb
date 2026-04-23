# Bulky Book Web

A comprehensive full-stack e-commerce platform for selling books, built with ASP.NET Core and modern web technologies.

## Overview

Bulky Book Web is a multi-tenant e-commerce application that enables businesses to manage product catalogs, process orders, handle payments, and manage users. The platform supports both individual customers and corporate accounts with role-based access control.

## Features

### Customer Features
- **Product Browsing** - Browse and search for books with detailed product information
- **Shopping Cart** - Add/remove items, view cart summary, and manage quantities
- **Secure Checkout** - Integrated Stripe payment processing for secure transactions
- **Order Management** - View order history and track order status
- **Account Management** - User profile, password management, and personal data management
- **Two-Factor Authentication** - Enhanced security with 2FA support
- **Social Authentication** - Login via Facebook for convenience

### Admin Features
- **Product Management** - Create, edit, and delete products with image uploads
- **Category Management** - Organize products into categories
- **Company/Tenant Management** - Manage multiple companies or business units
- **Order Management** - View, process, and fulfill customer orders
- **Payment Management** - Track payments and payment status
- **User Management** - Manage users and assign roles/permissions
- **Role-Based Access Control** - Fine-grained permission management

### General Features
- **Email Notifications** - Order confirmations and status updates via email
- **Session Management** - Secure session handling with 100-minute timeout
- **HTTPS Security** - All connections secured with SSL/TLS
- **HSTS Protection** - Protection against man-in-the-middle attacks
- **Data Seeding** - Automatic database initialization with sample data

## Technology Stack

### Backend
- **.NET 8** - Latest LTS framework for high performance
- **ASP.NET Core** - Web framework with MVC and Razor Pages
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Relational database

### Frontend
- **Razor Pages & Views** - Server-side rendering with C#
- **Bootstrap** - Responsive UI framework
- **jQuery & jQuery Validation** - Client-side interactivity and validation

### Services & APIs
- **Stripe** - Payment processing and checkout sessions
- **Facebook Authentication** - Social login integration
- **ASP.NET Identity** - User authentication and authorization

### Architecture
- **Repository Pattern** - Data access abstraction layer
- **Unit of Work Pattern** - Coordinated data persistence
- **Dependency Injection** - Loose coupling and testability
- **Layered Architecture** - Separation of concerns

## Project Structure

```
BulkyWeb/
??? BulkyWeb/                 # Web application (ASP.NET Core)
?   ??? Areas/
?   ?   ??? Admin/           # Admin section (controllers, views)
?   ?   ??? Customer/        # Customer section (controllers, views)
?   ?   ??? Identity/        # Authentication (Razor Pages)
?   ??? ViewComponents/      # Reusable UI components
?   ??? wwwroot/             # Static files (CSS, JS, images)
?   ??? Program.cs           # Application startup
?
??? Bulky.DataAccess/        # Data access layer
?   ??? Data/                # DbContext and migrations
?   ??? Repository/          # Repository implementations
?   ??? DBInitializer/       # Database seeding
?
??? Bulky.Models/            # Domain models and entities
?
??? Bulky.Utility/           # Shared utilities and helpers
    ??? StripeSettings.cs    # Configuration models
```

## Prerequisites

- **.NET 8 SDK** or later
- **SQL Server** (local or remote instance)
- **Visual Studio 2022** (recommended) or VS Code
- **Stripe Account** - For payment processing (create at [stripe.com](https://stripe.com))
- **Facebook Developer Account** (optional) - For social authentication

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/mahmoudalsharkawy572/BulkyBookWeb.git
cd BulkyBookWeb
```

### 2. Configure the Database

Update the connection string in `BulkyWeb/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=BulkyDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

For local SQL Server with Windows Authentication:
```
Server=.;Database=BulkyDb;Trusted_Connection=True;TrustServerCertificate=True;
```

### 3. Configure Stripe Keys

Add your Stripe test/live keys to `BulkyWeb/appsettings.json`:

```json
{
  "Stripe": {
    "SecretKey": "sk_test_YOUR_SECRET_KEY",
    "PublishableKey": "pk_test_YOUR_PUBLISHABLE_KEY"
  }
}
```

Get your keys from the [Stripe Dashboard](https://dashboard.stripe.com/apikeys)

### 4. Configure Facebook Authentication (Optional)

In `Program.cs`, update Facebook credentials:

```csharp
builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "YOUR_FACEBOOK_APP_ID";
    options.AppSecret = "YOUR_FACEBOOK_APP_SECRET";
});
```

### 5. Apply Database Migrations

```bash
cd BulkyWeb
dotnet ef database update
```

Or via Package Manager Console in Visual Studio:
```powershell
Update-Database
```

### 6. Run the Application

```bash
dotnet run
```

The application will start at `https://localhost:7001` (or the configured port).

### 7. Access the Application

- **Customer Area**: `https://localhost:7001/customer`
- **Admin Area**: `https://localhost:7001/admin`
- **User Registration**: `https://localhost:7001/identity/account/register`

### Default Test Credentials

After database seeding, test accounts are available. Check the `DBInitializer` class for default credentials.

## Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;..."
  },
  "Stripe": {
    "SecretKey": "sk_test_...",
    "PublishableKey": "pk_test_..."
  }
}
```

### appsettings.Development.json

Create environment-specific settings for development:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

## API Endpoints

### Authentication
- `GET /identity/account/login` - User login
- `GET /identity/account/register` - User registration
- `POST /identity/account/logout` - User logout

### Customer
- `GET /customer/home/index` - Product listing
- `GET /customer/home/details/{id}` - Product details
- `GET /customer/cart/index` - Shopping cart
- `POST /customer/cart/add` - Add to cart
- `POST /customer/cart/remove` - Remove from cart
- `GET /customer/cart/confirmation/{id}` - Order confirmation

### Admin
- `GET /admin/product/index` - Product management
- `GET /admin/category/index` - Category management
- `GET /admin/order/index` - Order management
- `GET /admin/user/index` - User management
- `GET /admin/company/index` - Company management

## Database Schema

### Key Entities

- **OrderHeader** - Main order information
- **OrderDetail** - Individual items in an order
- **Product** - Book/product catalog
- **Category** - Product categories
- **ShoppingCart** - User shopping carts
- **ApplicationUser** - Extended user information
- **Company** - Business/tenant information

## Security Features

- **HTTPS Enforcement** - All connections secured
- **HSTS** - HTTP Strict Transport Security (30-day default)
- **Authentication** - ASP.NET Identity with email confirmation
- **Authorization** - Role-based access control (RBAC)
- **Two-Factor Authentication** - Optional 2FA support
- **CSRF Protection** - Built-in CSRF tokens on forms
- **Password Security** - Secure password hashing and validation

## Stripe Integration

### Payment Flow

1. User adds items to cart
2. User proceeds to checkout
3. Stripe Checkout Session is created
4. User is redirected to Stripe hosted checkout
5. Payment is processed
6. User is redirected to order confirmation page
7. `OrderConfirmation` action verifies payment status with Stripe
8. Order is marked as "Approved" upon successful payment

### Session Management

- Stripe Session IDs are stored in `OrderHeader.SessionId`
- Payment Intent IDs are stored in `OrderHeader.PaymentIntentId`
- Session verification occurs at `CartController.OrderConfirmation()`

## Email Configuration

Email notifications are sent for:
- Order confirmations
- Order status updates
- Password reset requests
- Email verification

Configure SMTP settings in `EmailSender` class or appsettings.

## Logging

Application logging is configured in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Troubleshooting

### Common Issues

**Database Connection Errors**
- Verify SQL Server is running
- Check connection string in `appsettings.json`
- Ensure database user has proper permissions

**Stripe Errors**
- Verify API keys are correct in `appsettings.json`
- Check Stripe dashboard for test/live mode
- Ensure webhook configuration for production

**Authentication Issues**
- Clear browser cookies and cache
- Verify email confirmation link
- Check `ApplicationDbContext` migrations

**Session Timeout**
- Adjust timeout in `Program.cs`:
  ```csharp
  options.IdleTimeout = TimeSpan.FromMinutes(100);
  ```

## Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Commit your changes: `git commit -m 'Add amazing feature'`
4. Push to the branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

## Deployment

### Azure Deployment

1. Create an Azure App Service
2. Create an Azure SQL Database
3. Configure connection strings in Azure portal
4. Deploy using Visual Studio or Azure CLI:
   ```bash
   dotnet publish -c Release
   az webapp deployment source config-zip -r <zipfile> -n <app-name> -g <resource-group>
   ```

### Local IIS Deployment

1. Publish the application: `dotnet publish -c Release -o publish`
2. Create a new IIS Application Pool (.NET CLR version: No Managed Code)
3. Create a new IIS Website pointing to the publish folder
4. Configure application pool identity and permissions

## Performance Optimization

- Use SQL Server query optimization
- Implement caching for frequently accessed data
- Enable gzip compression in IIS/Kestrel
- Optimize image sizes and use CDN for static assets
- Use Entity Framework Core async/await patterns

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For issues and questions:
- GitHub Issues: [Create an issue](https://github.com/mahmoudalsharkawy572/BulkyBookWeb/issues)
- Email: contact@example.com
- Documentation: [Stripe API Docs](https://stripe.com/docs)

## Acknowledgments

- Built with [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- Payment processing by [Stripe](https://stripe.com)
- UI framework [Bootstrap](https://getbootstrap.com)
- Authentication framework [ASP.NET Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)

## Project Links

- **Repository**: https://github.com/mahmoudalsharkawy572/BulkyBookWeb
- **Author**: [Mahmoud Alsharkawy](https://github.com/mahmoudalsharkawy572)
- **.NET Documentation**: https://docs.microsoft.com/en-us/dotnet
- **ASP.NET Core Documentation**: https://docs.microsoft.com/en-us/aspnet/core

---

**Last Updated**: 2024
**Version**: 1.0.0
**Status**: Active Development