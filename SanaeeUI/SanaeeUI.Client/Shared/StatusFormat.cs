using MudBlazor;
using SanaeeUI.Client.Models;

namespace SanaeeUI.Client.Shared;

// Presentation helpers shared by all Sanaee pages: status -> color/icon and
// enum -> human readable text (e.g. OfferAccepted -> "Offer Accepted").
public static class StatusFormat
{
    public static string Spaced(this Enum value) => SplitCamel(value.ToString());

    private static string SplitCamel(string input)
    {
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            if (i > 0 && char.IsUpper(input[i])) sb.Append(' ');
            sb.Append(input[i]);
        }
        return sb.ToString();
    }

    public static Color ToColor(this OrderStatus s) => s switch
    {
        OrderStatus.Pending => Color.Warning,
        OrderStatus.OfferAccepted => Color.Info,
        OrderStatus.InProgress => Color.Primary,
        OrderStatus.Completed => Color.Success,
        OrderStatus.Cancelled => Color.Error,
        _ => Color.Default
    };

    public static string ToIcon(this OrderStatus s) => s switch
    {
        OrderStatus.Pending => Icons.Material.Filled.HourglassEmpty,
        OrderStatus.OfferAccepted => Icons.Material.Filled.Handshake,
        OrderStatus.InProgress => Icons.Material.Filled.Build,
        OrderStatus.Completed => Icons.Material.Filled.CheckCircle,
        OrderStatus.Cancelled => Icons.Material.Filled.Cancel,
        _ => Icons.Material.Filled.Circle
    };

    public static Color ToColor(this OfferStatus s) => s switch
    {
        OfferStatus.Pending => Color.Warning,
        OfferStatus.Accepted => Color.Success,
        OfferStatus.Rejected => Color.Error,
        OfferStatus.Expired => Color.Default,
        OfferStatus.Cancelled => Color.Dark,
        _ => Color.Default
    };

    public static Color ToColor(this PaymentStatus s) => s switch
    {
        PaymentStatus.Pending => Color.Warning,
        PaymentStatus.Success => Color.Success,
        PaymentStatus.Failed => Color.Error,
        PaymentStatus.Refunded => Color.Info,
        _ => Color.Default
    };

    public static Color ToColor(this PaymentMethod m) => m switch
    {
        PaymentMethod.Cash => Color.Default,
        PaymentMethod.CreditCard => Color.Info,
        PaymentMethod.Wallet => Color.Primary,
        PaymentMethod.Fawry => Color.Warning,
        PaymentMethod.VodafoneCash => Color.Error,
        _ => Color.Default
    };

    public static Color ToColor(this TransactionType t) => t switch
    {
        TransactionType.Deposit => Color.Success,
        TransactionType.Withdrawal => Color.Warning,
        TransactionType.OrderPayment => Color.Info,
        TransactionType.OrderEarning => Color.Primary,
        TransactionType.PlatformCommission => Color.Dark,
        TransactionType.Refund => Color.Error,
        TransactionType.AdminAdjustment => Color.Default,
        _ => Color.Default
    };

    public static Color ToColor(this ProviderStatus s) => s switch
    {
        ProviderStatus.Active => Color.Success,
        ProviderStatus.Suspended => Color.Error,
        ProviderStatus.Inactive => Color.Default,
        _ => Color.Default
    };

    public static Color ToColor(this ApplicationStatus s) => s switch
    {
        ApplicationStatus.Pending => Color.Warning,
        ApplicationStatus.Approved => Color.Success,
        ApplicationStatus.Rejected => Color.Error,
        ApplicationStatus.ActionRequired => Color.Info,
        _ => Color.Default
    };

    public static Color ToColor(this DocumentStatus s) => s switch
    {
        DocumentStatus.Pending => Color.Warning,
        DocumentStatus.Approved => Color.Success,
        DocumentStatus.Rejected => Color.Error,
        DocumentStatus.Expired => Color.Dark,
        DocumentStatus.ReUploadRequired => Color.Info,
        _ => Color.Default
    };

    public static Color ToColor(this UserType t) => t switch
    {
        UserType.Admin => Color.Error,
        UserType.Customer => Color.Info,
        UserType.Provider => Color.Primary,
        _ => Color.Default
    };

    public static Color ToColor(this ActionType a) => a switch
    {
        ActionType.Activate => Color.Success,
        ActionType.DeActivate => Color.Warning,
        ActionType.Delete => Color.Error,
        ActionType.Restore => Color.Info,
        ActionType.Cancellation => Color.Dark,
        _ => Color.Default
    };

    public static Color RatingColor(double? rating) => rating switch
    {
        null => Color.Default,
        >= 4.5 => Color.Success,
        >= 3.5 => Color.Info,
        >= 2.5 => Color.Warning,
        _ => Color.Error
    };
}
