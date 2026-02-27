using System.Net;
using Transcard.Application.DTOs;

namespace Transcard.WebApp.Services
{
    public class PaymentClient : IPaymentClient
    {
        private readonly HttpClient _httpClient;

        public PaymentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaymentResponseDto> SubmitAsync(PaymentRequestDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/payments", request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new Exception("Forbidden");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Network error");

            return await response.Content.ReadFromJsonAsync<PaymentResponseDto>()
                   ?? throw new Exception("Invalid response");
        }
    }
}