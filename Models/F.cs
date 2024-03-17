namespace ReflectionConsoleApp.Models
{
    [Serializable]
    public class F
    {
        public int i1 { get; set; }
        public int i2 { get; set; }
        public int i3 { get; set; }
        public int i4 { get; set; }
        public int i5 { get; set; }
        public bool IsTest { get; set; }
        public DateTime DateCreate { get; set; }

        public F()
        {
            i1 = 1;
            i2 = 2;
            i3 = 3;
            i4 = 4;
            i5 = 5;
            IsTest = true;
            DateCreate = DateTime.Now;
        }
    }
}
