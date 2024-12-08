# FoodyWeb
http://foodyweb.somee.com/
FoodyWeb is a web application designed to manage food orders and payments. This project demonstrates the integration of various technologies including ASP.NET Core MVC, Entity Framework Core, Identity for authentication, and Stripe for payment processing.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Setup](#setup)
- [Usage](#usage)
- [Project Structure](#project-structure)


## Features

- User authentication and authorization
- Product management
- Shopping cart functionality
- Order processing
- Stripe payment integration
- Two-factor authentication (2FA)

## Technologies

- **Backend**: ASP.NET Core 8.0
- **Frontend**: Razor Pages, JavaScript, Bootstrap, jquery.
- **Database**: Entity Framework Core with SQL Server 
- **Payment Processing**: Stripe
- **Others**: Identity for user management

## Setup

### Prerequisites

- .NET 8.0 SDK
- SQL Server 
- Stripe account for payment processing

### Installation

1. Clone the repository:
```bash
    git clone https://github.com/yourusername/FoodyWeb.git
    cd FoodyWeb
```

2. Set up the database:
    - Update the connection string in `appsettings.json` for your database.
```bash
     "ConnectionStrings": {
   "DefaultConnection": "Server=Your_server_name; Database=Your_Db_name;Trusted_Connection=True;TrustSeRverCertificate=True"
 }
 ```

3. Configure Stripe:
    - Add your Stripe API keys in `appsettings.json`:
  
  
```bash
   {
      "Stripe": {
          "SecretKey": "your_secret_key",
          "PublishableKey": "your_publishable_key"
      }
  }
 ```
## Usage

- Navigate to `https://localhost:5001` in your web browser.
- Register a new user or log in with an existing account.
- Add products to the shopping cart and proceed to checkout.
- Complete the payment using Stripe.

## Project Structure

- **FoodyWeb**: Main web application project.
- **Foody.DataAccess**: Data access layer using Entity Framework Core.
- **Foody.Models**: Contains the data models.
- **Foody.Utility**: Utility classes and helpers.

## Roles and Accessibility

### Admin

- **Access**: Full access to all features and functionalities.
- **Capabilities**:
  - Manage users and roles.
  - Add, edit, and delete products.
  - View and manage all orders.


### Employee

- **Access**: Limited administrative access.
- **Capabilities**:
  - Manage products (add, edit, delete).
  - View and manage orders.


### Customer

- **Access**: Basic access to user-specific features.
- **Capabilities**:
  - Browse products.
  - Add products to the shopping cart.
  - Place orders and make payments.
  - View order history and status.

### Guest ( Not Log in )

- **Access**: Limited access to public features.
- **Capabilities**:
  - Browse products.
  - Register for an account.

