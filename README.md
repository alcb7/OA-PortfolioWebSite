# OA-PortfolioWebSite
Usage
Authentication and Authorization
JWT is used for securing the API endpoints. To access protected routes, users must authenticate via the /api/auth/authenticate endpoint.
Users can register via /api/auth/register.
Contact Form
The contact form on the homepage allows users to send messages. The message is sent to a specified email, and the user receives a confirmation email.
API Documentation
User API
Register: POST /api/auth/register
Login: POST /api/auth/authenticate
Get User by ID: GET /api/auth/{id}
File API
Upload File: POST /api/file/upload
About Me API
Get About Me: GET /api/aboutme/1
Edit About Me: PUT /api/aboutme/{id}
Admin API
The Admin panel uses JWT for managing various aspects of the application.
Contribution Guidelines
Fork the repository and create your branch from main.
Ensure your code is well-commented and follows the existing code style.
Make sure your changes pass the tests.
License
This project is licensed under the MIT License.

