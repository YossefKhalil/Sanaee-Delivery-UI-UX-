
namespace Core.Domain.Enums
{
    public enum ProviderStatus
    {
        Active = 1,          // الحساب نشط ومقبول وجاهز لتلقي الطلبات
        Suspended = 2,         // الحساب محظور/موقوف بقرار من الأدمن (بسبب مخالفة أو تقييم منخفض)
        Inactive = 3           // الفني قام بإيقاف حسابه مؤقتاً بنفسه (قام بتعطيل حسابه)
    }
}
