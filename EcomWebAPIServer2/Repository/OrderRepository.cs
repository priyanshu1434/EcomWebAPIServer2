﻿using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using Microsoft.AspNetCore.Authentication;
namespace EcomWebAPIServer2.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcomContext db;

        private readonly CartItemService _cartItemRepository;

        public OrderRepository(EcomContext db)
        {
            this.db = db;
        }

        static int UniqueNumberGenerate()
        {
            int timestamp = (int)(DateTime.UtcNow.Ticks % 1000000000);
            int randomPart = new Random().Next(1000, 9999);
            return timestamp + randomPart;
        }
        public int AddOrder(Order order)
        {
            order.OrderId = UniqueNumberGenerate();

            var cartItems = (from cartItem in db.CartItems
                             join product in db.Products on cartItem.ProductId equals product.ProductId
                             where cartItem.UserId == order.UserId
                             select new
                             {
                                 CartItemId = cartItem.CartItemId,
                                 UserId = cartItem.UserId,
                                 ProductId = cartItem.ProductId,
                                 Quantity = cartItem.Quantity,
                                 ProductName = product.ProductName,
                                 ProductPrice = product.ProductPrice,
                                 ProductDes = product.ProductDescription,
                                 ProductCate = product.ProductCategory
                             }).ToList();

            foreach (var cartItem in cartItems) 
            {
                order.TotalPrice += cartItem.ProductPrice * cartItem.Quantity;
            }


            db.Orders.Add(order);
            return db.SaveChanges();
        }


        public int DeleteOrder(int id)
        {
            Order c = db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            db.Orders.Remove(c);
            return db.SaveChanges();
        }


        public Order GetOrder(int id)
        {
            return db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
        }



        public List<Order> GetOrders()
        {
            return db.Orders.ToList();

        }

        public int UpdateOrder(int id, Order order)
        {
            Order c = db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            c.TotalPrice = order.TotalPrice;
            c.ShippingAddress = order.ShippingAddress;
            c.OrderStatus = order.OrderStatus;
            c.PaymentStatus = order.PaymentStatus;
            db.Entry<Order>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }

        public object GetOrdersByUserId(int userId)
        {

            var orders = (from order in db.Orders
                             join product in db.Products on order.ProductId equals product.ProductId
                             join user in db.Users on order.UserId equals user.UserId
                             where order.UserId == userId
                             select new
                             {
                                 OrderId = order.OrderId,
                                 UserId = order.UserId,
                                 ProductId = order.ProductId,
                                 TotalPrice = order.TotalPrice,
                                 ShippingAddress = order.ShippingAddress, 
                                 OrderStatus = order.OrderStatus,
                                 PaymentStatus = order.PaymentStatus,
                                 DateTime = order.OrderDateTime,
                                 ProductName = product.ProductName,
                                 ProductPrice = product.ProductPrice,
                                 ProductDes = product.ProductDescription,
                                 ProductCate = product.ProductCategory,
                                 UserName = user.Name,
                                 email =  user.Email,
                                 phone = user.PhoneNumber
                              

                             }).ToList();

            return orders;

        }
    }
}

