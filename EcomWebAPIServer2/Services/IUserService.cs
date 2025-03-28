﻿using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Services
{
    public interface IUserService
    {
        List<User> GetUsers();//read all the data and display 
        User GetUser(int id);//read single customer details
        int AddUser(User user);//insert
        int UpdateUser(int id, User user);//update
        int DeleteUser(int id);//delete
    }
}
