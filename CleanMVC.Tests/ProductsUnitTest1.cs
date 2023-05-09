using CleanMVC.Domain.Entities;
using CleanMVC.Domain.Validation;
using FluentAssertions;

namespace CleanMVC.Tests
{
    public class ProductsUnitTest1
    {
        [Fact]
        public void CreateProductWithValidParametersResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProductNegativeIdValueDomainExceptionInvalidId()
        {
            Action action = () => new Product(- 1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid id value.");
        }

        [Fact]
        public void CreateProductShortNameValueDomainExceptionShortName()
        {
            Action action = () => new Product(1, "Ca", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characteres.");
        }

        [Fact]
        public void CreateProductInvalidNameValueDomainExceptionRequiredName()
        {
            Action action = () => new Product(1, "", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is requird.");
        }

        [Fact]
        public void CreateProductInvalidDescriptionValueDomainExceptionRequiredDescription()
        {
            Action action = () => new Product(1, "Product Name", "", 9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid description. Description is requird");
        }

        [Fact]
        public void CreateProductInvalidImageValueDomainExceptionTooLongImage()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, 
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, maximum 250 characteres.");
        }

        [Fact]
        public void CreateProductWithEmptyImageNoDomainException()
        { 
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should()
                .NotThrow<DomainExceptionValidation>();        
        }

        [Fact]
        public void CreateProductWithNullImageNoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProductWithNullImageNoNullReferenceException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact]
        public void CreateProductInvalidPriceValueDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid price value.");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProductInvalidStockValueDomainException(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid stock value.");
        }
    }
}
