namespace ConceptArchitect.Utils
{
    public interface IRepository<Entity,Id>
    {
        Task<Entity> Add(Entity entity);
        Task<List<Entity>> GetAll();
        Task<List<Entity>> GetAllF(string uname);
        Task<List<Entity>> GetAll(Func<Entity, bool> predicate);
        Task<Entity> fav(Entity entity, string userId);

        Task<Entity> GetById(Id id);

        Task<Entity> Update(Entity entity, Action<Entity,Entity> mergeOldNew);
        Task DeleteFav(string bookId, string userId); 

        Task Delete(Id id);
        Task Delete(string id, string uname);

    }
}