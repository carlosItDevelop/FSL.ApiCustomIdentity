﻿using FSL.ApiCustomIdentity.Models;
using FSL.ApiCustomIdentity.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSL.ApiCustomIdentity.Controllers
{
    [Route("api/login")]
    [ApiController]
    public sealed class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Service.IAuthorizationService _authorizationService;

        public LoginController(
            IAuthenticationService authenticationService,
            Service.IAuthorizationService authorizationService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(
            [FromBody] LoginUser loginUser)
        {
            var authorization = _authorizationService.Authorize(loginUser);
            if (!authorization.Success)
            {
                return Ok(authorization);
            }

            var authentication = _authenticationService.Authenticate(authorization.Data);
            if (!authentication.Success)
            {
                return Ok(authentication);
            }

            return Ok(authentication);
        }
    }
}
