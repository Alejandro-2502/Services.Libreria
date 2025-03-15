using Libreria.Application.Gateways;
using Libreria.Application.Interactors;
using Libreria.Application.Interactors.Common;
using Libreria.Application.Interactors.Token;
using Libreria.Application.Interfaces;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Interfaces.Token;
using Libreria.Application.Validations;
using Libreria.Infrastructura;
using Libreria.Infrastructura.DAOs.LibroDAO;
using Libreria.Infrastructura.DAOs.UsuarioDAO;

namespace Services.Libreria.Extensions;

public static class IInjectionExtension
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            services.AddSingleton<ILibroCommandGateway, LibroCommandDao>();
            services.AddSingleton<ILibroQuerysGateway, LibroQuerysDao>();
            services.AddSingleton<ILibroCommandInteractor, LibroCommandInteractor>();
            services.AddSingleton<ILibroQuerysInteractor, LibroQuerysInteractor>();
            services.AddSingleton<IUsuarioInteractor, UsuarioInteractor>();
            services.AddSingleton<ITokenInteractor, TokenInteractor>();
            services.AddSingleton<IUsuarioGateway, UsuarioDAO>();

            services.AddSingleton<LibroValidations>();
            services.AddSingleton<IValidationsInteractor, ValidationsInteractor>();
            services.AddSingleton<DbSession>();

            services.AddScoped<ResponseHttp>();
            services.AddSingleton<ILogServicesInteractor>(new LogServicesInteractor("logfileLibreria.txt"));
      
            return services;
        }
    }