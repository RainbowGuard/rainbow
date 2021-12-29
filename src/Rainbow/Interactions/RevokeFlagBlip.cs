namespace Rainbow.Interactions;

public class RevokeFlagBlip : Blip
{
    public RevokeFlagBlip(ulong targetUserId)
    {
        Type = BlipType.RevokeFlag;
        TargetUserId = targetUserId;
    }
}