using Dapper;
using System.Reflection;

namespace Libreria.Infrastructura.DAOs.Helper
{
    public static class CreateParameters
    {
        public static DynamicParameters GetParameters<T>(T ObjetoDTO)
        {
            var parametros = new DynamicParameters();
            var propiedades = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propiedad in propiedades)
            {
                var parameterName = $"@{propiedad.Name.ToLower()}";
                var dato = propiedad.GetValue(ObjetoDTO);
                parametros.Add(parameterName, dato);
            }

            return parametros;
        }
    }
}
