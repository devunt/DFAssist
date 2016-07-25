namespace App
{
    public class Instance
    {
        public string Name { get; }
        public byte Tank { get; }
        public byte DPS { get; }
        public byte Healer { get; }

        public Instance(string name, byte tank, byte healer, byte dps)
        {
            Name = name;
            Tank = tank;
            DPS = dps;
            Healer = healer;
        }
    }
}
