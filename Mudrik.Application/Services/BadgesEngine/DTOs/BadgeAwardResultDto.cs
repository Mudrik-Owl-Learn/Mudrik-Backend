using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.BadgesEngine.DTOs;

/// <summary>Returned after CheckAndAwardBadges — lists what was newly awarded this evaluation.</summary>
public record BadgeAwardResultDto(
    IReadOnlyList<EarnedBadgeDto> NewlyAwarded
);