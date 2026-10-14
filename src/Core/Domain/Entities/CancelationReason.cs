
using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class CancelationReason
    {
        public int ReasonId { get; set; }
        public string Text { get; set; } = string.Empty;
        public UserType ApplicableTo { get; set; }
        public bool IsActive { get; private set; }


        public void Freeze()
        {
            // check if it is already Deactivated
            if (!IsActive) return;

            IsActive = false;
        }
        public void Unfreeze()
        {
            // check if it is already Activated
            if (IsActive) return;

            IsActive = true;
        }
    }
}
