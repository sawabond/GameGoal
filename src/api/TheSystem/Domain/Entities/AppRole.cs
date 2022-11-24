﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppRole : IdentityRole<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    public virtual IEnumerable<AppUser> Users { get; set; }
}
