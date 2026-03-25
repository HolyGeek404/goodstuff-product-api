namespace GoodStuff.ProductApi.Application.Interfaces;

public interface IReadRepository<TProduct>
{
    public Task<IEnumerable<TProduct>> GetByType(string category);
    public Task<TProduct?> GetById(string category, string id);
}