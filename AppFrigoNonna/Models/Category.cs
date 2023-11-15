namespace AppFrigoNonna.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FridgeProd> FridgeProds { get; set; }

        public Category() { }
    }
}
