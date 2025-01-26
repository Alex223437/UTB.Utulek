namespace UTB.Utulek.Domain.Entities;

public class Donation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; } // Сумма пожертвования
    public string Message { get; set; } = string.Empty; // Сообщение от донора
    public DateTime Date { get; set; } = DateTime.UtcNow; // Дата пожертвования

    // Навигационные свойства
    public User? User { get; set; }
}