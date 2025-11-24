using System.Reflection;

namespace ProductLibrary
{
    public class Product
    {
        private int id;
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string PrintInfo()
        {
            return $"Name={name}, Id={id}";
        }

    }
}
