using RazorPageShop.Core.DTOs;
using RazorPageShop.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RazorPageShop.Core
{
    public class CheckoutManager : ICheckoutManager
    {
        private readonly Context _context;
        private string serverFilePath;

        public CheckoutManager(Context context)
        {
            this._context = context;
        }

        public CheckoutManager(string serverFilePath)
        {
            this.serverFilePath = serverFilePath;
        }

        public CheckoutSummaryDTO GetCheckoutSummary()
        {
            var cartItems = GetCartItems();
            var subtotal = cartItems.Sum(i => i.Subtotal);
            var discountRule = DiscountManager.Instance.GetDiscount(subtotal);
            var discountValue = discountRule.CalculatedDiscount;
            var total = subtotal - discountValue;

            return new CheckoutSummaryDTO
            {
                OrderNumber = "123456789",
                DeliveryUpToNWorkingDays = 4,
                Total = total,
                CustomerInfo = GetDummyCustomerInfo(),
                CartItems = cartItems
            };
        }

        public CartDTO GetCart()
        {
            return GetCart(GetCartItems());
        }

        public CartDTO GetCart(List<CartItemDTO> cartItems)
        {
            var subtotal = cartItems.Sum(i => i.Subtotal);
            var discountRule = DiscountManager.Instance.GetDiscount(subtotal);
            var discountValue = discountRule.CalculatedDiscount;
            var total = subtotal - discountValue;

            return new CartDTO
            {
                Subtotal = subtotal,
                DiscountRate = discountRule.Rate * 100M,
                DiscountValue = discountValue,
                Total = total,
                CartItems = cartItems
            };
        }

        public void SaveCart(CartItemDTO newOrEditItem)
        {
            if (newOrEditItem.Quantity < 0)
                newOrEditItem.Quantity = 0;

            var product = _context.Product.Where(p => p.SKU == newOrEditItem.SKU).Single();

            var cartItem =
                (from ci in _context.CartItem
                 join p in _context.Product on ci.Product.Id equals p.Id
                 where p.SKU == newOrEditItem.SKU
                 select ci)
                .SingleOrDefault();

            if (cartItem != null)
            {
                if (newOrEditItem.Quantity == 0)
                    _context.CartItem.Remove(cartItem);
                else
                {
                    cartItem.Quantity = newOrEditItem.Quantity;
                    cartItem.Product = product;
                }
            }
            else
            {
                _context.CartItem.Add(new CartItem
                {
                    Product = product,
                    Quantity = newOrEditItem.Quantity
                });
            }

            _context.SaveChanges();
        }

        public List<ProductDTO> GetProducts()
        {
            return _context.Product
                .Select(i =>
                new ProductDTO
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    SmallImagePath = i.SmallImagePath,
                    Description = i.Description,
                    Price = i.Price
                }).ToList();
        }

        private List<CartItemDTO> GetCartItems()
        {
            return
                (from ci in _context.CartItem
                 from p in _context.Product.Where(p => p.Id == ci.Product.Id)
                 select new { ci, p })
                .Select(i =>
                new CartItemDTO
                {
                    Id = i.ci.Id,
                    SKU = i.p.SKU,
                    SmallImagePath = i.p.SmallImagePath,
                    Description = i.p.Description,
                    Price = i.p.Price,
                    Quantity = i.ci.Quantity
                }).ToList();
        }

        private CustomerInfoDTO GetDummyCustomerInfo()
        {
            return new CustomerInfoDTO
            {
                CustomerName = "John Doe",
                PhoneNumber = "(11) 555-12345",
                Email = "johndoe@email.com",
                DeliveryAddress = "503-250 Ferrand Drive - Toronto Ontario, M3C 3G8 Canada"
            };
        }

        public void InitializeDB()
        {
            _context.Database.EnsureCreated();
            if (this._context.Product.Count() == 0)
            {
                var products = new string[]
                {
                    "10 Million Member CodeProject T-Shirt|3399",
                    "Women's T-Shirt|3399",
                    "CodeProject.com Body Suit|1399",
                    "CodeProject Mug Mugs|1099",
                    "RootAdmin Mug|1099",
                    "Drinking Glass|1099",
                    "Stein|1399",
                    "Mousepad|1099",
                    "Square Sticker|299"
                };

                var index = 1;
                foreach (var p in products)
                {
                    var description = p.Split('|')[0];
                    var price = decimal.Parse(p.Split('|')[1]) / 100M;

                    var product =
                    _context.Product.Add(new Product
                    {
                        SKU = Guid.NewGuid().ToString(),
                        SmallImagePath = string.Format("Images/Products/small_{0}.jpg", index),
                        LargeImagePath = string.Format("Images/Products/large_{0}.jpg", index),
                        Description = description,
                        Price = price
                    });

                    var cartItem =
                    _context.CartItem.Add(new CartItem
                    {
                        Product = product.Entity,
                        Quantity = 1
                    });

                    index++;

                    _context.SaveChanges();
                }
            }
        }
    }
}
