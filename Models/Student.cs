namespace Models
{
    public class Student
    {
        public Student(int id, string firstname, string lastname, string statut)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Statut = statut;
        }

        public int Id { get; set;}
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Statut { get; set; }
    }
}