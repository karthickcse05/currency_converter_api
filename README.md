# currency_converter_api
api for the currency converter

1. Project Setup
- Use ASP.NET Core Web API template.
- Configure dependency injection for services like logging, authentication, and exchange rate providers.
- Set up API versioning to future-proof updates.
2. Exchange Rate Services
- Implement a service layer for interacting with the Frankfurter API.
- Apply caching using Redis to reduce API calls.
- Introduce retry policies and circuit breakers using Polly for robustness.
3. Endpoints
- Latest Exchange Rates (GET /api/currency/rates/{baseCurrency}) — Fetch latest rates.
- Currency Conversion (POST /api/currency/convert) — Convert amounts, enforcing currency exclusions.
- Historical Rates (GET api/currency/historical) — Paginated results for a given timeframe.
4. Security Implementation
- JWT authentication for user validation.
- RBAC enforcement to restrict actions based on roles.
- Rate limiting to prevent abuse using ASP.NET Core IP rate-limiting middleware.
5. Logging & Monitoring
- Serilog + Seq for structured logging.
- OpenTelemetry for distributed tracing.
- Store request metadata (client IP, token, method, etc.) for debugging.
6. Testing & QA
- Use xUnit or NUnit for unit tests (90%+ coverage).
- Integration tests with TestServer to simulate API calls.
- Generate test coverage reports using Coverlet.
7. Deployment Considerations
- Dockerize the API for containerized deployment.
- Support horizontal scaling with Kubernetes.
- Ensure CI/CD integration for seamless updates.

