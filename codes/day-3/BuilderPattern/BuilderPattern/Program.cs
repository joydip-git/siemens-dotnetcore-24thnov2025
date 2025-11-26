namespace BuilderPattern
{
    class Garden
    {

    }
    class SwimmingPool
    {

    }
    class Door
    {

    }
    class Window
    {

    }
    class House
    {
        public List<Door> Doors { get; set; } = [];
        public List<Window> Windows { get; set; } = [];

        public Garden? Garden { get; set; }
        public SwimmingPool? SwimmingPool { get; set; }
    }
    class HouseBuilder
    {
        private House house;
        public HouseBuilder()
        {
            house = new House { Doors = [new Door(), new Door()], Windows = [new Window(), new Window()] };
        }
        public HouseBuilder Add<T>(T? instance) where T : class, new()
        {
            var properties = house.GetType().GetProperties().ToList();
            var foundProp = properties.Find(
                p => p.PropertyType == typeof(T));
            if (foundProp != null)
            {
                if (instance == null)
                    foundProp.SetValue(house, new T());
                else
                    foundProp.SetValue(house, instance);
            }
            return this;
        }
        public HouseBuilder AddGarden(Garden? garden = null)
        {
            if (garden == null)
                house.Garden = new Garden();
            else
                house.Garden = garden;

            return this;
        }
        public HouseBuilder AddSwimmingPool(SwimmingPool? pool = null)
        {
            if (pool == null)
                house.SwimmingPool = new SwimmingPool();
            else
                house.SwimmingPool = pool;

            return this;
        }
        public House BuildHouse() => house;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //IHostBuilder builder = Host.CreateApplicationBuilder();
            HouseBuilder builder = new HouseBuilder();
            House house = builder.BuildHouse();

            builder = builder.AddGarden(new Garden());
            builder = builder.AddSwimmingPool();

            house = builder.BuildHouse();
            
        }
    }
}
