using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transcard.Application.DTOs;
using Transcard.Application.Interfaces;

namespace Transcard.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> Submit(PaymentRequestDto request)
    {
        var result = await _paymentService.SubmitAsync(request);

        return Ok(result);
    }
}