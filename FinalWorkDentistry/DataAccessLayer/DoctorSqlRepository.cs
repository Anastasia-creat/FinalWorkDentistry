using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkDentistry.DataAccessLayer
{
    public class DoctorSqlRepository : IRepository<Doctor>
    {
        private readonly ApplicationDbContext _context;

        public DoctorSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Doctor model)
        {
            _context.Doctors.Add(model);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entry = _context.Doctors.Find(id);
            _context.Doctors.Remove(entry);
            _context.SaveChanges();
        }

        public Doctor FindByName(string name) => _context.Doctors.FirstOrDefault(x => x.Name == name);      
      

        public IEnumerable<Doctor> GetList()
        {
            return _context.Doctors.Include(p => p.DoctorCategory);
        }

        public Doctor Read(long id)
        {
            var entry = _context.Doctors
               .Include(p => p.DoctorCategory)
               .FirstOrDefault(p => p.DoctorId == id);
            return entry;
        }

        public Doctor ReadWithRelations(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Doctor model)
        {
            var entry = _context.Doctors.Find(model.DoctorId);
            _context.Entry(entry).CurrentValues.SetValues(model);
            entry.DoctorCategory = model.DoctorCategory;
            _context.SaveChanges();
        }
    }
}
