using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using tinyCard.Core.Model;
using tinyCard.Core.Services;
using Xunit;

namespace tinyCard.Core.Tests
{
    public class CardTests : IClassFixture<TinyCardFixture>
    {
        private ICardService _card;
        private Data.CardDbContext _dbContext;

        public CardTests(TinyCardFixture fixture)
        {
            _card = fixture.Scope.ServiceProvider
                .GetRequiredService<ICardService>();
            _dbContext = fixture.Scope.ServiceProvider
                .GetRequiredService<Data.CardDbContext>();
        }

        [Fact]
        public async Task Add_CardAsync()
        {
            var options = new Services.Options.IssueNewCardOptions()
            {
                CardNumber = "1358789645366985",
                CardPresentBalance = 0M,
                EcommerceBalance = 0M
            };

            var card = await _card.IssueNewCardAsync(options);

            Assert.NotNull(card);

            var savedCard = await _dbContext.Set<Card>()
                .Where(c => c.CardNumber == card.CardNumber)
                .SingleOrDefaultAsync();

            Assert.NotNull(savedCard);
            Assert.Equal(options.CardNumber, savedCard.CardNumber);
            Assert.Equal(options.CardPresentBalance, savedCard.CardPresentBalance);
            Assert.Equal(options.EcommerceBalance, savedCard.EcommerceBalance);
        }
    }
}
