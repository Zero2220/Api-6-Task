namespace UniversityApi.Data
{
    public class Group
    {
        public int Id { get; set; }
        public string No { get; set; }
        public byte Limit { get; set; }
        public List<Student> Students { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
