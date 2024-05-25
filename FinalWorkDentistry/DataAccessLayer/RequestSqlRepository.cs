using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;

namespace FinalWorkDentistry.DataAccessLayer;

public class RequestSqlRepository : IRepository<Request>
{
    private readonly ApplicationDbContext _context;

    public RequestSqlRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(Request model)
    {
       _context.Requests.Add(model);
        _context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entry = _context.Requests.Find(id);
        _context.Requests.Remove(entry);
        _context.SaveChanges();
    }

    public Request FindByName(string name) => _context.Requests.FirstOrDefault(x => x.Name == name);
   

    public IEnumerable<Request> GetList()
    {
       return _context.Requests;
    }

    public Request Read(long id)
    {
      var entry = _context.Requests.FirstOrDefault(x => x.RequestId == id);
        return entry;
    }

    public Request ReadWithRelations(long id)
    {
        throw new NotImplementedException();
    }

    public void Update(Request model)
    {
        var entry = _context.Requests.Find(model.RequestId);
        _context.Entry(entry).CurrentValues.SetValues(entry);
        entry.RequestId = model.RequestId;
        _context.SaveChanges();
    }
}
