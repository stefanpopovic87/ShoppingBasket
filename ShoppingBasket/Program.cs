using ShoppingBasket.Data;

namespace ShoppingBasket
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext dataContext = new DataContext();
            var app = new ShoppingBasketApp(dataContext);
            app.Execute();           
        }        
    }
}
    


