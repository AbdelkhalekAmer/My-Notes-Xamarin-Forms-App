using MyNotes.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Services.Contracts
{
    public interface INoteStore : IDataStore<Note>
    {
        Task<IEnumerable<string>> GetCoursesAsync();
    }
}
