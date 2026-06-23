using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.BadgesEngine.DTOs;

/// <summary>Earned badge with display state — used in Badge Shelf and Rewards Board.</summary>
public record EarnedBadgeDto(
    Guid BadgeId,
    string Name,
    string Description,
    string Rarity,
    string ImageUrl,
    bool HasBeenDisplayed,
    DateTime EarnedAt
);