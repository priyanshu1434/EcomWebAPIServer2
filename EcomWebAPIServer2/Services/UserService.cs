﻿using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Repository;


namespace EcomWebAPIServer2.Services

{

    public class UserService : IUserService

    {

        private readonly IUserRepository repo;

        public UserService(IUserRepository repo)

        {

            this.repo = repo;

        }

        public int AddUser(User user)

        {

            if (repo.GetUser(user.UserId) != null)

            {

                throw new UserAlreadyExistsException($"User with user id {user.UserId} already exists");

            }

            return repo.AddUser(user);

        }

        public int DeleteUser(int id)

        {

            if (repo.GetUser(id) == null)

            {

                throw new UserNotFoundException($"User with user id {id} does not exist");

            }

            return repo.DeleteUser(id);

        }

        public User GetUser(int id)

        {

            User user = repo.GetUser(id);

            if (user == null)

            {

                throw new UserNotFoundException($"User with user id {id} does not exist");

            }

            return user;

        }

        public List<User> GetUsers()

        {

            return repo.GetUsers();

        }

        public int UpdateUser(int id, User user)

        {

            if (repo.GetUser(id) == null)

            {

                throw new UserNotFoundException($"User with user id {id} does not exist");

            }

            return repo.UpdateUser(id, user);

        }

    }

}

