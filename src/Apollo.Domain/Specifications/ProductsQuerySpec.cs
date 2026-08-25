using Apollo.Domain.Entities;

namespace Apollo.Domain.Specifications;

public sealed class ProductsQuerySpec : Specification<Product>
{
    public ProductsQuerySpec(string? name = null, bool? isActive = null, int skip = 0, int take = 50)
    {
        Query(product =>
            (string.IsNullOrWhiteSpace(name) || product.Name.Contains(name)) &&
            (!isActive.HasValue || product.IsActive == isActive.Value));

        ApplyOrderBy(product => product.Name);
        ApplyPaging(skip, take);
    }
}
