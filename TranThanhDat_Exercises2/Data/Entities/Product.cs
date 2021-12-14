namespace TranThanhDat_Exercises2.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Quannity { get; set;}
        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
