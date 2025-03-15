using Dapper;
using Libreria.Application.DTOs;
using Libreria.Application.Gateways;
using Libreria.Application.Interfaces.ICommon;

namespace Libreria.Infrastructura.DAOs.UsuarioDAO;

public class UsuarioDAO : IUsuarioGateway
{
    private DbSession _session;
    private readonly ILogServicesInteractor _logServicesInteractor;

    public UsuarioDAO(ILogServicesInteractor logServicesInteractor, DbSession session)
    {
        _logServicesInteractor = logServicesInteractor;
        _session = session;
    }

    public async Task<UserDTO> GetUser(string user, string password)
    {
        try
        {
            var sql = ScriptRecursos.Scripts.UsuarioDAO_GetUser;
            var parametros = new DynamicParameters();
            parametros.Add("@user", user);
            parametros.Add("@password", password);

            var result = await _session.Connection.QueryAsync<UserDTO>(sql, parametros);
            
            _session.Connection.Close();

            if (!result.Any())
                return null;

            return result.FirstOrDefault()!;

        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(UsuarioDAO) + nameof(GetUser) + ex.Message);
            return null;
        }
    }
}
