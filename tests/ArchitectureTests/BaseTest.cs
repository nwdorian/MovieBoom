using System.Reflection;
using Application.Abstractions.Database;
using Domain.Movies;
using Infrastructure.Database;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Movie).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IApplicationDbContext).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly WebAssembly = typeof(Program).Assembly;
}
