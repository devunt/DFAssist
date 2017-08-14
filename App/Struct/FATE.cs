namespace App
{
    public class FATE
    {
        public string Name { get; set; }

        public static explicit operator FATE(string name)
        {
            return new FATE { Name = name };
        }
    }
}
