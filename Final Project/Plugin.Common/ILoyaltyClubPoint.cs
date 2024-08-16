using FinalProject.Common;

namespace Plugin.Common;

public interface ILoyaltyClubPoint
{
    public Dictionary<int,int> CalculateLoyaltyClubPoints(IDbContext dbContext);
}