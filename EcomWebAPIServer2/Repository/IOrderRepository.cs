using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Repository
{
    public interface IOrderRepository
    {
        List<Order> Order { get;//read all the data and display 
}

        Order GetOrder(int id);//read single customer details
        int AddOrder(Order order);//insert
        int UpdateOrder(int id, Order order);//update
        int DeleteOrder(int id);//delete
        List<Order> GetOrder();
    }
}
