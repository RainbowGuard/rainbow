namespace Rainbow.Interactions;

public class BlipResult
{
    public bool Success { get; init; }

    public string ErrorMessage { get; init; }

    public static readonly BlipResult CompletedBlip = new() { Success = true };
}