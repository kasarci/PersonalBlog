namespace PersonalBlog.DataAccess.Entities.Abstract;

public abstract record BlogItemBase : IBlogItem
{
        public Guid Id { get ; init ; } = Guid.NewGuid();
        public bool IsActive { get ; init ; } = true;
        public DateTime CreatedAt { get ; init ; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
}