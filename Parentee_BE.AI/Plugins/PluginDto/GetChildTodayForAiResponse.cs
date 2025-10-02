using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.AI.Plugins.PluginDto
{
    public class GetChildTodayForAiResponse
    {
        [Description("The unique identifier of the child.")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Description("The unique identifier of the family this child belongs to.")]
        [JsonPropertyName("familyId")]
        public Guid FamilyId { get; set; }

        [Description("The full name of the child.")]
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [Description("The birth date of the child.")]
        [JsonPropertyName("birthDate")]
        public DateOnly BirthDate { get; set; }

        [Description("The biological sex of the child (e.g., Male, Female).")]
        [JsonPropertyName("sex")]
        public string? Sex { get; set; }

        [Description("The ID of the child's profile photo image, if available.")]
        [JsonPropertyName("photoImageId")]
        public Guid? PhotoImageId { get; set; }

        [Description("Additional notes or information about the child.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [Description("The most recent measurement record for the child.")]
        [JsonPropertyName("measurement")]
        public virtual MeasurementResponse Measurement { get; set; }

        [Description("A list of all diaper change events recorded for the child today.")]
        [JsonPropertyName("diaperChanges")]
        public virtual ICollection<DiaperChangeResponse> DiaperChanges { get; set; }

        [Description("A list of all feeding records for the child today.")]
        [JsonPropertyName("feedings")]
        public virtual ICollection<FeedingResponse> Feedings { get; set; }

        [Description("A list of all sleep sessions for the child today.")]
        [JsonPropertyName("sleeps")]
        public virtual ICollection<SleepResponse> Sleeps { get; set; }
    }

    public class MeasurementResponse
    {
        [Description("The type of measurement (e.g., Weight, Height).")]
        [JsonPropertyName("type")]
        public MeasureType Type { get; set; }

        [Description("The date and time when the measurement was taken.")]
        [JsonPropertyName("measuredAt")]
        public DateTime MeasuredAt { get; set; }

        [Description("The measured value.")]
        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [Description("The unit of measurement (e.g., kg, cm).")]
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = null!;

        [Description("The source of the measurement data, if available.")]
        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [Description("Additional notes about the measurement.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [Description("The date and time this record was created.")]
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Description("The date and time this record was last updated.")]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

    public class DiaperChangeResponse
    {
        [Description("The date and time the diaper change occurred.")]
        [JsonPropertyName("changedAt")]
        public DateTime ChangedAt { get; set; }

        [Description("The type of diaper change (e.g., Wet, Dirty).")]
        [JsonPropertyName("type")]
        public DiaperType Type { get; set; }

        [Description("Whether a rash was observed during the diaper change.")]
        [JsonPropertyName("rashObserved")]
        public bool? RashObserved { get; set; }

        [Description("Additional notes about the diaper change event.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [Description("The date and time this record was created.")]
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Description("The date and time this record was last updated.")]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

    public class FeedingResponse
    {
        [Description("The method of feeding (e.g., Breastfeeding, Bottle).")]
        [JsonPropertyName("method")]
        public FeedingMethod Method { get; set; }

        [Description("The start time of the feeding session.")]
        [JsonPropertyName("startedAt")]
        public DateTime StartedAt { get; set; }

        [Description("The end time of the feeding session, if available.")]
        [JsonPropertyName("endedAt")]
        public DateTime? EndedAt { get; set; }

        [Description("The duration of the feeding session in minutes.")]
        [JsonPropertyName("durationMin")]
        public int? DurationMin { get; set; }

        [Description("The amount of milk consumed in milliliters, if available.")]
        [JsonPropertyName("amountMl")]
        public decimal? AmountMl { get; set; }

        [Description("The side used for breastfeeding (e.g., Left, Right), if applicable.")]
        [JsonPropertyName("side")]
        public string? Side { get; set; }

        [Description("Additional notes about the feeding session.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [Description("The date and time this record was created.")]
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Description("The date and time this record was last updated.")]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }

    public class SleepResponse
    {
        [Description("The start time of the sleep session.")]
        [JsonPropertyName("startedAt")]
        public DateTime StartedAt { get; set; }

        [Description("The end time of the sleep session, if available.")]
        [JsonPropertyName("endedAt")]
        public DateTime? EndedAt { get; set; }

        [Description("The total duration of the sleep session in minutes.")]
        [JsonPropertyName("durationMin")]
        public int? DurationMin { get; set; }

        [Description("The location where the child slept (e.g., crib, stroller).")]
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [Description("Additional notes about the sleep session.")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [Description("The date and time this record was created.")]
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Description("The date and time this record was last updated.")]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
