using AARS.Debugging;

namespace AARS;

public class AARSConsts
{
    public const string LocalizationSourceName = "AARS";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "41acae1f2cdf4bca89cfa15d17a56939";
}
