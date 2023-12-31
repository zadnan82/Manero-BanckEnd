﻿using Manero_BanckEnd.Entities;

namespace Manero_BanckEnd.Schemas.PromoCodes;

public class PromoCodeSchema
{
    public string PromoName { get; set; } = null!;

    public double SalePercentage { get; set; }

    public DateTime Validity { get; set; }


    public static implicit operator PromoCodeEntity(PromoCodeSchema promoCodeSchema)
    {
        if (promoCodeSchema != null)
            return new PromoCodeEntity
            {

                PromoName = promoCodeSchema.PromoName,
                SalePercentage = promoCodeSchema.SalePercentage,
                Validity = promoCodeSchema.Validity,
            };

        return null!;
    }
}
