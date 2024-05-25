using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkDentistry.DataAccessLayer
{
    public class CategoryUslugiSqlRepository : IRepository<CategoryService>
    {
        private readonly ApplicationDbContext _context;

        public CategoryUslugiSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public void Create(CategoryService model)
        {
            var existing = FindByName(model.Name);
            if (existing == null)
            {
                _context.CategoryService.Add(model);
                _context.SaveChanges();
            }
            else
            {
                model.CategoryServiceId = existing.CategoryServiceId;
                // TODO: бросаем эксепшн?
            }
        }

        public void Delete(long id)
        {
            var entry = _context.CategoryService.Find(id);
            _context.CategoryService.Remove(entry);
            _context.SaveChanges();
        }

        public CategoryService FindByName(string name) =>
            _context.CategoryService
                .FirstOrDefault(c => c.Name == name);

        public CategoryService Read(long id)
        {
            var entry = _context.CategoryService.Find(id);
            return entry;
        }

        public CategoryService ReadWithRelations(long id)
        {
            var entry = _context
               .CategoryService
               .Include(c => c.ServiceOfCategory)
               .FirstOrDefault(c => c.CategoryServiceId == id);
            return entry;
        }

        public void Update(CategoryService model)
        {
            var entry = _context.CategoryService.Find(model.CategoryServiceId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            // связи - relation - нужно менять дополнительно
            _context.SaveChanges();
        }

        //CategoryUslugi IRepository<CategoryUslugi>.FindByName(string name)
        //{
        //    throw new NotImplementedException();
        //}

        IEnumerable<CategoryService> IRepository<CategoryService>.GetList()
        {
            return _context.CategoryService;

        }

        //CategoryService IRepository<CategoryService>.Read(long id)
        //{
        //    var entry = _context.CategoryService.Find(id);
        //    return entry;
        //}

        //CategoryService IRepository<CategoryService>.ReadWithRelations(long id)
        //{
        //    var entry = _context
        //         .CategoryService
        //         // .Include(c => c.DoctorsOfCategory)
        //         .FirstOrDefault(c => c.CategoryServiceId == id);
        //    return entry;
        //}

        
    }
}
