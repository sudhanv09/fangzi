using api.Models.DTO;

namespace api.Services;

public class MetaBuilder
{
    public List<string> BuildMeta(ListingDto dto)
    {
        List<string> metadata = [];

        if (dto.Meta is not null) metadata.AddRange(dto.Meta);
        if (dto.Utilities is not null) metadata.AddRange(dto.Utilities);
        
        metadata.AddRange([dto.City, dto.Cost, dto.PostalCode.ToString()]);
        
        return metadata;
    }

    private List<string> GetKeywords(string title)
    {
        return [];
    }
}