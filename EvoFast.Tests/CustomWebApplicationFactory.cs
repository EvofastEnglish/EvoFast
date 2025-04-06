using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace EvoFast.Tests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddAuthentication("DefineForTest") // Đăng ký phương thức xác thực với tên scheme "Test"
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("DefineForTest", options => { }); // Thêm một scheme "Test" với handler tùy chỉnh

            services.Configure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = "DefineForTest"; // Đặt phương thức xác thực mặc định là "Test"
                options.DefaultChallengeScheme = "DefineForTest"; // Đặt phương thức xác thực mặc định là "Test"
            });
        });
    }
}