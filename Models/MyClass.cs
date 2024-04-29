namespace ReflectionConsoleApp.Models
{
    public class MyClass
    {
        private static int id { get; set; } = 1;
        public int Id { get; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public MyClass(){}
        public MyClass(string name)
        {
            Id = id;
            id++;
            Name = name;
            IsActive = true;
            CreateDate = DateTime.Now;
        }
        public string SayMyName() => $"Меня зовут {Name}";
    }
}
