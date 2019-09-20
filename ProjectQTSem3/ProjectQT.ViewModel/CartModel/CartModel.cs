using ProjectQT.DataModel.Models;

namespace ProjectQT.ViewModel.CartModel
{
    public class CartModel
    {
        public Product Products { get; set; }
        public string Attrs { get; set; }
        public int Quantity { get; set; }
    }
}
