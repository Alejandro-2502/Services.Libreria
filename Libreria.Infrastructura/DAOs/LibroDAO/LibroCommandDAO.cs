using Dapper;
using Libreria.Application.DTOs;
using Libreria.Application.Gateways;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Infrastructura.DAOs.Helper;

namespace Libreria.Infrastructura.DAOs.LibroDAO;

public class LibroCommandDao : ILibroCommandGateway
{
    private DbSession _session;
    private readonly ILogServicesInteractor _logServicesInteractor;
    public LibroCommandDao(DbSession dbSession, ILogServicesInteractor logServicesInteractor)
    {
        _session = dbSession;
        _logServicesInteractor = logServicesInteractor;
    }
    public async Task<bool> Delete(int id)
    {
        try
        {
            var sql = ScriptRecursos.Scripts.LibroCommandDAO_Delete;
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            var result = await _session.Connection.ExecuteAsync(sql, parameters);

            _session.Connection.Close();

            if (Convert.ToBoolean(result) is false)
                return Convert.ToBoolean(result);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroCommandDao) + nameof(Delete) + ex.Message);
            return false;
        }
    }

    public async Task<bool> Add(LibroDTO libroDTO)
    {
        try
        {
            var sql = ScriptRecursos.Scripts.LibroCommandDAO_Add;
            var parameters = CreateParameters.GetParameters(libroDTO);
            var result = await _session.Connection.ExecuteAsync(sql, parameters);

            _session.Connection.Close();

            if (Convert.ToBoolean(result) is false)
                return Convert.ToBoolean(result);

            return Convert.ToBoolean(result);

        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroCommandDao) + nameof(Add) + ex.Message);
            return false;
        }
    }

    public async Task<bool> Update(LibroDTO libroDTO)
    {
        try
        {
            var sql = ScriptRecursos.Scripts.LibroCommandDAO_Update;
            var parameters = CreateParameters.GetParameters(libroDTO);

            var result = await _session.Connection.ExecuteAsync(sql, parameters);

            _session.Connection.Close();

            if (Convert.ToBoolean(result) is false)
                return Convert.ToBoolean(result);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroCommandDao) + nameof(Update) + ex.Message);
            return false;
        }
    }
}
