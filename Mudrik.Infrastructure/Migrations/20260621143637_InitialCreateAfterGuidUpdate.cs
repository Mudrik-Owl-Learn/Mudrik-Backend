using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mudrik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAfterGuidUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EligibilityCriteriaJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ConsentGiven = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandardLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeLevel = table.Column<int>(type: "int", nullable: false),
                    ChapterNumber = table.Column<int>(type: "int", nullable: false),
                    LessonOrder = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RawContentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LearningObjectivesJson = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "[]"),
                    PrerequisiteIdsJson = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "[]"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardLessons_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ParentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    GradeLevel = table.Column<int>(type: "int", nullable: false),
                    AvatarId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HasDyslexia = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HasADHD = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FontPreference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ColorOverlay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AudioEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    InterestsJson = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "[]"),
                    LearningStylePref = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonalityTag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OnboardingComplete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.Id);
                    table.CheckConstraint("CK_StudentProfiles_Age", "[Age] BETWEEN 4 AND 12");
                    table.CheckConstraint("CK_StudentProfiles_GradeLevel", "[GradeLevel] BETWEEN 1 AND 4");
                    table.ForeignKey(
                        name: "FK_StudentProfiles_ParentProfiles_ParentProfileId",
                        column: x => x.ParentProfileId,
                        principalTable: "ParentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StandardLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OptionsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectOptionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConceptTag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GradeLevel = table.Column<int>(type: "int", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_StandardLessons_StandardLessonId",
                        column: x => x.StandardLessonId,
                        principalTable: "StandardLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdaptedLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StandardLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdaptationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdaptationVersion = table.Column<int>(type: "int", nullable: false),
                    TotalChunks = table.Column<int>(type: "int", nullable: false),
                    AdaptedContentJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassedSafetyFilter = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SafetyFlagReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdaptedLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdaptedLessons_StandardLessons_StandardLessonId",
                        column: x => x.StandardLessonId,
                        principalTable: "StandardLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdaptedLessons_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamificationStreaks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CurrentLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CurrentStreak = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LongestStreak = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastStreakDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FreezeTokensAvailable = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamificationStreaks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamificationStreaks_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnerAIProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DyslexiaSeverity = table.Column<int>(type: "int", nullable: false),
                    ADHDSeverity = table.Column<int>(type: "int", nullable: false),
                    ReadingScore = table.Column<int>(type: "int", nullable: false),
                    WritingScore = table.Column<int>(type: "int", nullable: false),
                    ComprehensionScore = table.Column<int>(type: "int", nullable: false),
                    AttentionSpanScore = table.Column<int>(type: "int", nullable: false),
                    AttentionSpanMinutes = table.Column<int>(type: "int", nullable: false),
                    PreferredFormat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChunkSizePref = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConfidenceBias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AudioSupportRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    NumeracyLevel = table.Column<int>(type: "int", nullable: false),
                    ReadingLevel = table.Column<int>(type: "int", nullable: false),
                    DiagnosticResultJson = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "[]"),
                    ProfileVersion = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnerAIProfiles", x => x.Id);
                    table.CheckConstraint("CK_LearnerAIProfiles_ADHDSeverity", "[ADHDSeverity] BETWEEN 0 AND 5");
                    table.CheckConstraint("CK_LearnerAIProfiles_AttentionSpanScore", "[AttentionSpanScore] BETWEEN 0 AND 100");
                    table.CheckConstraint("CK_LearnerAIProfiles_ComprehensionScore", "[ComprehensionScore] BETWEEN 0 AND 100");
                    table.CheckConstraint("CK_LearnerAIProfiles_DyslexiaSeverity", "[DyslexiaSeverity] BETWEEN 0 AND 5");
                    table.CheckConstraint("CK_LearnerAIProfiles_ReadingScore", "[ReadingScore] BETWEEN 0 AND 100");
                    table.CheckConstraint("CK_LearnerAIProfiles_WritingScore", "[WritingScore] BETWEEN 0 AND 100");
                    table.ForeignKey(
                        name: "FK_LearnerAIProfiles_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentBadges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BadgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasBeenDisplayed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EarnedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBadges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentBadges_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentBadges_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLessonStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StandardLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    AverageQuizScore = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    TotalAttempts = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NextReviewDate = table.Column<DateTime>(type: "date", nullable: true),
                    SpacedRepInterval = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SpacedRepBox = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    FirstStartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAttemptedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasteredAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessonStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLessonStates_StandardLessons_StandardLessonId",
                        column: x => x.StandardLessonId,
                        principalTable: "StandardLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentLessonStates_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonMicroChunks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AdaptedLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChunkOrder = table.Column<int>(type: "int", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ContentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AudioScriptUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IllustrationUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EstimatedDurationSeconds = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonMicroChunks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonMicroChunks_AdaptedLessons_AdaptedLessonId",
                        column: x => x.AdaptedLessonId,
                        principalTable: "AdaptedLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonMicroChunks_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "XpTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamificationStreakId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseXp = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    BonusXp = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    StreakMultiplier = table.Column<decimal>(type: "decimal(4,2)", nullable: false, defaultValue: 1.0m),
                    TotalXpAwarded = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: true),
                    ReferenceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AwardedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XpTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XpTransactions_GamificationStreaks_GamificationStreakId",
                        column: x => x.GamificationStreakId,
                        principalTable: "GamificationStreaks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_XpTransactions_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgentGeneratedQuizzes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonMicroChunkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StandardLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttemptNumber = table.Column<int>(type: "int", nullable: false),
                    AudioReplayCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ScorePercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    IsPassed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TotalTimeSeconds = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentGeneratedQuizzes", x => x.Id);
                    table.CheckConstraint("CK_AgentGeneratedQuizzes_AttemptNumber", "[AttemptNumber] >= 1");
                    table.CheckConstraint("CK_AgentGeneratedQuizzes_ScorePercent", "[ScorePercent] BETWEEN 0.00 AND 100.00");
                    table.ForeignKey(
                        name: "FK_AgentGeneratedQuizzes_LessonMicroChunks_LessonMicroChunkId",
                        column: x => x.LessonMicroChunkId,
                        principalTable: "LessonMicroChunks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgentGeneratedQuizzes_StandardLessons_StandardLessonId",
                        column: x => x.StandardLessonId,
                        principalTable: "StandardLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgentGeneratedQuizzes_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentQuizAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AgentGeneratedQuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedOptionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    TimeToAnswerMs = table.Column<int>(type: "int", nullable: false),
                    AnswerChangeCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuizAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentQuizAnswers_AgentGeneratedQuizzes_AgentGeneratedQuizId",
                        column: x => x.AgentGeneratedQuizId,
                        principalTable: "AgentGeneratedQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentQuizAnswers_QuizQuestions_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentQuizAnswers_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdaptedLessons_StandardLessonId",
                table: "AdaptedLessons",
                column: "StandardLessonId");

            migrationBuilder.CreateIndex(
                name: "UIX_AdaptedLessons_ActivePerStudentLesson",
                table: "AdaptedLessons",
                columns: new[] { "StudentProfileId", "StandardLessonId" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_AgentGeneratedQuizzes_LessonMicroChunkId",
                table: "AgentGeneratedQuizzes",
                column: "LessonMicroChunkId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentGeneratedQuizzes_StandardLessonId",
                table: "AgentGeneratedQuizzes",
                column: "StandardLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentGeneratedQuizzes_StudentProfileId_LessonMicroChunkId_AttemptNumber",
                table: "AgentGeneratedQuizzes",
                columns: new[] { "StudentProfileId", "LessonMicroChunkId", "AttemptNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GamificationStreaks_StudentProfileId",
                table: "GamificationStreaks",
                column: "StudentProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnerAIProfiles_StudentProfileId",
                table: "LearnerAIProfiles",
                column: "StudentProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonMicroChunks_AdaptedLessonId_ChunkOrder",
                table: "LessonMicroChunks",
                columns: new[] { "AdaptedLessonId", "ChunkOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonMicroChunks_StudentProfileId",
                table: "LessonMicroChunks",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentProfiles_UserId",
                table: "ParentProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_StandardLessonId",
                table: "QuizQuestions",
                column: "StandardLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_SubjectId",
                table: "QuizQuestions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardLessons_SubjectId",
                table: "StandardLessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBadges_BadgeId",
                table: "StudentBadges",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBadges_StudentProfileId_BadgeId",
                table: "StudentBadges",
                columns: new[] { "StudentProfileId", "BadgeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonStates_StandardLessonId",
                table: "StudentLessonStates",
                column: "StandardLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonStates_StudentProfileId_StandardLessonId",
                table: "StudentLessonStates",
                columns: new[] { "StudentProfileId", "StandardLessonId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_ParentProfileId",
                table: "StudentProfiles",
                column: "ParentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAnswers_AgentGeneratedQuizId",
                table: "StudentQuizAnswers",
                column: "AgentGeneratedQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAnswers_QuizQuestionId",
                table: "StudentQuizAnswers",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAnswers_StudentProfileId",
                table: "StudentQuizAnswers",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_XpTransactions_GamificationStreakId",
                table: "XpTransactions",
                column: "GamificationStreakId");

            migrationBuilder.CreateIndex(
                name: "IX_XpTransactions_StudentProfileId",
                table: "XpTransactions",
                column: "StudentProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LearnerAIProfiles");

            migrationBuilder.DropTable(
                name: "StudentBadges");

            migrationBuilder.DropTable(
                name: "StudentLessonStates");

            migrationBuilder.DropTable(
                name: "StudentQuizAnswers");

            migrationBuilder.DropTable(
                name: "XpTransactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "AgentGeneratedQuizzes");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "GamificationStreaks");

            migrationBuilder.DropTable(
                name: "LessonMicroChunks");

            migrationBuilder.DropTable(
                name: "AdaptedLessons");

            migrationBuilder.DropTable(
                name: "StandardLessons");

            migrationBuilder.DropTable(
                name: "StudentProfiles");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "ParentProfiles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
