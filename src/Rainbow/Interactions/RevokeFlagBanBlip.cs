namespace Rainbow.Interactions;

public class RevokeFlagBanBlip : Blip
{
    public RevokeFlagBanBlip(ulong targetUserId)
    {
        Type = BlipType.RevokeFlagAndBan;
        TargetUserId = targetUserId;
    }
}