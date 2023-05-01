﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using VideoShareData.DTOs;
using VideoShareData.Models;

namespace VideoShareApp.Authentication
{
    internal class UserAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public UserAuthenticationProvider(ProtectedSessionStorage sessionStorage) {
            _sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
                var userStorage = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userStorage is null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                else
                {
                    var claimsList = new List<Claim>{
                        new Claim(ClaimTypes.NameIdentifier, userStorage.Id.ToString()),
                        new Claim(ClaimTypes.Name, userStorage.FullName),
                        new Claim(ClaimTypes.Role, userStorage.Role.ToString()),
                        new Claim("http://VideoShare/claims/ProfilePicturePath", userStorage.PfpFilepath ?? "")
                    };
                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claimsList));
                    return await Task.FromResult(new AuthenticationState(claimsPrincipal));
                }
            }
            catch
            {
                //You are here if the user modified the encrypted data in the Session Storage
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        public async Task Login(User user) {
            var userSession = new UserSession
            {
                FullName = user.FullName,
                Id = user.UserId,
                Role = user.UserType
            };
            //TO DO: Get filepath of server cached profile picture
            await _sessionStorage.SetAsync("UserSession", userSession);
            var claimsList = new List<Claim>{
                        new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString()),
                        new Claim(ClaimTypes.Name, userSession.FullName),
                        new Claim(ClaimTypes.Role, userSession.Role.ToString()),
                        new Claim("http://VideoShare/claims/ProfilePicturePath", userSession.PfpFilepath ?? "")
                    };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claimsList, "CustomVideoShareAuthentication"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task Logout() {
            //TODO: un-cache profile picture
            await _sessionStorage.DeleteAsync("UserSession");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}
