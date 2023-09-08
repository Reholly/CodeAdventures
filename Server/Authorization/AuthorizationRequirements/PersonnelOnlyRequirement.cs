using Microsoft.AspNetCore.Authorization;

namespace Server.Authorization.AuthorizationRequirements;

public record PersonnelOnlyRequirement : IAuthorizationRequirement;