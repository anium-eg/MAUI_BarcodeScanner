using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MAUI_BarcodeScanner.Models;

namespace MAUI_BarcodeScanner.Helpers.Validators
{
    class CartItemPriceValidator:AbstractValidator<CartItem>
    {
        public CartItemPriceValidator()
        {
            RuleFor(cartItem => cartItem.PricePerItem)
                .NotEmpty().WithMessage("Price per item is required.")
                .LessThanOrEqualTo(cartItem => cartItem.MRP).WithMessage("Price cannot be more than MRP.");
        }
    }
}
