namespace Dapper_Example.DAL
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public bool IsRowActive { get; set; }
    }
}
