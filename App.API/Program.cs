using Scalar.AspNetCore;

namespace App.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options => { options.WithTitle("Demo Application").WithTheme(ScalarTheme.Purple).WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient); }); // /scalar/v1
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
