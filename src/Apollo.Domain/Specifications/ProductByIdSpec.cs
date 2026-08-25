using Apollo.Domain.Entities;

namespace Apollo.Domain.Specifications;

public sealed class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(Guid id)
    {
        Query(product => product.Id == id);
        EnableTracking();
    }
}
