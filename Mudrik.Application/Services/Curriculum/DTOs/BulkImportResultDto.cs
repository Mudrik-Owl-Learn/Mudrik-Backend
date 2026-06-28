using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.DTOs;
/// <summary>
/// Bulk import result — returned after POST /api/curriculum/import.
/// </summary>
public record BulkImportResultDto(
    int TotalProcessed,
    int Created,
    int Skipped,
    IReadOnlyList<string> Errors
);
