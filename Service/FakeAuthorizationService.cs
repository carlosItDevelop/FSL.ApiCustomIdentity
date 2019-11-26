﻿using System;
using FSL.ApiCustomIdentity.Models;

namespace FSL.ApiCustomIdentity.Service
{
    public sealed class FakeAuthorizationService : IAuthorizationService
    {
        public BaseResult<IUser> Authorize(
            LoginUser loginUser)
        {
            var loginOrEmail = loginUser?.LoginOrEmail ?? "";
            var password = loginUser?.Password ?? "";

            var result = new BaseResult<IUser>();

            if (loginOrEmail == "fsl" && password == "1234")
            {
                result.Success = true;
                result.Message = "User authorized!";
                result.Data = new MyLoggedUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Name test",
                    Credentials = "01|02|09",
                    IsAdmin = false
                };
            }
            else
            {
                result.Success = false;
                result.Message = "Not authorized!";
            }

            return result;
        }
    }
}
