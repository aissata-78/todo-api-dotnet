using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace TodoApi.Models;

[CollectionName("roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{
}
