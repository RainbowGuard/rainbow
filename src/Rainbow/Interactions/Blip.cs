using System;
using System.Text.RegularExpressions;

namespace Rainbow.Interactions;

/// <summary>
/// A serializable object used to represent message interaction data.
/// </summary>
public class Blip
{
    /// <summary>
    /// The blip type.
    /// </summary>
    protected BlipType Type { get; init; }

    /// <summary>
    /// A target user ID. If the blip type does not support this property, it will be left at 0.
    /// </summary>
    public ulong TargetUserId { get; protected init; }

    private static readonly Regex Format = new(@"(?<Type>[^-]*)-(?<TargetUserId>\d*)-blip", RegexOptions.Compiled);

    protected Blip() { }

    /// <summary>
    /// Formats a <see cref="Blip"/> into a string that it can be restored from later.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Type}-{TargetUserId}-blip";
    }

    /// <summary>
    /// Parses a <see cref="Blip"/> from a blip string.
    /// </summary>
    /// <param name="blipString">The blip string.</param>
    /// <param name="blip">The blip that was read from the blip string, or null if parsing failed.</param>
    /// <returns>Whether or not parsing succeeded.</returns>
    public static bool TryParse(string blipString, out Blip blip)
    {
        blip = null;

        if (string.IsNullOrEmpty(blipString))
        {
            return false;
        }

        try
        {
            var matches = Format.Match(blipString);
            if (!matches.Success)
            {
                return false;
            }

            if (!ulong.TryParse(matches.Groups["TargetUserId"].Value, out var targetUserId))
            {
                return false;
            }

            var type = Enum.Parse<BlipType>(matches.Groups["Type"].Value);
            blip = type switch
            {
                BlipType.RevokeFlagAndBan => new RevokeFlagBanBlip(targetUserId),
                _ => null,
            };

            if (blip == null)
            {
                return false;
            }
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static implicit operator string(Blip blip) => blip.ToString();
}