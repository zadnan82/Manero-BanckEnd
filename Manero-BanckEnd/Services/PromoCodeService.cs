using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas.PromoCodes;
using System.Diagnostics;

namespace Manero_BanckEnd.Services;

public class PromoCodeService
{
    private readonly PromoCodeRepo _promoCodeRepo;

    public PromoCodeService(PromoCodeRepo promoCodeRepo)
    {
        _promoCodeRepo = promoCodeRepo;
    }

    public async Task<PromoCodeEntity> CreateAsync(PromoCodeSchema schema)
    {
        try
        {
            if (schema != null)
            {
                var promoCodeEntity = new PromoCodeEntity
                {
                    PromoName = schema.PromoName,
                    SalePercentage = schema.SalePercentage,
                    Validity = schema.Validity,
                };
                return await _promoCodeRepo.AddAsync(promoCodeEntity);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public async Task<IEnumerable<PromoCodeEntity>> GetAllAsync()
    {
        try
        {
            return await _promoCodeRepo.GetAllAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex.Message}");
        }
        return null!;
    }

    public async Task<PromoCodeEntity> DeleteAsync(int id)
    {
        try
        {
            return await _promoCodeRepo.DeleteAsync(id);

        }
        catch (Exception Ex) { Debug.WriteLine(Ex.Message); }


        return null!;
    }
}
