﻿using Source.net.infrastructure.Dtos;
using Source.net.infrastructure.Entities;
using Source.net.infrastructure.Exceptions;
using Source.net.infrastructure.SearchFilters;
using Source.net.infrastructure.Views;
using Source.net.services.Mappers;
using Source.net.services.Repositories.Interfaces;
using Source.net.services.Services.Interfaces;
using System.Collections.Generic;

namespace Source.net.services.Services.Implementations
{
    public class UserServiceImp : 
        BaseServiceImp<
            User, 
            UserView, 
            UserMapper, 
            UserRepository, 
            RegisterDto, 
            UpdateUserDto, 
            UserFilters
        >,
        UserService
    {
        private readonly PasswordCryptoService _crytpoService;


        public UserServiceImp(UserMapper userMapper, UserRepository userRepository, PasswordCryptoService crytpoService) : 
            base(userMapper, userRepository)
        {
            _crytpoService = crytpoService;
        }

        public UserView GetByUsername(string username)
        {
            var user = _repo.GetUserByUsername(username);
            return _mapper.From(user);
        }

        public override UserView Add(RegisterDto dto)
        {
            if (_repo.GetByEmail(dto.Email) != null)
            {
                throw new BadRequestException("Email already in use.");
            }
            if (_repo.GetUserByUsername(dto.Username) != null)
            {
                throw new BadRequestException("Username already in use.");
            }
            return base.Add(dto);
        }

        public override UserView Update(int userId, UpdateUserDto dto)
        {
            var user = _repo.Get(userId);
            if (user.Email != dto.Email
                && _repo.GetByEmail(dto.Email) != null)
            {
                throw new BadRequestException("Email already in use.");
            }
            if (user.Username != dto.Username
                && _repo.GetUserByUsername(dto.Username) != null)
            {
                throw new BadRequestException("Username already in use.");
            }
            return base.Update(userId, dto);
        }

        public UserView UpdatePassword(int userId, UpdatePasswordDto dto)
        {
            if (dto.PassowrdConfirmation != dto.Password)
            {
                throw new BadRequestException("Passwords do not match.");
            }
            var user = _repo.Get(userId);
            string salt = _crytpoService.GenerateSalt();
            string hash = _crytpoService.GenerateHash(salt, dto.Password);
            user.PasswordSalt = salt;
            user.PasswordHash = hash;
            _repo.Update(user);
            return _mapper.From(user);
        }

        public UserView UpdateRole(int userId, UpdateRoleDto dto)
        {
            var user = _repo.Get(userId);
            user.Role = dto.Role;
            _repo.Update(user);
            return _mapper.From(user);
        }

        public UserView DeleteUser(int userId)
        {
            var user = _repo.Delete(userId);
            return _mapper.From(user);
        }

        public UserView ActivateUser(int userId)
        {
            var user = _repo.ActivateUser(userId);
            return _mapper.From(user);
        }

        public bool hasPermissions(int id)
        {
            var user = Get(id);

            if (user.isAdmin())
                return true;
            
            return id == user.id && user.Active;
        }

        public UserView UpdatePackage(int userId)
        {
            var user = Get(userId);
            if(user is null)
            {
                throw new System.Exception("Unable to find user.");
            }

            return _mapper.From(_repo.UpdatePackage(userId));
        }
    }
}
