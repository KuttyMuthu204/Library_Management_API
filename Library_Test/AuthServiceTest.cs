using Library_Management.Controllers;
using Library_Management.DBContext;
using Library_Management.Models;
using Library_Management.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace Library_Test
{
    public class AuthServiceTest
    {
        private readonly AuthService _authService;
        private readonly ApplicationDbContext _context;

        public AuthServiceTest()
        {
            var options = GetDbContextOptions();
            var configurations = GetConfigurationBuilder(false);

            _context = new ApplicationDbContext(options);
            _authService = new AuthService(configurations, _context);
        }

        [Fact]
        public async Task GetJWTToken_WithNullLoginModel_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _authService.GetJWTToken(null!));
            Assert.NotNull(ex);
            Assert.NotEmpty(ex.Message);
            Assert.Contains("Value cannot be null.", ex.Message);
        }

        [Fact]
        public async Task GetJWTToken_WithNullUser_ShouldThrowInvalidCredentialException()
        {
            // Arrange
            await SeedLoginUsers(_context);
            var loginModel = new LoginModel
            {
                Username = "muthuraman.s@gamil.com",
                Password = "Pass1234"
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidCredentialException>(() => _authService.GetJWTToken(loginModel));
            Assert.NotNull(ex);
            Assert.NotEmpty(ex.Message);
            Assert.Contains("Invalid username or password.", ex.Message);
        }

        [Fact]
        public async Task GetJWTToken_WithValidUser_ShouldReturnJWTToken()
        {
            // Arrange
            await SeedLoginUsers(_context);
            var loginModel = new LoginModel
            {
                Username = "michael.johnson@example.com",
                Password = "Pass1234"
            };

            // Act
            var response = await _authService.GetJWTToken(loginModel);

            // Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.IsType<string>(response);
        }

        [Fact]
        public async Task GetJWTToken_WithInvalidJWTConfiguration_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "michael.johnson@example.com",
                Password = "Pass1234"
            };

            var options = GetDbContextOptions();
            var configurations = GetConfigurationBuilder(true); // Simulate missing JWT configuration
            var applicationDbContext = new ApplicationDbContext(options);
            var authService = new AuthService(configurations, applicationDbContext);
            await SeedLoginUsers(applicationDbContext);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => authService.GetJWTToken(loginModel));
            Assert.NotNull(ex);
            Assert.NotEmpty(ex.Message);
            Assert.Contains("JWT configuration is missing in appsettings.json.", ex.Message);
        }

        [Fact]
        public void Controller_WithNullDBContext_ShouldThrowArgumentNullException()
        {
            // Arrange
            var configurations = GetConfigurationBuilder(false);
            
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new AuthService(configurations, null!));
            Assert.NotNull(ex);
            Assert.NotEmpty(ex.Message);
            Assert.Contains("Value cannot be null.", ex.Message);
        }

        [Fact]
        public void Controller_WithNullConfiguration_ShouldThrowArgumentNullException()
        {
            // Arrange
            var options = GetDbContextOptions();
            var applicationDbContext = new ApplicationDbContext(options);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new AuthService(null!, applicationDbContext));
            Assert.NotNull(ex);
            Assert.NotEmpty(ex.Message);
            Assert.Contains("Value cannot be null.", ex.Message);
        }

        private DbContextOptions<ApplicationDbContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        }

        private IConfigurationRoot GetConfigurationBuilder(bool isFake)
        {
            if (!isFake)
                return new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>            {
                { "Jwt:Key", "librarymanagmentsupersecretkey12345" },
                { "Jwt:Issuer", "LibraryManagementAPI" },
                { "Jwt:Audience", "LibraryManagementAPIUsers" }
            }).Build();
            else
                return new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>()).Build();
        }

        private async Task SeedLoginUsers(ApplicationDbContext context)
        {
            var users = new List<Users>
            {
                new Users
                {
                    FirstName = "Michael",
                    LastName = "Johnson",
                    EmailId = "michael.johnson@example.com",
                    Password = "Pass1234",
                    Country = "United States"
                },
                new Users
                {
                    FirstName = "Priya",
                    LastName = "Kumar",
                    EmailId = "priya.kumar@example.com",
                    Password = "SecurePwd9",
                    Country = "India"
                }
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
