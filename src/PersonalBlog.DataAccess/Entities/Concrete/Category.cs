using System.Runtime.InteropServices;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Entities.Concrete
{
    public record Category : BlogItemBase, IBlogItem
    {
        public string Name { get; init; } = null!;
    }
}