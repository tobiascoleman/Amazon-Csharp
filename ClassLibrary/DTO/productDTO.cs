using Library.eCommerce.Models;
namespace Library.eCommerce.DTO
{
	public class ProductDTO
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public decimal Price { get; set; }

		public string? Display
		{
			get
			{
				return $"{Id}. {Name} - {Price:C}";
			}
		}

		public ProductDTO()
		{
			Name = string.Empty;
			Price = 0m;
		}

		public ProductDTO(Product p)
		{
			Name = p.Name;
			Id = p.Id;
			Price = 0m;
		}

		public ProductDTO(ProductDTO p)
		{
			Name = p.Name;
			Id = p.Id;
			Price = p.Price;
		}

		public override string ToString()
		{
			return Display ?? string.Empty;
		}
	}
}
