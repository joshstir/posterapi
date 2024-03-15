namespace PosterApi.Interfaces;

public abstract class AuditEntity
{
	// create properties for audit fields
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public string? CreatedBy { get; set; }
	public string? UpdatedBy { get; set; }
}

