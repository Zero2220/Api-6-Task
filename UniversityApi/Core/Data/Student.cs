namespace UniversityApi.Data
{
    public class Student
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Group Group { get; set; }

        public string ImageName { get; set; }
    }
}
                    