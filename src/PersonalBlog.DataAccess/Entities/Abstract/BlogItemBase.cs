namespace PersonalBlog.DataAccess.Entities.Abstract;

public abstract record BlogItemBase : IBlogItem
{
        public Guid Id { get ; set ; } = Guid.NewGuid();
        public bool IsActive { get ; init ; } = true;
        public DateTimeOffset CreatedAt { get ; init ; } = DateTime.UtcNow;
        public DateTimeOffset? ModifiedAt { get; set; }
}