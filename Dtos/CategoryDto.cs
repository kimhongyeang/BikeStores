namespace DotNetCore.Dtos
{
    public class CategoryDto
    {
        public string Name { get; set; }
    }

    public class GetCategoryDto : CategoryDto
    {
        public long Id { get; set; }
    }

    public class AddCategoryDto : CategoryDto { }
}
