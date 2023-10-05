
    public class Student
    {
        private string firstname;
        private string lastname;
        public Student(string lastname , string firstname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string Firstname
        {
            get => firstname;
            set => firstname = value;
        }

        public string LastName {
            get => this.lastname ;
            set => this.lastname  = value;
        }
    }

