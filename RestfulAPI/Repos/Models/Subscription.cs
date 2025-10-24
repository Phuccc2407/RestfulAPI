using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Repos.Models;

public partial class Subscription
{
    [Key]
    public long SubscriptionId { get; set; }

    public string UserId { get; set; }

    [StringLength(100)]
    public string PlanName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Subscriptions")]
    public virtual User User { get; set; } = null!;
}
