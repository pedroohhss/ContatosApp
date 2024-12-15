using ContatosApp.Models;
using SQLite;

namespace ContatosApp.Services;
public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Contato>().Wait();
    }

    public async Task<List<Contato>> GetContatosAsync()
    {
        return await _database.Table<Contato>().OrderBy(x => x.Nome).ToListAsync();
    }

    public async Task<Contato> GetContatoByIdAsync(int id)
    {
        return await _database.Table<Contato>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> AdicionarContatoAsync(Contato contato)
    {
        if (ValidarCampos(contato))
            return await _database.InsertAsync(contato);

        throw new InvalidDataException("Algum dos campos não é válido");
    }

    public async Task<int> AtualizarContatoAsync(Contato contato)
    {
        return await _database.UpdateAsync(contato);
    }

    public async Task<int> ExcluirContatoAsync(Contato contato)
    {
        return await _database.DeleteAsync(contato);
    }

    private static bool ValidarCampos(Contato contato)
    {
        if (string.IsNullOrWhiteSpace(contato.Nome))
            return false;
        else if (string.IsNullOrWhiteSpace(contato.Telefone))
            return false;
        else if (string.IsNullOrWhiteSpace(contato.Email))
            return false;

        return true;
    }
}
