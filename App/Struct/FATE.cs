namespace App
{
    public class FATE
    {
        public Area Area { get; set; }
        public string Name { get; set; }

        public static explicit operator FATE(string name)
        {
            return new FATE { Name = name };
        }
    }
}
