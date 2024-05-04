using Bogus;
using PizzaShop.API.Domain;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Exceptions;

namespace PizzaShop.Tests.Domain
{
	public class RestaurantTest
	{
		[Fact(DisplayName = "[Domain.Restaurant] - Não deve permitir criar um Restaurant para um usuário 'Customer'")]
		public void Restaurant_NaoDeveCriarRestaurantComUserDoTipoCustomer()
		{
			var faker = new Faker("pt_BR");
			var user = new User(faker.Person.FullName, faker.Person.Email, faker.Phone.PhoneNumber(), RoleUser.Customer);

			var exception = Assert.Throws<DomainException>(()
				=> new Restaurant("Test", "Restaurant test", user));

			Assert.NotNull(exception);
			Assert.IsType<DomainException>(exception);
			Assert.Equal("User role type not allowed.", exception.Message);
		}
	}
}