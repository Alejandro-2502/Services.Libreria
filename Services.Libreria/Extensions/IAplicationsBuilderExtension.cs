namespace Services.Libreria.Extensions;

public static class IAplicationsBuilderExtension
{
    public static IApplicationBuilder Configure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        UseSwaggerUI(app);
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        return app;
    }

    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Servicio Libreria");
        });

        return app;
    }
}
