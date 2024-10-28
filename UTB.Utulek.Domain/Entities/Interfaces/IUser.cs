using UTB.Utulek.Domain.Entities.Enums;

namespace UTB.Utulek.Domain.Entities.Interfaces;

public interface IUser<TKey>: IEntity<TKey>

{
    string? UserName { get; set; }
    string? Email { get; set; }
    string? PasswordHash { get; set; }
    string? PhoneNumber { get; set; }
    string? FirstName { get; set; }
    string? LastName { get; set; }
    UserRole UserRole { get; set; }
}