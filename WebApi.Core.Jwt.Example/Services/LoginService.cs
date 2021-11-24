using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Core.Jwt.Example.Repository;
using WebApi.Core.Jwt.Example.Repository.Entities;
using WebApi.Core.Jwt.Example.Util;

namespace WebApi.Core.Jwt.Example.Services
{
    public interface ILoginService
    {
        User SingIn(string username, string password);
        User SingUp(User model);
        List<Role> GetUserRole(int idUser);
    }

    public class LoginService : ILoginService
    {
        private readonly IRepository<User> _userRepository;
        private readonly Context _dbContext;

        public LoginService(Context dbContext, IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        public User SingIn(string username, string password)
        {
            string pwd = PasswordHash.GetPasswordHash(password);

            return (_dbContext.User.Where(x => x.Login == username && x.Pwd == pwd).FirstOrDefault());
        }

        public User SingUp(User model)
        {            
            model.Pwd = PasswordHash.GetPasswordHash(model.Pwd);
            _userRepository.Add(model);
            return (model);
        }

        public List<Role> GetUserRole(int idUser)
        {            
            return (from ur in _dbContext.UserRole
                     join r in _dbContext.Role on ur.IdRole equals r.IdRole
                     where ur.IdUser == idUser
                     orderby r.Name
                     select r).ToList();
        }        

    }
}