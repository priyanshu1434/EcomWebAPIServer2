﻿using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Repository;

namespace EcomWebAPIServer2.Services
{ 
        public class OrderService : IOrderService
        {
            private readonly IOrderRepository repo;

            public OrderService(IOrderRepository repo)
            {
                this.repo = repo;
            }
            public int AddOrder(Order order)
            {
                if (repo.GetOrder(order.OrderId) != null)
                {
                    throw new OrderAlreadyExistsException($"Order with order id {order.OrderId} already exists");
                }
                return repo.AddOrder(order);
            }

            public int DeleteOrder(int id)
            {
                if (repo.GetOrder(id) == null)
                {

                    throw new OrderNotFoundException($"Order with Order id {id} does not exists");
                }
                return repo.DeleteOrder(id);
            }

            public Order GetOrder(int id)
            {
                Order c = repo.GetOrder(id);
                if (c == null)
                {
                    throw new OrderNotFoundException($"Order with order id {id} does not exists");
                }
                return c;
            }

            public List<Order> GetOrder()
            {
                return repo.Order;
            }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public int UpdateOrder(int id, Order order)
            {
                if (repo.GetOrder(id) == null)
                {
                    throw new OrderNotFoundException($"Order with Order id {id} does not exists");
                }
                return repo.UpdateOrder(id, order);
            }

            Order IOrderService.GetOrder(int id)
            {
                throw new NotImplementedException();
            }

            Order IOrderService.GetOrder(int id)
            {
                throw new NotImplementedException();
            }
        }
    }

