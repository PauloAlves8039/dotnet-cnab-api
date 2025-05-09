namespace CNAB.Application.DTOs;

public class StoreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public decimal Balance { get; set; }
}