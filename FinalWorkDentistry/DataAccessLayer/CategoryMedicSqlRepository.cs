using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkDentistry.DataAccessLayer
{
    public class CategoryMedicSqlRepository : IRepository<CategoryDoctor>
    {
        private readonly ApplicationDbContext _context;

        public CategoryMedicSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(CategoryDoctor model)
        {
            var existing = FindByName(model.Name);
            if (existing == null)
            {
                _context.CategoryDoctor.Add(model);
                _context.SaveChanges();
            }
            else
            {
                model.CategoryDoctorId = existing.CategoryDoctorId;
                // TODO: бросаем эксепшн?
            }
        }

        public void Delete(long id)
        {
            var entry = _context.CategoryDoctor.Find(id);
            _context.CategoryDoctor.Remove(entry);
            _context.SaveChanges();
        }

        public CategoryDoctor FindByName(string name) =>
          _context.CategoryDoctor
              .FirstOrDefault(c => c.Name == name);



        public IEnumerable<CategoryDoctor> GetList()
        {
            return _context.CategoryDoctor;
        }

        public CategoryDoctor Read(long id)
        {
            var entry = _context.CategoryDoctor.Find(id);
            return entry;
        }

        public CategoryDoctor ReadWithRelations(long id)
        {
            var entry = _context
                 .CategoryDoctor
                  .Include(c => c.DoctorOfCategory)
                 .FirstOrDefault(c => c.CategoryDoctorId == id);
            return entry;
        }

        public void Update(CategoryDoctor model)
        {
            var entry = _context.CategoryDoctor.Find(model.CategoryDoctorId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            // связи - relation - нужно менять дополнительно
            _context.SaveChanges();
        }
    }
}
