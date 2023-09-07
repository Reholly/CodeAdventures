using Microsoft.AspNetCore.Authorization;

namespace Server.Authorization.AuthorizationRequirements;

public record ConcreteMentorRequirement : IAuthorizationRequirement;