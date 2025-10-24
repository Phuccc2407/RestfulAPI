using RestfulAPI.Repos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulAPI.Modal
{
    public class CustomerModal
    {
        public Guid UserId { get; set; }

        [StringLength(100)]
        public string UserName { get; set; } = null!;

        [StringLength(256)]
        public string Email { get; set; } = null!;

        [StringLength(512)]
        public string PasswordHash { get; set; } = null!;

        public bool? IsActive { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

        [InverseProperty("PerformedByNavigation")]
        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

        [InverseProperty("FolloweeUser")]
        public virtual ICollection<Follow> FollowFolloweeUsers { get; set; } = new List<Follow>();

        [InverseProperty("Follower")]
        public virtual ICollection<Follow> FollowFollowers { get; set; } = new List<Follow>();

        [InverseProperty("User")]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [InverseProperty("OwnerUser")]
        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

        [InverseProperty("User")]
        public virtual ICollection<StreamHistory> StreamHistories { get; set; } = new List<StreamHistory>();

        [InverseProperty("User")]
        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        [InverseProperty("User")]
        public virtual ICollection<TrackLike> TrackLikes { get; set; } = new List<TrackLike>();

        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public string? Statusname { get; set; }
    }
}
