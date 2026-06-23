using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.BadgesEngine.DTOs;

/// <summary>Split response used by /learn/profile Badge Shelf and /learn/rewards board.</summary>
public record StudentBadgesDto(
    Guid StudentProfileId,
    IReadOnlyList<EarnedBadgeDto> Earned,
    IReadOnlyList<LockedBadgeDto> Locked
);