using Dapper;
using Libreria.Application.DTOs;
using Libreria.Application.Gateways;
using Libreria.Application.Interfaces.ICommon;

namespace Libreria.Infrastructura.DAOs.LibroDAO;

public class LibroQuerysDao : ILibroQuerysGateway
{
    private DbSession _session;
    private readonly ILogServicesInteractor _logServicesInteractor;

    public LibroQuerysDao(DbSession dbSession, ILogServicesInteractor logServicesInteractor)
    {
        _session = dbSession;
        _logServicesInteractor = logServicesInteractor;
    }
    public async Task<List<LibroDTO>> GetAll()
    {
        try
        {
            var sql = ScriptRecursos.Scripts.LiroQuerysDAO_GetAll;
            var result = await _session.Connection.QueryAsync<LibroDTO>(sql);
            _session.Connection.Close();

            if (!result.Any())
                return null;

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroQuerysDao) + nameof(GetAll) + ex.Message);
            return null;
        }
    }

    public async Task<LibroDTO> GetById(int id)
    {
        try
        {
            var sql = ScriptRecursos.Scripts.LibroQuerysDAO_GetById;
            var parameters = new DynamicParameters();
            parameters.Add($"@{nameof(id).ToLower()}", id);
            var result = await _session.Connection.QueryAsync<LibroDTO>(sql, parameters);

            _session.Connection.Close();

            if (!result.Any())
                return null;

            return result.FirstOrDefault()!;

        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroQuerysDao) + nameof(GetById) + ex.Message);
            return null;
        }
    }

    public async Task<List<LibroDTO>> GetByPriceGreaterThan(decimal price)
    {
        try
        {
            var sql = ScriptRecursos.Scripts.LibroQuerysDAO_GetByPriceGreaterThan;
            var parameters = new DynamicParameters();
            parameters.Add("@price", price);
            var result = await _session.Connection.QueryAsync<LibroDTO>(sql, parameters);

            _session.Connection.Close();

            if (!result.Any())
                return null;

            return result.ToList();

        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(LibroQuerysDao) + nameof(GetByPriceGreaterThan) + ex.Message);
            return null;
        }
    }
}