﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using OneRing.Utils;

namespace OneRing.Controllers
{
	public class AccountController : Controller
	{
		public void SignIn() {
			// Send an OpenID Connect sign-in request.
			if (!Request.IsAuthenticated) {
				HttpContext.GetOwinContext()
					.Authentication.Challenge(new AuthenticationProperties { RedirectUri = "/" },
						OpenIdConnectAuthenticationDefaults.AuthenticationType);
			}
		}

		public void SignOut() {
			// Remove all cache entries for this user and send an OpenID Connect sign-out request.
			string userObjectID =
				ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
			AuthenticationContext authContext = new AuthenticationContext(Startup.Authority,
				new NaiveSessionCache(userObjectID));
			authContext.TokenCache.Clear();
			AuthenticationHelper.token = null;
			HttpContext.GetOwinContext().Authentication.SignOut(
				OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
		}
	}
}