using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tinyCard.Core.Services;
using tinyCard.Core.Services.Options;

namespace tinyCard.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : Controller
    {
        private readonly ICardService _card;
        private readonly ILogger<CardController> _logger;

        public CardController(
            ILogger<CardController> logger,
            ICardService card)
        {
            _logger = logger;
            _card = card;
        }

        [HttpGet("{CardNumber}")]
        public async Task<IActionResult> GetCard(string cardNumber)
        {
            RetrieveCardOptions options = new RetrieveCardOptions
            {
                CardNumber = cardNumber
            };

            var card = await _card.RetrieveCardAsync(options);

            return Json(card);
        }

        [HttpPost]
        public async Task<IActionResult> Authorize(
            [FromBody] AuthorizeOptions options)
        {
            var card = await _card.AuthorizeAsync(options);

            return Json(card);
        }
    }
}