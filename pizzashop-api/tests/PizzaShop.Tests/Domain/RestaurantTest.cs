using Bogus;
using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Domain.Enums;
using PizzaShop.API.Domain.Exceptions;

namespace PizzaShop.Tests.Domain
{
	public class RestaurantTest
	{
		[Fact(DisplayName = "[Domain.Restaurant] - Não deve permitir alterar um manager de um Restaurant para um usuário 'Customer'")]
		public void Restaurant_NaoDevePermitirAlterarRestaurantParaUserDoTipoCustomer()
		{
			var faker = new Faker("pt_BR");
			var user = new User(
				 faker.Person.FullName,
				 faker.Person.Email,
				  faker.Phone.PhoneNumber(),
				   RoleUser.Customer);

			var restaurant = new Restaurant(
				 faker.Company.CompanyName(),
				   faker.Company.CatchPhrase(),
				   faker.Person.FullName,
					 faker.Person.Email,
					  faker.Phone.PhoneNumber());

			var exception = Assert.Throws<DomainException>(() => restaurant.ChangeManager(user));

			Assert.NotNull(exception);
			Assert.IsType<DomainException>(exception);
			Assert.Equal("User role type not allowed.", exception.Message);
		}
	}
}