using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Domain.Enums;
public enum Gender
{
    Male,
    Female,
}


public enum LessonState
{
    Locked,
    Unlocked,
    InProgress,
    Completed,
    Mastered,
    RevisitRequired
}