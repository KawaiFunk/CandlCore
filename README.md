# CandlCore

CandlCore is a .NET Web API that displays cryptocurrency asset data from the [CoinLore API](https://www.coinlore.com/cryptocurrency-data-api). The application emphasizes clean architecture and implements multiple design patterns and OOP principles.

## 🧠 Features

- Fetches and displays real-time crypto asset data
- Layered architecture (API, Application, Domain, Infrastructure)
- Ready for Flutter UI integration
- Background jobs support

## 🧱 Design Patterns Implemented

- **Builder** – Constructs CoinLore API URLs
- **Proxy** – Adds caching to external service calls
- **Factory** – Creates external API clients
- **Mediator** – Decouples controller and handler communication (custom implementation)
- **Repository & Generic Repository** – Data access abstraction

## ✅ OOP Principles

Follows SOLID principles:
- SRP, OCP, LSP, ISP, DIP

## 📁 Project Structure (Simplified)


## 🔜 Upcoming

- Flutter frontend client to consume this API and display the data in the user-friendly UI.
