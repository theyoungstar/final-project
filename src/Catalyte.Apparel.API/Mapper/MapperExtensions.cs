using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.DTOs.Purchases;

namespace Catalyte.Apparel.API.DTOMappings
{
    public static class MapperExtensions
    {

        public static IEnumerable<PurchaseDTO> MapPurchasesToPurchaseDtos(this IMapper mapper, IEnumerable<Purchase> purchases)
        {
            return purchases
                .Select(x => mapper.MapPurchaseToPurchaseDto(x))
                .ToList();
        }

        /// <summary>
        /// Helper method to build model for a purchase DTO.
        /// </summary>
        /// <param name="purchase">The purchase to be persisted.</param>
        /// <returns>A purchase DTO.</returns>
        public static PurchaseDTO MapPurchaseToPurchaseDto(this IMapper mapper, Purchase purchase)
        {
            return new PurchaseDTO()
            {
                Id = purchase.Id,
                OrderDate = purchase.OrderDate,
                LineItems = mapper.Map<List<LineItemDTO>>(purchase.LineItems),
                DeliveryAddress = mapper.Map<DeliveryAddressDTO>(purchase),
                BillingAddress = mapper.Map<BillingAddressDTO>(purchase),
                CreditCard = mapper.Map<CreditCardDTO>(purchase)
            };
        }

        public static Purchase MapCreatePurchaseDtoToPurchase(this IMapper mapper, CreatePurchaseDTO purchaseDTO)
        {
            var purchase = new Purchase
            {
                OrderDate = DateTime.UtcNow,
            };
            purchase = mapper.Map(purchaseDTO.DeliveryAddress, purchase);
            purchase = mapper.Map(purchaseDTO.BillingAddress, purchase);
            purchase = mapper.Map(purchaseDTO.CreditCard, purchase);
            purchase.LineItems = mapper.Map(purchaseDTO.LineItems, purchase.LineItems);

            return purchase;
        }
    }
}
