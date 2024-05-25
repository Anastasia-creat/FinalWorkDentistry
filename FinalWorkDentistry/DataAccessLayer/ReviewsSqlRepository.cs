using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkDentistry.DataAccessLayer
{
    public class ReviewsSqlRepository : IRepository<Reviews>
    {
        private readonly ApplicationDbContext _context;

        public ReviewsSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Reviews model)
        {
            _context.Reviews.Add(model);
            _context.SaveChanges();
        }

       

        public void Delete(long id)
        {
            var entry = _context.Reviews.Find(id);
            _context.Reviews.Remove(entry);
            _context.SaveChanges();
        }

        public Reviews FindByName(string name) => _context.Reviews.FirstOrDefault(r => r.NameReview == name);
      

        public IEnumerable<Reviews> GetList()
        {
            return _context.Reviews;
        }

        public Reviews Read(long id)
        {
            var entry = _context.Reviews
             //  .Include(p => p.ReviewId)
               .FirstOrDefault(p => p.ReviewId == id);
            return entry;
        }

        public Reviews ReadWithRelations(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Reviews model)
        {
            var entry = _context.Reviews.Find(model.ReviewId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            entry.ReviewId = model.ReviewId;
            _context.SaveChanges();
        }

     

  
    }
}
