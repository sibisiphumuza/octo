using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace octo.Domain.Model
{
    public class OctoUser : IdentityUser
    {
        [Key]
        public string UserId { get; set; }
        public Guid UserGuid { get; set; }
        public required string? Fullname { get; set; }

        [DisplayName("Legacy Hours")]
        public string? DayNumber { get; set; }
        [DisplayName("Working Location")]
        public string? Location { get; set; }

        // Navigation properties
        public PersonalInformation? PersonalInformation { get; set; }
        public ICollection<Credential> Credentials { get; set; } = [];
        public ICollection<ContactInformation> ContactInformations { get; set; } = [];
        public ICollection<AccessPermission> AccessPermissions { get; set; } = [];
        public ICollection<EmergencyContact> EmergencyContacts { get; set; } = [];
        public ICollection<Akctivity>? Activities { get; set; }
        public HealthMetric? HealthMetrics { get; set; }

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; } // Nullable to indicate whether a token is currently active
    }
    public class PersonalInformation
    {
        [Key]
        public required string PersonalInformationId { get; set; }
        public Guid PersonalInformationGuid { get; set; }
        public string? FullName { get; set; }
        public byte[]? IdentityNumber { get; set; }
        public string? Address { get; set; }
        public string? Location { get; set; }
        public string? Gender { get; set; }
        public string? Pronoun { get; set; }

        // Navigation property
        public OctoUser OctoBase { get; set; }
    }
    public class Credential
    {
        [Key]
        public string CredentialId { get; set; }
        public string Type { get; set; } // e.g., "Password", "OAuthToken"
        public string Value { get; set; }

        // Foreign key
        public string UserId { get; set; }

        // Navigation property
        public OctoUser User { get; set; }
    }
    public class ContactInformation
    {
        [Key]
        public int ContactInformationId { get; set; }
        public string Type { get; set; } // e.g., "Phone", "Email"
        public string Value { get; set; }

        // Foreign key
        public string UserId { get; set; }

        // Navigation property
        public OctoUser User { get; set; }
    }
    public class AccessPermission
    {
        [Key]
        public int AccessPermissionId { get; set; }
        public string PermissionName { get; set; }

        // Foreign key
        public string UserId { get; set; }

        // Navigation property
        public OctoUser User { get; set; }
    }
    public class EmergencyContact
    {
        [Key]
        public string EmergencyContactId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Foreign key
        public string UserId { get; set; }

        // Navigation property
        public OctoUser User { get; set; }
    }

    #region Akctivity
    public class Akctivity
    {
        [Key]
        public int ActivityId { get; set; }
        public ActivityType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Steps { get; set; }
        public double Distance { get; set; } // In kilometers

        // Foreign key
        public string UserId { get; set; }

        // Navigation property
        public OctoUser User { get; set; }
    }
    public enum ActivityType
    {
        Walk,
        Run
    }

    #endregion

    #region Heart Rates
    public class HeartRate
    {
        [Key]
        public int HeartRateId { get; set; }
        public int Rate { get; set; } // Beats per minute
        public DateTime Timestamp { get; set; }
        public MonitoringDevice Device { get; set; }

        // Foreign key
        public string UserId { get; set; }

        // Navigation property
        public OctoUser User { get; set; }
    }

    public enum MonitoringDevice
    {
        Watch,
        ChestStrap
    }

    #endregion

    #region Health Metric

    public class HealthMetric
    {
        [Key]
        public int HealthMetricsId { get; set; }
        public bool IsDiabetic { get; set; }
        public bool HasHighBloodPressure { get; set; }

        // Foreign key
        public string UserId { get; set; }

        // Navigation property
        public OctoUser User { get; set; }
    }

    #endregion
}

