using Hermes.Models;
using System.Threading.Tasks;

namespace Hermes.Repositories;

public class StopRepository(HermesContext db) : BaseRepository<Stop>(db)
{
    public async Task RestoreAsync(Stop stop)
    {
        stop.IsRestored = true;
        await this.SaveChangesAsync();
    }
}