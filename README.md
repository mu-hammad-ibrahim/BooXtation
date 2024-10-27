# BooXtation: The Future of Bookstore Management 📚✨

BooXtation is a robust online bookstore management system developed as part of the Digital Egypt Pioneers Initiative (DEPI). The project encapsulates the skills and knowledge gained throughout the full stack .NET web development program. BooXtation aims to bring efficiency and ease to digital bookstore operations, offering a powerful, scalable platform for both administrators and customers.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Usage](#usage)
- [GitHub Repository](#github-repository)
- [Demo](#demo)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)

## Features

- **3-Tier Architecture:** Separates Presentation, Business Logic, and Data Access layers for modularity and scalability.
- **Advanced Book Management:** Full CRUD operations, enhanced search and filtering, and custom recommendations.
- **Smooth Checkout Process:** Includes discounts, promo codes, and secure payment options with Stripe.
- **Email Integration with MailKit:** Efficient notifications for order confirmations and customer communications.
- **Comprehensive Admin Dashboard:** Full control over inventory, orders, user management, and analytics using Chart.js.
- **Customer-Centric Features:** Wishlist, review, and rating options for an improved shopping experience.

## Technologies Used

- **Backend:** .NET Framework & ASP.NET Core MVC
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Frontend:** JavaScript, CSS, Bootstrap
- **Real-time Updates:** AJAX
- **Payment Processing:** Stripe
- **Email Services:** MailKit

## Installation

To get started with BooXtation locally, follow these steps:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Muhammad-Ibrahim-Dev/BooXtation.git
   ```

2. **Navigate to the project directory:**
   ```bash
   cd BooXtation
   ```

3. **Install dependencies:**
   - If you're using the .NET CLI, run:
   ```bash
   dotnet restore
   ```

4. **Set up the database:**
   - Ensure you have SQL Server installed and configured.
   - Update the connection string in the `appsettings.json` file.

5. **Run the application:**
   ```bash
   dotnet run
   ```

## Usage

- Open your web browser and navigate to `http://localhost:5000` to access the BooXtation application.
- Create an account, browse books, manage inventory, and enjoy the features available!

## GitHub Repository

- [GitHub Repository Link](https://github.com/Muhammad-Ibrahim-Dev/BooXtation)

## Demo

- Check out the demo video [here](https://drive.google.com/file/d/1L6lZ36MLTaOkiCkdSi76M2qDyeuWJGGm/view?usp=sharing).

## Contributing

We welcome contributions! If you'd like to contribute to BooXtation, please fork the repository and create a pull request.

1. Fork the repository
2. Create your feature branch:
   ```bash
   git checkout -b feature/YourFeature
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add some feature"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/YourFeature
   ```
5. Open a pull request.

## License

This project is licensed under the [Creative Commons Attribution-NonCommercial 4.0 International License](LICENSE.md). You are free to share and adapt this work non-commercially, provided you give appropriate credit.

## Acknowledgments

- Special thanks to my teammates Mahmoud Mohamed, Hadeer Ragab, Mina Nader, and Noha Nasser for their hard work and to Mohamed Agour for his invaluable guidance during this project.
