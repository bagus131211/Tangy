namespace Tangy.Models.LearnBlazorModels
{
    public class DemoProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DemoProductProperties> ProductProperties { get; set; }

    }
}
