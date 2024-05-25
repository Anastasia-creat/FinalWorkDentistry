namespace FinalWorkDentistry.Abstract
{
    public interface IRepository<T>
    {
        T FindByName(string name);

        void Create(T model);

        T Read(long id);

        T ReadWithRelations(long id);
        void Update(T model);

        void Delete(long id);

        IEnumerable<T> GetList(); //выдать список
    }
}
