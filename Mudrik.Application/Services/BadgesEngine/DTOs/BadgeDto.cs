using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.BadgesEngine.DTOs;
public record BadgeDto(
        Guid Id,
        string Name,
        string Description,
        string Rarity,
        string ImageUrl,
        string EligibilityCriteriaJson,
        bool IsActive
    );
