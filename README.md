# RetailProductMicroservice

This is a microservice project for managing the logistics movements of retail products for a cell phone accessories store. The microservice is built using ASP.NET Core with a RESTful architecture and follows the principles of Domain-Driven Design (DDD).

## Project Structure

The project follows a modular structure, with separate projects for different layers of the application. Here is an overview of the project structure:

- `RetailProductMicroservice.Api`: This project contains the API controllers for handling product and logistic endpoints.
- `RetailProductMicroservice.Application`: This project contains the application services for managing products and logistics.
- `RetailProductMicroservice.Domain`: This project contains the domain entities and interfaces for accessing and manipulating data.
- `RetailProductMicroservice.Infrastructure`: This project contains the data access layer, including the database context and repositories.
- `RetailProductMicroservice.Tests`: This project contains unit tests and integration tests for the application.

## Functionality

The microservice provides the following functionality:

- Product Management: The owner of the store can view all products and get the details of a specific product. Products can be withdrawn due to sales, breakage, or inventory adjustment. New product entries can be registered through purchase or loan.
- Logistic Management: The owner can view the incoming and outgoing product movements between warehouses, as well as movements between warehouses. Logistic movements can be registered for incoming, outgoing, and transfers between warehouses.

## Getting Started

To run the microservice locally, follow these steps:

1. Clone the repository.
2. Set up the SQL database and update the connection string in the `appsettings.json` file.
3. Build the solution using Visual Studio or the .NET CLI.
4. Run the database migrations to create the necessary tables.
5. Start the microservice by running the `RetailProductMicroservice.Api` project.

## Testing

The microservice includes unit tests and integration tests to ensure the correctness of the application logic. You can run the tests using Visual Studio Test Explorer or the .NET CLI.

## Contributing

Contributions to the project are welcome. If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
```

Please note that this is a basic README template and you can customize it according to your specific project requirements and preferences.