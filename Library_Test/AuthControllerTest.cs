using Library_Management.Controllers;
using Library_Management.Models;
using Library_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Test
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _authService;
        private readonly AuthController _authController;

        private const string JWT_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VybmFtZSIsIm5hbWUiOiJ1c2VybmFtZSIsImp0aSI6IjEyMzQ1NiIsImlhdCI6MTY4NzQyMDAwMCwiZXhwIjoxNjg3NDIzNjAwfQ.abc123def456ghi789jkl012mno345pqr678stu901vwx234yz567890abc123d";

        public AuthControllerTest()
        {
            _authService = new Mock<IAuthService>();
            _authController = new AuthController(_authService.Object);
        }

        [Fact]
        public async Task Login_WithInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            _authController.ModelState.AddModelError("UserName", "UserName is required");

            var loginModel = new LoginModel
            {
                Username = "",
                Password = "password"
            };

            // Act
            var result = await _authController.Login(loginModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.True(errorResponse.ContainsKey("UserName"));
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsJWTToken()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "UserName",
                Password = "Password"
            };

            _authService.Setup(c => c.GetJWTToken(loginModel)).ReturnsAsync(JWT_TOKEN);

            // Act
            var result = await _authController.Login(loginModel);

            // Assert
            var oKResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<string>(oKResult.Value);
            Assert.NotNull(oKResult);
            Assert.Equal(JWT_TOKEN, response);
            Assert.Equal(200, oKResult.StatusCode);
        }

        [Fact]
        public async Task Login_WithException_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "UserName",
                Password = "Password"
            };

            _authService.Setup(s => s.GetJWTToken(loginModel)).ThrowsAsync(new Exception("DB Error"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _authController.Login(loginModel));
        }
    }
}
