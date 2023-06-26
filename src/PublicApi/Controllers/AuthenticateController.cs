using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.PublicApi.AuthEndpoints;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microsoft.eShopWeb.PublicApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenClaimsService _tokenClaimsService;

    public AuthenticateController(SignInManager<ApplicationUser> signInManager, ITokenClaimsService tokenClaimsService)
    {
        _signInManager = signInManager;
        _tokenClaimsService = tokenClaimsService;
    }


    // POST api/<AuthenticateController>
    [HttpPost]
    public async Task<ActionResult<AuthenticateResponse>> Post([FromBody] AuthenticateRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = new AuthenticateResponse(request.CorrelationId());

        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);

        response.Result = result.Succeeded;
        response.IsLockedOut = result.IsLockedOut;
        response.IsNotAllowed = result.IsNotAllowed;
        response.RequiresTwoFactor = result.RequiresTwoFactor;
        response.Username = request.Username;

        if (result.Succeeded)
        {
            response.Token = await _tokenClaimsService.GetTokenAsync(request.Username);
        }

        return response;
    }

}
