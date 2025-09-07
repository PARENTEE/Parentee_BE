namespace Parentee_BE.DAL.Data.Enums
{
    public enum SigninMethod { App, Google }

    public enum DiaperType { Wet, Dirty, Mixed, Dry }

    public enum EntitlementStatus { Active, Expired, Revoked }

    public enum FamilyRole { Father, Mother, Others }

    public enum FeedingMethod { Breast, Bottle, Formula, Solid }

    public enum MeasureType { Weight, Length, HeadCircumference }

    public enum PaymentMethod { Vietqr, Momo, Vnpay, Viettelmoney, Napas, CreditCard, ApplePay, GooglePay }

    public enum PriceType { OneTime, RecurringMonth, RecurringYear }

    public enum PurchaseStatus { Pending, Paid, Failed, Refunded, Chargeback }

    public enum ReminderChannel { Push, Email, Sms }

    public enum TaskStatus { Pending, Completed, Cancelled }

    public enum VaccinationStatus { Scheduled, Done, Skipped, Cancelled }
}