namespace SanaeeUI.Client.Models;

// Mirrors Core\Domain\Enums of the Sanaee-Delivery backend.

public enum UserType
{
    Admin = 1,
    Customer = 2,
    Provider = 3
}

public enum OrderStatus
{
    Pending = 1,
    OfferAccepted = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5
}

public enum OfferStatus
{
    Pending = 1,
    Accepted = 2,
    Rejected = 3,
    Expired = 4,
    Cancelled = 5
}

public enum PaymentStatus
{
    Pending = 1,
    Success = 2,
    Failed = 3,
    Refunded = 4
}

public enum PaymentMethod
{
    Cash = 1,
    CreditCard = 2,
    Wallet = 3,
    Fawry = 4,
    VodafoneCash = 5
}

public enum PaymentType
{
    OrderPayment = 1,
    WalletTopUp = 2,
    SubscriptionFee = 3
}

public enum TransactionType
{
    Deposit = 1,
    Withdrawal = 2,
    OrderPayment = 3,
    OrderEarning = 4,
    PlatformCommission = 5,
    Refund = 6,
    AdminAdjustment = 7
}

public enum MessageType
{
    Text = 1,
    Image = 2,
    VoiceNote = 3,
    Location = 4,
    Document = 5
}

public enum ProviderStatus
{
    Active = 1,
    Suspended = 2,
    Inactive = 3
}

public enum ApplicationStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    ActionRequired = 4
}

public enum DocumentType
{
    NationalId = 1,
    CriminalRecord = 2,
    WorkPermit = 3,
    PersonalPhoto = 4
}

public enum DocumentStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Expired = 4,
    ReUploadRequired = 5
}

public enum EntityName
{
    User = 1,
    Wallet = 2,
    Order = 3
}

public enum ActionType
{
    DeActivate = 1,
    Activate = 2,
    Delete = 3,
    Restore = 4,
    Cancellation = 5
}
