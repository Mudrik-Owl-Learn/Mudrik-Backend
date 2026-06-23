using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.BadgesEngine.DTOs;

/// <summary>Locked badge shows silhouette only — no description exposed to avoid spoilers.</summary>
public record LockedBadgeDto(
    Guid BadgeId,
    string Name,
    string SilhouetteImageUrl
);